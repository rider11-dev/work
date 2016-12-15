using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.CustomQuery.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace MyNet.CustomQuery.Client.Models.ExecQuery
{
    public class SortFieldsSelector
    {
        private ExecQueryModel QModel;
        //排序字段视图数据源
        public CollectionViewSource ViewSrcSortFields { get; set; }
        public CollectionViewSource ViewSortFields { get; set; }
        public SortFieldsSelector(ExecQueryModel qModel)
        {
            QModel = qModel;
        }

        public void FilterSortFieldsSrc(IEnumerable<FieldViewModel> baseFields = null)
        {
            ICollectionView view = ViewSrcSortFields.View;
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
            var leftFields = baseFields.Where(f => QModel.SelectedSorts.Where(s => s.Field == f.fieldname).Count() == 0);
            view.Filter = model =>
            {
                return leftFields.Contains((FieldViewModel)model);
            };

            //删除已选但不存在的排序字段
            QModel.SelectedSorts.DeleteBatch(QModel.SelectedSorts.Where(s => baseFields.Count(f => f.fieldname == s.Field) < 1));
        }

        private ICommand _addSortFieldsCmd;
        public ICommand AddSortFieldsCmd
        {
            get
            {
                if (_addSortFieldsCmd == null)
                {
                    _addSortFieldsCmd = new DelegateCommand(AddSortFieldsAction);
                }
                return _addSortFieldsCmd;
            }
        }

        private void AddSortFieldsAction(object obj)
        {
            var fields = obj as IEnumerable<object>;
            if (fields.IsEmpty())
            {
                return;
            }
            //TODO
            var maxOrder = QModel.SelectedSorts.GetMax(s => s.Order) + 1;
            var sorts = fields.Select(o =>
              {
                  var field = o as FieldViewModel;
                  //这里不能直接设置Order，因为会导致不连续
                  return new SortViewModel { Field = field.fieldname, SortType = SortType.Asc, DisplayName = field.displayname };
              }).ToList();//需要转换为List，IEnumerable延迟查询
            sorts.ForEach(s => s.Order = maxOrder++);
            QModel.SelectedSorts.AddRange(sorts);
            FilterSortFieldsSrc();
        }

        private ICommand _delSortFieldsCmd;
        public ICommand DelSortFieldsCmd
        {
            get
            {
                if (_delSortFieldsCmd == null)
                {
                    _delSortFieldsCmd = new DelegateCommand(DelSortFieldsAction);
                }
                return _delSortFieldsCmd;
            }
        }

        private void DelSortFieldsAction(object obj)
        {
            var sorts = obj as IEnumerable<object>;
            if (sorts.IsEmpty())
            {
                return;
            }
            QModel.SelectedSorts.DeleteBatch(() =>
            {
                var lst = sorts.Select(o => (SortViewModel)o);
                lst.ToList().ForEach(s => s.ResetOrder());//重置序号
                return lst;
            });
            FilterSortFieldsSrc();
        }

        private ICommand _ascSortCmd;
        public ICommand AscSortCmd
        {
            get
            {
                if (_ascSortCmd == null)
                {
                    _ascSortCmd = new DelegateCommand(AscSortAction);
                }
                return _ascSortCmd;
            }
        }

        private void AscSortAction(object obj)
        {
            SetSortType(obj as ObservableCollection<object>, SortType.Asc);
        }

        private ICommand _descSortCmd;
        public ICommand DescSortCmd
        {
            get
            {
                if (_descSortCmd == null)
                {
                    _descSortCmd = new DelegateCommand(DescSortAction);
                }
                return _descSortCmd;
            }
        }

        private void DescSortAction(object obj)
        {
            SetSortType(obj as ObservableCollection<object>, SortType.Desc);
        }

        private static void SetSortType(ObservableCollection<object> sorts, SortType type)
        {
            if (sorts.IsEmpty())
            {
                return;
            }
            foreach (var sort in sorts)
            {
                (sort as SortViewModel).SortType = type;
            }
        }


        private ICommand _moveUpSortFieldCmd;
        public ICommand MoveUpSortFieldCmd
        {
            get
            {
                if (_moveUpSortFieldCmd == null)
                {
                    _moveUpSortFieldCmd = new DelegateCommand(MoveUpSortFieldAction);
                }
                return _moveUpSortFieldCmd;
            }
        }

        private void MoveUpSortFieldAction(object obj)
        {
            var cur = obj as SortViewModel;
            if (QModel.SelectedSorts.IsEmpty() || cur == null || cur.Order == QModel.SelectedSorts.GetMin(s => s.Order))
            {
                return;
            }
            //上移：将当前order与前一个替换
            var pre = QModel.SelectedSorts.Where(s => s.Order == cur.Order - 1).First();
            var temp = pre.Order;
            pre.Order = cur.Order;
            cur.Order = temp;

            SortSortFields();
        }

        private ICommand _moveDownSortFieldCmd;
        public ICommand MoveDownSortFieldCmd
        {
            get
            {
                if (_moveDownSortFieldCmd == null)
                {
                    _moveDownSortFieldCmd = new DelegateCommand(MoveDownSortFieldAction);
                }
                return _moveDownSortFieldCmd;
            }
        }

        private void MoveDownSortFieldAction(object obj)
        {
            var cur = obj as SortViewModel;
            if (QModel.SelectedSorts.IsEmpty() || cur == null || cur.Order == QModel.SelectedSorts.GetMax(s => s.Order))
            {
                return;
            }
            //下移
            var next = QModel.SelectedSorts.Where(s => s.Order == cur.Order + 1).First();
            var temp = next.Order;
            next.Order = cur.Order;
            cur.Order = temp;

            SortSortFields();
        }

        private void SortSortFields()
        {
            var view = ViewSortFields.View;
            if (view == null)
            {
                return;
            }
            if (view.SortDescriptions.IsEmpty())
            {
                view.SortDescriptions.Add(new SortDescription("Order", ListSortDirection.Ascending));
            }
            else
            {
                view.Refresh();
            }
        }
    }
}
