using MyNet.Components.Extensions;
using MyNet.Components.WPF.Models;
using MyNet.CustomQuery.Model;
using MyNet.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Client.Models.ExecQuery
{
    public class DataSourceInitalizer
    {
        private ExecQueryModel ExecQModel;
        public DataSourceInitalizer(ExecQueryModel execQModel)
        {
            ExecQModel = execQModel;
        }

        public void Init()
        {
            var tables = TableMngViewModel.GetTables(new PageQuery { pageIndex = 1, pageSize = 1000 });
            ExecQModel.Tables = new ObservableCollection<TableViewModel>(tables);
            tables = null;

            var fields = FieldMngViewModel.GetFields(new PageQuery { pageIndex = 1, pageSize = 1000 });
            var idx = 0;
            fields.ToList().ForEach(f => f.order = idx++);//设置字段初始顺序
            ExecQModel.Fields = new ObservableCollection<FieldViewModel>(fields);
            fields = null;

            ExecQModel.JoinTypes = new ObservableCollection<CmbItem>(
                EnumExtension.ConvertEnumToDict<TableJoinType>()
                .Select(kvp => new CmbItem { Id = kvp.Key, Text = kvp.Value }));
        }
    }
}
