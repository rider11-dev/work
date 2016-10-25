using DapperExtensions.Mapper;
using MyNet.Model.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Card.Mapper
{
    public class CardInfoMapper : ClassMapper<CardInfo>
    {
        public CardInfoMapper()
        {
            Table("card_info");

            Map(c => c.State).Ignore();

            AutoMap();
        }
    }
}
