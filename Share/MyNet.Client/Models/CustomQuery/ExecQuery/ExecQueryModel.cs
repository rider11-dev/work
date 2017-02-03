using MyNet.Client.Public;
using MyNet.Components;
using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using MyNet.Components.Result;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Controls;
using MyNet.Components.WPF.Extension;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.Model.CustomQuery;
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
using MyNet.Client.Models.CustomQuery;

namespace MyNet.Client.Models.CustomQuery.ExecQuery
{
    public class ExecQueryModel : BaseModel
    {
        public QueryExecutor QueryExecutor { get; private set; }
        public TableSelector TableSelector { get; private set; }
        public SelFieldsSelector SelFieldsSelector { get; private set; }
        public SortFieldsSelector SortFieldsSelector { get; private set; }
        public FilterFieldsSelector FilterFieldsSelector { get; private set; }
        public ExecQueryModel()
        {
            JoinTables = new ObservableCollection<JoinTable>();
            RelFields = new ObservableCollection<RelationField>();
            SelectedFields = new ObservableCollection<FieldViewModel>();
            SelectedConditions = new ObservableCollection<ConditionViewModel>();
            SelectedSorts = new ObservableCollection<SortViewModel>();

            QueryExecutor = new QueryExecutor(this);
            TableSelector = new TableSelector(this);
            SelFieldsSelector = new SelFieldsSelector(this);
            SortFieldsSelector = new SortFieldsSelector(this);
            FilterFieldsSelector = new FilterFieldsSelector(this);

            InitDataSource();
        }


        #region 查询模板数据
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
        //已选主表
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
        //已选关联表
        public ObservableCollection<JoinTable> JoinTables { get; set; }
        //已选关联字段
        public ObservableCollection<RelationField> RelFields { get; set; }
        //已选查询结果字段
        public ObservableCollection<FieldViewModel> SelectedFields { get; set; }
        //已选过滤条件字段
        public ObservableCollection<ConditionViewModel> SelectedConditions { get; set; }
        //已选排序字段
        public ObservableCollection<SortViewModel> SelectedSorts { get; set; }
        #endregion

        #region 数据源
        //查询表数据源
        public ObservableCollection<TableViewModel> Tables { get; set; }

        //字段数据源
        public ObservableCollection<FieldViewModel> Fields { get; set; }

        //基础可选字段视图数据源（根据主表和关联表过滤）
        public CollectionViewSource ViewSrcBaseFields { get; set; }

        //基础字段源过滤
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
            SelFieldsSelector.FilterSelFieldsSrc(baseFields);
            FilterFieldsSelector.FilterFilterFieldsSrc(baseFields);
            SortFieldsSelector.FilterSortFieldsSrc(baseFields);
        }

        //根据查询表（包含主表和关联表）获取所有查询字段
        public IEnumerable<FieldViewModel> GetFieldsBySelectedTable()
        {
            if (TableMain == null && JoinTables.IsEmpty())
            {
                return null;
            }
            return Fields.Where(field =>
                            (JoinTables.IsNotEmpty() && JoinTables.Select(t => t.Table).Contains(field.tbname)) ||
                            (TableMain != null && field.tbname == TableMain.tbname));
        }

        //初始化数据源
        public void InitDataSource()
        {
            var tables = TableMngViewModel.GetTables(new PageQuery { pageIndex = 1, pageSize = 1000 });
            Tables = new ObservableCollection<TableViewModel>(tables);
            tables = null;

            var fields = FieldMngViewModel.GetFields(new PageQuery { pageIndex = 1, pageSize = 1000 });
            Fields = new ObservableCollection<FieldViewModel>(fields);
            fields = null;

        }
        #endregion

    }
}
