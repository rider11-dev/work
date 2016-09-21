using DapperExtensions.Mapper;
using OneCardSln.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Test
{
    public class CardInfoMapper : ClassMapper<CardInfo>
    {
        public CardInfoMapper()
        {
            Table("card_info");

            AutoMap();
        }

    }
}
