using MyNet.Client.Public;
using MyNet.Components.Extensions;
using MyNet.Components.WPF.Command;
using MyNet.Components.WPF.Models;
using MyNet.Components.WPF.Windows;
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
    public class TableSelector
    {
        private ExecQueryModel QModel;
        public CollectionViewSource ViewRelFields { get; set; }
        //表连接方式数据源
        public ObservableCollection<CmbItem> JoinTypes { get; set; }
        public TableSelector(ExecQueryModel qModel)
        {
            QModel = qModel;
            JoinTypes = new ObservableCollection<CmbItem>(DataCacheUtils.GetEnumCmbSource<TableJoinType>());
        }
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
            if (QModel.TableMain == null)
            {
                MessageWindow.ShowMsg(MessageType.Info, CustomQueryOptDesc.AddJoinTable, "请选择主表");
                return;
            }

            QModel.JoinTables.Add(new JoinTable() { JoinType = TableJoinType.Left });

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
                QModel.RelFields.DeleteBatch(() => { return QModel.RelFields.Where(f => f.Table2 == table.Table); });
                QModel.JoinTables.Remove(table);
            }

            QModel.FilterBaseFieldsSrc();
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

            QModel.FilterBaseFieldsSrc();

            QModel.RelFields.Add(new RelationField { Table2 = table.Table });
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
            QModel.RelFields.Remove(field);
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
            ICollectionView view = ViewRelFields.View;
            var table2 = obj as JoinTable;
            if (table2 == null)
            {
                view.Filter = model => { return 1 == 0; };
            }
            else
            {
                view.Filter = model =>
                {
                    return (model as RelationField).Table2 == table2.Table;
                };
            }
        }
    }
}
