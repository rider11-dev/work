using Biz.PartyBuilding.YS.Models;
using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.PartyBuilding.YS.Repository.Mapper
{
    public class InfoModelMapper : ClassMapper<InfoModel>
    {
        public InfoModelMapper()
        {
            Table("party_info");

            AutoMap();
        }
    }
}
