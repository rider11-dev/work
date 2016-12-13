using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.CustomQuery.Model;
using MyNet.Model;
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

namespace MyNet.CustomQuery.Client.Models
{
    public class ExecQueryModel : BaseModel
    {
        public ExecQueryModel()
        {
            JoinTables = new ObservableCollection<JoinTable>();
            RelFields = new ObservableCollection<RelationField>();
            SelectedFields = new ObservableCollection<FieldViewModel>();
            Conditions = new ObservableCollection<Condition>();
            Sorts = new ObservableCollection<Sort>();

            InitDataSources();
        }

        #region 初始化
        private void InitDataSources()
        {
            var tables = TableMngViewModel.GetTables(new PageQuery { pageIndex = 1, pageSize = 1000 });
            Tables = new ObservableCollection<TableViewModel>(tables);
            tables = null;

            var fields = FieldMngViewModel.GetFields(new PageQuery { pageIndex = 1, pageSize = 1000 });
            Fields = new ObservableCollection<FieldViewModel>(fields);
            fields = null;

            JoinTypes = new ObservableCollection<CmbItem>(
                EnumExtension.ConvertEnumToDict<TableJoinType>()
                .Select(kvp => new CmbItem { Id = kvp.Key, Text = kvp.Value }));
        }
        #endregion

        #region 查询模板数据区
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
        //过滤条件字段
        public ObservableCollection<Condition> Conditions { get; set; }
        //排序字段
        public ObservableCollection<Sort> Sorts { get; set; }
        #endregion

        #region 数据源
        public ObservableCollection<TableViewModel> Tables { get; set; }

        public ObservableCollection<CmbItem> JoinTypes { get; set; }

        public ObservableCollection<FieldViewModel> Fields { get; set; }
        public CollectionViewSource ViewSrcFields { get; set; }
        public CollectionViewSource ViewSrcSelFields { get; set; }

        #endregion

        #region 命令

        #region tab1——表选择
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

            JoinTables.Add(new JoinTable { JoinType = TableJoinType.Left });

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

            FilterFields();
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

            FilterFields();

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

        #region tab2——查询字段选择
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
        #endregion

        #endregion

        #region 外部方法

        public void FilterFields()
        {
            ICollectionView view = ViewSrcFields.View;
            if (view == null)
            {
                return;
            }
            var fields = GetFieldsBySelectedTable();
            if (fields.IsEmpty())
            {
                view.Filter = model => { return 1 == 0; };
            }
            else
            {
                view.Filter = model => { return fields.Contains((FieldViewModel)model); };
            }
            //基础字段发生变化后，已选的查询结果字段、过滤、排序都得更新
            SelectedFields.DeleteBatch(SelectedFields.Where(sel => !fields.Contains(sel)));//删除SelectedFields中存在而fields中不存在的
            Conditions.DeleteBatch(Conditions.Where(c => fields.Count(f => f.fieldname == c.Field) < 1));
            Sorts.DeleteBatch(Sorts.Where(s => fields.Count(f => f.fieldname == s.Field) < 1));
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
        public void FilterSelFieldsSrc()
        {
            ICollectionView view = ViewSrcSelFields.View;
            if (view == null)
            {
                return;
            }

            var baseFields = GetFieldsBySelectedTable();
            if (baseFields.IsEmpty())
            {
                view.Filter = model => { return 1 == 0; };
                return;
            }
            var leftFields = baseFields.Except(SelectedFields);
            view.Filter = model =>
            {
                return leftFields.Contains((FieldViewModel)model);//获取差集
            };

        }
        #endregion
    }
}
