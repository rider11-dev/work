using DapperExtensions.Mapper;
using MyNet.Model.CustomQuery;

namespace MyNet.Repository.CustomQuery.Mapper
{
    public class FieldMapper : ClassMapper<Field>
    {
        public FieldMapper()
        {
            Table("query_fields");

            AutoMap();
        }
    }
}
