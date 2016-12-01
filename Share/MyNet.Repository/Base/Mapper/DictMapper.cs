using DapperExtensions.Mapper;
using MyNet.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Base.Mapper
{
    public class DictMapper:ClassMapper<Dict>
    {
        public DictMapper()
        {
            Table("base_dict");

            AutoMap();
        }
    }
}
