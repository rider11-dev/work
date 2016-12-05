using DapperExtensions.Mapper;
using MyNet.CustomQuery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Repository.Mapper
{
    public class TableMapper : ClassMapper<Table>
    {
        public TableMapper()
        {
            Table("query_tables");
            Map(t => t.fields).Ignore();

            AutoMap();
        }
    }
}
