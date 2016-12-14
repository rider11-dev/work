using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Client
{
    public class CustomQueryApiKeys
    {
        public const string Key_ApiProvider_CustomQuery = "customquery";
        /*————————————tables——————————*/
        public const string TablePage = "table_page";
        public const string TableAllWithFields = "table_all_with_fields";
        public const string TableAdd = "table_add";
        public const string TableUpdate = "table_update";
        public const string TableDel = "table_del";
        public const string TableDbTables = "table_dbtables";
        public const string TableInit = "table_init";

        /*————————————fields——————————*/
        public const string FieldPage = "field_page";
        public const string FieldAdd = "field_add";
        public const string FieldUpdate = "field_update";
        public const string FieldDel = "field_del";
        public const string FieldDbFields = "field_dbfields";
        public const string FieldInit = "field_init";

        /*————————————query——————————*/
        public const string ExecQuery = "exec_query";
    }
}
