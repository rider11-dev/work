using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.CustomQuery.Client.Models.ExecQuery;
using MyNet.CustomQuery.Model;
using MyNet.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public class ExecQueryModel : BaseModel
    {
        public QueryExecutor QueryExecutor { get; set; }
        private DataSourceInitalizer DataSrcInitalizer;
        public ExecQueryModel()
        {
            JoinTables = new ObservableCollection<JoinTable>();
            RelFields = new ObservableCollection<RelationField>();
            SelectedFields = new ObservableCollection<FieldViewModel>();
            SelectedConditions = new ObservableCollection<Condition>();
            SelectedSorts = new ObservableCollection<SortViewModel>();
            QueryExecutor = new QueryExecutor(this);
            DataSrcInitalizer = new DataSourceInitalizer(this);

            DataSrcInitalizer.Init();
        }

        #region 2、查询模板数据
        private string _queryTCode;
        public string QueryTCode
        {
            get { return _queryTCode; }
            set
            {
                if (_queryTCode != value)
                {
                    _queryTCode = value;
                    base.RaisePropertyChanged("QueryTCode");
                }
            }
        }

        TableViewModel _table_main;
        //主表
        public TableViewModel TableMain
        {
            get { return _table_main; }
            set
            {
                if (_table_main != value)
                {
                    _table_main = value;
                    base.RaisePropertyChanged("TableMain");
                }
            }
        }
        //关联表
        public ObservableCollection<JoinTable> JoinTables { get; set; }
        //关联字段
        public ObservableCollection<RelationField> RelFields { get; set; }
        //查询结果字段
        public ObservableCollection<FieldViewModel> SelectedFields { get; set; }
        public CollectionViewSource ViewSelectedFields { get; set; }
        //过滤条件字段
        public ObservableCollection<Condition> SelectedConditions { get; set; }
        //排序字段
        public ObservableCollection<SortViewModel> SelectedSorts { get; set; }
        #endregion

        #region 3、数据源
        public ObservableCollection<TableViewModel> Tables { get; set; }

        public ObservableCollection<CmbItem> JoinTypes { get; set; }

        public ObservableCollection<FieldViewModel> Fields { get; set; }
        //基础可选字段视图数据源
        public CollectionViewSource ViewSrcBaseFields { get; set; }
        //查询结果字段视图数据源
        public CollectionViewSource ViewSrcSelFields { get; set; }
        //过滤字段视图数据源
        public CollectionViewSource ViewSrcFilterFields { get; set; }
        //排序字段视图数据源
        public CollectionViewSource ViewSrcSortFields { get; set; }
        #region 3.1数据源过滤

        public void FilterBaseFieldsSrc()
        {
            ICollectionView view = ViewSrcBaseFields.View;
            if (view == null)
            {
                return;
            }
            var baseFields = GetFieldsBySelectedTable();
            if (baseFields.IsEmpty())
            {
                view.Filter = model => { return 1 == 0; };
            }
            else
            {
                view.Filter = model => { return baseFields.Contains((FieldViewModel)model); };
            }

            //基础字段发生变化后，查询结果字段、过滤、排序数据源都得更新
            FilterSelFieldsSrc(baseFields);
            FilterFilterFieldsSrc(baseFields);
            FilterSortFieldsSrc(baseFields);
        }

        /// <summary>
        /// 根据查询表（包含主表和关联表）获取所有查询字段
        /// </summary>
        /// <returns></returns>
        private IEnumerable<FieldViewModel> GetFieldsBySelectedTable()
        {
            if (TableMain == null && JoinTables.IsEmpty())
            {
                return null;
            }
            return Fields.Where(field =>
                            (JoinTables.IsNotEmpty() && JoinTables.Select(t => t.Table).Contains(field.tbname)) ||
                            (TableMain != null && field.tbname == TableMain.tbname));
        }

        /// <summary>
        /// 过滤查询结果字段选择源，同理还有过滤条件、排序选择源
        /// 因为有左右选择，所以需要添加额外过滤条件
        /// </summary>
        private void FilterSelFieldsSrc(IEnumerable<FieldViewModel> baseFields = null)
        {
            ICollectionView view = ViewSrcSelFields.View;
            if (view == null)
            {
                return;
            }

            if (baseFields == null)
            {
                baseFields = GetFieldsBySelectedTable();
            }
            if (baseFields.IsEmpty())
            {
                view.Filter = model => { return 1 == 0; };
                return;
            }
            var leftFields = baseFields.Except(SelectedFields);//差集
            view.Filter = model => { return leftFields.Contains((FieldViewModel)model); };
            //删除已选但不存在的查询字段
            SelectedFields.DeleteBatch(SelectedFields.Where(sel => !baseFields.Contains(sel)));//删除SelectedFields中存在而fields中不存在的
        }

        private void FilterFilterFieldsSrc(IEnumerable<FieldViewModel> baseFields = null)
        {
            ICollectionView view = ViewSrcFilterFields.View;
            if (view == null)
            {
                return;
            }

            if (baseFields == null)
            {
                baseFields = GetFieldsBySelectedTable();
            }
            if (baseFields.IsEmpty())
            {
                view.Filter = model => { return 1 == 0; };
                return;
            }
            var leftFields = baseFields.Where(f => SelectedConditions.Where(c => c.Field == f.fieldname).Count() == 0);
            view.Filter = model =>
            {
                return leftFields.Contains((FieldViewModel)model);
            };

            //删除已选但不存在的过滤字段
            SelectedConditions.DeleteBatch(SelectedConditions.Where(c => baseFields.Count(f => f.fieldname == c.Field) < 1));
        }

        private void FilterSortFieldsSrc(IEnumerable<FieldViewModel> baseFields = null)
        {
            ICollectionView view = ViewSrcSortFields.View;
            if (view == null)
            {
                return;
            }
            if (baseFields == null)
            {
                baseFields = GetFieldsBySelectedTable();
            }
            if (baseFields.IsEmpty())
            {
                view.Filter = model => { return 1 == 0; };
                return;
            }
            var leftFields = baseFields.Where(f => SelectedSorts.Where(s => s.Field == f.fieldname).Count() == 0);
            view.Filter = model =>
            {
                return leftFields.Contains((FieldViewModel)model);
            };

            //删除已选但不存在的排序字段
            SelectedSorts.DeleteBatch(SelectedSorts.Where(s => baseFields.Count(f => f.fieldname == s.Field) < 1));
        }
        #endregion

        #endregion

        #region 4、命令

        #region 4.1 tab1——表选择
        private ICommand _addJoinTbCmd;
        public ICommand AddJoinTbCmd
        {
            get
            {
                if (_addJoinTbCmd == null)
                {
                    _addJoinTbCmd = new DelegateCommand(AddJoinTableAction);
                }
                return _addJoinTbCmd;
            }
        }

        private void AddJoinTableAction(object obj)
        {
            if (TableMain == null)
            {
                MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.AddJoinTable, "请选择主表");
                return;
            }

            JoinTables.Add(new JoinTable() { JoinType = TableJoinType.Left });

            //MessageWindow.ShowMsg(MessageType.Info, "测试", string.Join(Environment.NewLine, JoinTables.Select(t => t.ToString())));
        }

        private ICommand _delJoinTbCmd;
        public ICommand DelJoinTbCmd
        {
            get
            {
                if (_delJoinTbCmd == null)
                {
                    _delJoinTbCmd = new DelegateCommand(DelJoinTableAction);
                }
                return _delJoinTbCmd;
            }
        }

        private void DelJoinTableAction(object obj)
        {
            var table = obj as JoinTable;
            if (table == null)
            {
                return;
            }
            var rst = MessageWindow.ShowMsg(MessageType.Ask, CustomQueryOptDesc.DelJoinTable, "删除关联表将同时删除对应关联字段，是否继续？");
            if (rst == true)
            {
                RelFields.DeleteBatch(() => { return RelFields.Where(f => f.Table2 == table.Table); });
                JoinTables.Remove(table);
            }

            FilterBaseFieldsSrc();
        }

        private ICommand _addRelFieldCmd;
        public ICommand AddRelFieldCmd
        {
            get
            {
                if (_addRelFieldCmd == null)
                {
                    _addRelFieldCmd = new DelegateCommand(AddRelFieldAction);
                }
                return _addRelFieldCmd;
            }
        }

        private void AddRelFieldAction(object obj)
        {
            var table = obj as JoinTable;
            if (table == null)
            {
                MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.AddRelField, "请先添加关联表");
                return;
            }
            if (table.Table.IsEmpty())
            {
                MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.AddRelField, "请先选择关联表");
                return;
            }

            FilterBaseFieldsSrc();

            RelFields.Add(new RelationField { Table2 = table.Table });
        }

        private ICommand _delRelFieldCmd;
        public ICommand DelRelFieldCmd
        {
            get
            {
                if (_delRelFieldCmd == null)
                {
                    _delRelFieldCmd = new DelegateCommand(DelRelFieldAction);
                }
                return _delRelFieldCmd;
            }
        }

        private void DelRelFieldAction(object obj)
        {
            //MessageWindow.ShowMsg(MessageType.Info, "测试", string.Join(Environment.NewLine, RelFields.Select(f => f.ToString())));
            var field = obj as RelationField;
            if (field == null)
            {
                return;
            }
            RelFields.Remove(field);
        }

        private ICommand _filterRelFieldCmd;
        public ICommand FilterRelFieldCmd
        {
            get
            {
                if (_filterRelFieldCmd == null)
                {
                    _filterRelFieldCmd = new DelegateCommand(FilterRelFieldAction);
                }
                return _filterRelFieldCmd;
            }
        }

        private void FilterRelFieldAction(object obj)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(RelFields);
            var table2 = obj as JoinTable;
            if (table2 == null)
            {
                view.Filter = null;
            }
            else
            {
                view.Filter = model =>
                {
                    return (model as RelationField).Table2 == table2.Table;
                };
            }
        }

        #endregion

        #region 4.2 tab2——查询字段选择
        private ICommand _addSelFieldsCmd;
        public ICommand AddSelFieldsCmd
        {
            get
            {
                if (_addSelFieldsCmd == null)
                {
                    _addSelFieldsCmd = new DelegateCommand(AddSelFieldsAction);
                }
                return _addSelFieldsCmd;
            }
        }

        private void AddSelFieldsAction(object obj)
        {
            var fields = obj as IEnumerable<object>;
            if (fields.IsEmpty())
            {
                return;
            }
            SelectedFields.AddRange(fields.Select(o => (FieldViewModel)o));
            //过滤字段源
            FilterSelFieldsSrc();
        }

        private ICommand _delSelFieldsCmd;
        public ICommand DelSelFieldsCmd
        {
            get
            {
                if (_delSelFieldsCmd == null)
                {
                    _delSelFieldsCmd = new DelegateCommand(DelSelFieldsAction);
                }
                return _delSelFieldsCmd;
            }
        }

        private void DelSelFieldsAction(object obj)
        {
            var fields = obj as IEnumerable<object>;
            if (fields.IsEmpty())
            {
                return;
            }
            SelectedFields.DeleteBatch(fields.Select(o => (FieldViewModel)o));
            FilterSelFieldsSrc();
        }

        private ICommand _addAllSelFieldsCmd;
        public ICommand AddAllSelFieldsCmd
        {
            get
            {
                if (_addAllSelFieldsCmd == null)
                {
                    _addAllSelFieldsCmd = new DelegateCommand(AddAllSelFieldsAction);
                }
                return _addAllSelFieldsCmd;
            }
        }

        private void AddAllSelFieldsAction(object obj)
        {
            var fields = obj as ItemCollection;//这里传递的是ListBox的Items属性
            if (fields.IsEmpty())
            {
                return;
            }
            SelectedFields.AddRange(() => { return fields.Cast<FieldViewModel>().Select(o => o as FieldViewModel); });
            FilterSelFieldsSrc();
        }

        private ICommand _delAllSelFieldsCmd;
        public ICommand DelAllSelFieldsCmd
        {
            get
            {
                if (_delAllSelFieldsCmd == null)
                {
                    _delAllSelFieldsCmd = new DelegateCommand(DelAllSelFieldsAction);
                }
                return _delAllSelFieldsCmd;
            }
        }

        private void DelAllSelFieldsAction(object obj)
        {
            SelectedFields.Clear();
            FilterSelFieldsSrc();
        }

        private ICommand _moveUpSelFieldCmd;
        public ICommand MoveUpSelFieldCmd
        {
            get
            {
                if (_moveUpSelFieldCmd == null)
                {
                    _moveUpSelFieldCmd = new DelegateCommand(MoveUpSelFieldAction);
                }
                return _moveUpSelFieldCmd;
            }
        }

        private void MoveUpSelFieldAction(object obj)
        {
            var cur = obj as FieldViewModel;
            if (SelectedFields.IsEmpty() || cur == null || cur.order == SelectedFields.Min(f => f.order))
            {
                return;
            }
            //上移：将当前order与前一个替换
            var pre = SelectedFields.Where(f => f.order == cur.order - 1).First();
            var temp = pre.order;
            pre.order = cur.order;
            cur.order = temp;

            SortSelectedFields();
        }

        private ICommand _moveDownSelFieldCmd;
        public ICommand MoveDownSelFieldCmd
        {
            get
            {
                if (_moveDownSelFieldCmd == null)
                {
                    _moveDownSelFieldCmd = new DelegateCommand(MoveDownSelFieldAction);
                }
                return _moveDownSelFieldCmd;
            }
        }

        private void MoveDownSelFieldAction(object obj)
        {
            var cur = obj as FieldViewModel;
            if (SelectedFields.IsEmpty() || cur == null || cur.order == SelectedFields.Max(f => f.order))
            {
                return;
            }
            //下移
            var next = SelectedFields.Where(f => f.order == cur.order + 1).First();
            var temp = next.order;
            next.order = cur.order;
            cur.order = temp;

            SortSelectedFields();
        }

        private void SortSelectedFields()
        {
            var view = ViewSelectedFields.View;
            if (view == null)
            {
                return;
            }
            if (view.SortDescriptions.IsEmpty())
            {
                view.SortDescriptions.Add(new SortDescription("order", ListSortDirection.Ascending));
            }
            else
            {
                view.Refresh();
            }
        }
        #endregion

        #region 4.4 tab4——过滤字段选择
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
            SelectedSorts.AddRange(fields.Select(o =>
            {
                var field = o as FieldViewModel;
                return new SortViewModel { Field = field.fieldname, SortType = SortType.Asc, DisplayName = field.displayname };
            }));
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
            var fields = obj as IEnumerable<object>;
            if (fields.IsEmpty())
            {
                return;
            }
            SelectedSorts.DeleteBatch(fields.Select(o => (SortViewModel)o));
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
        #endregion

        #endregion

    }
}
