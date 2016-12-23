using DapperExtensions.Mapper;
using MyNet.Model.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service.Base.Mapper
{
    public class DictDtoMapper : ClassMapper<DictCmbDto>
    {
        public DictDtoMapper()
        {
            Table("base_dict");

            Map(m => m.Id).Column("dict_code");
            Map(m => m.Text).Column("dict_name");
            Map(m => m.Type).Column("dict_type");
            Map(m => m.Order).Column("dict_order");
            Map(m => m.IsDefault).Column("dict_default");

            AutoMap();
        }
    }
}
