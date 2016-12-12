using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
using MyNet.CustomQuery.Model;
using MyNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyNet.CustomQuery.Client.Models
{
    public class ExecQueryModel : BaseModel
    {
        public ExecQueryModel()
        {
            JoinTables = new List<JoinTable>();
            RelFields = new List<RelationField>();
        }

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

        CmbItem _table_main;
        public CmbItem Table_Main
        {
            get { return _table_main; }
            set
            {
                if (_table_main != value)
                {
                    _table_main = value;
                    base.RaisePropertyChanged("Table_Main");
                }
            }
        }

        private IList<JoinTable> _joinTables;
        public IList<JoinTable> JoinTables
        {
            get { return _joinTables; }
            set
            {
                if (_joinTables != value)
                {
                    _joinTables = value;
                    base.RaisePropertyChanged("JoinTables");
                }
            }
        }

        private IList<RelationField> _relFields;
        public IList<RelationField> RelFields
        {
            get { return _relFields; }
            set
            {
                if (_relFields != value)
                {
                    _relFields = value;
                    base.RaisePropertyChanged("RelFields");
                }
            }
        }
        #endregion

        #region 数据源
        private IEnumerable<TableViewModel> _tables;
        public IEnumerable<TableViewModel> Tables
        {
            get { return _tables; }
            set
            {
                if (_tables != value)
                {
                    _tables = value;
                    base.RaisePropertyChanged("Tables");
                }
            }
        }

        private IEnumerable<CmbItem> _joinTypes;
        public IEnumerable<CmbItem> JoinTypes
        {
            get { return _joinTypes; }
            set
            {
                if (_joinTypes != value)
                {
                    _joinTypes = value;
                    base.RaisePropertyChanged("JoinTypes");
                }
            }
        }

        private IEnumerable<FieldViewModel> _fields;
        public IEnumerable<FieldViewModel> Fields
        {
            get { return _fields; }
            set
            {
                if (_fields != value)
                {
                    _fields = value;
                    base.RaisePropertyChanged("Fields");
                }
            }
        }

        public void InitDataSources()
        {
            Tables = TableMngViewModel.GetTables(new PageQuery { pageIndex = 1, pageSize = 1000 });
            Fields = FieldMngViewModel.GetFields(new PageQuery { pageIndex = 1, pageSize = 1000 });
            JoinTypes = EnumExtension.ToDict<TableJoinType>().Select(kvp => new CmbItem { Id = kvp.Key, Text = kvp.Value });
        }
        #endregion

        #region 命令
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
            JoinTables.Add(new JoinTable { });
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

        }
        #endregion
    }
}
