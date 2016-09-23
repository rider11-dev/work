using DapperExtensions.Mapper;
using OneCardSln.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Base.Mapper
{
    public class DictTypeMapper : ClassMapper<DictType>
    {
        public DictTypeMapper()
        {
            Table("base_dict_type");

            AutoMap();
        }
    }
}
