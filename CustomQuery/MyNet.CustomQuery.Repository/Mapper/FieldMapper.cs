using DapperExtensions.Mapper;
using MyNet.CustomQuery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Repository.Mapper
{
    public class FieldMapper : ClassMapper<Field>
    {
        public FieldMapper()
        {
            Table("query_fields");
            Map(f => f.FieldType).Ignore();

            AutoMap();
        }
    }
}
