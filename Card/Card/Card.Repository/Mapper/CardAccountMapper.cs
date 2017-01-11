using Card.Model;
using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Repository.Mapper
{
    public class CardAccountMapper : ClassMapper<CardAccount>
    {
        public CardAccountMapper()
        {
            Table("card_info");

            AutoMap();
        }
    }
}
