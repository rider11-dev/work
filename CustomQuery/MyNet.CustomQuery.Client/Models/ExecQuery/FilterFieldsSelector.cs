using MyNet.Client.Public;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using MyNet.CustomQuery.Model;
using MyNet.Model.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MyNet.CustomQuery.Client.Models.ExecQuery
{
    public class FilterFieldsSelector
    {
        private ExecQueryModel QModel;
        public DataGridComboBoxColumn DgColFieldType { get; set; }
        public DataGridComboBoxColumn DgColConditionType { get; set; }
        public DataGridTemplateColumn DgColValue { get; set; }
        //过滤条件组合方式
        public ObservableCollection<CmbItem> CmpTypes { get; set; }
        //过滤类型
        public ObservableCollection<CmbItem> ConditionTypes { get; set; }
        //字段类型
        public ObservableCollection<CmbItem> FieldTypes { get; set; }
        //是否取反——布尔
        public ObservableCollection<CmbItem> Booleans { get; set; }
        //过滤字段视图数据源
        public CollectionViewSource ViewSrcFilterFields { get; set; }

        public FilterFieldsSelector(ExecQueryModel qModel)
        {
            QModel = qModel;

            CmpTypes = new ObservableCollection<CmbItem>(DataCacheHelper.GetEnumCmbSource<CompositeType>());
            ConditionTypes = new ObservableCollection<CmbItem>(DataCacheHelper.GetEnumCmbSource<ConditionType>());
            FieldTypes = new ObservableCollection<CmbItem>(DataCacheHelper.GetEnumCmbSource<FieldType>());
            Booleans = new ObservableCollection<CmbItem>(DataCacheHelper.GetEnumCmbSource<BoolType>());
        }

        public void FilterFilterFieldsSrc(IEnumerable<FieldViewModel> baseFields = null)
        {
            ICollectionView view = ViewSrcFilterFields.View;
            if (view == null)
            {
                return;
            }

            if (baseFields == null)
            {
                baseFields = QModel.GetFieldsBySelectedTable();
            }
            if (baseFields.IsEmpty())
            {
                view.Filter = model => { return 1 == 0; };
                return;
            }
            var leftFields = baseFields.Where(f => QModel.SelectedConditions.Where(c => c.Field == f.fieldname).Count() == 0);
            view.Filter = model =>
            {
                return leftFields.Contains((FieldViewModel)model);
            };

            //删除已选但不存在的过滤字段
            QModel.SelectedConditions.DeleteBatch(QModel.SelectedConditions.Where(c => baseFields.Count(f => f.fieldname == c.Field) < 1));
        }

        private ICommand _addFilterFieldsCmd;
        public ICommand AddFilterFieldsCmd
        {
            get
            {
                if (_addFilterFieldsCmd == null)
                {
                    _addFilterFieldsCmd = new DelegateCommand(AddFilterFieldsAction);
                }
                return _addFilterFieldsCmd;
            }
        }

        private void AddFilterFieldsAction(object obj)
        {
            var fields = obj as IEnumerable<object>;
            if (fields.IsEmpty())
            {
                return;
            }
            var conditions = fields.Select(o =>
            {
                var field = o as FieldViewModel;
                return new ConditionViewModel
                {
                    Field = field.fieldname,
                    FieldFullName = field.fieldfullname,
                    CmpType = CompositeType.And,
                    ConditionType = ConditionType.Equal,
                    FieldType = field.fieldtype.ToEnum<FieldType>(),
                    IsChecked = false
                };
            });
            QModel.SelectedConditions.AddRange(conditions);

            FilterFilterFieldsSrc();
        }

        private ICommand _delFilterFieldsCmd;
        public ICommand DelFilterFieldsCmd
        {
            get
            {
                if (_delFilterFieldsCmd == null)
                {
                    _delFilterFieldsCmd = new DelegateCommand(DelFilterFieldsAction);
                }
                return _delFilterFieldsCmd;
            }
        }

        private void DelFilterFieldsAction(object obj)
        {
            var conditions = obj as IEnumerable<object>;
            if (conditions.IsEmpty())
            {
                return;
            }

            QModel.SelectedConditions.DeleteBatch(conditions.Select(o => o as ConditionViewModel));
            FilterFilterFieldsSrc();
        }

    }
}
