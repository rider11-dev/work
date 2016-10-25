using DapperExtensions.Mapper;
using MyNet.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Base.Mapper
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
