using DapperExtensions.Mapper;
using OneCardSln.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Card.Mapper
{
    public class CardBillMapper : ClassMapper<CardBill>
    {
        public CardBillMapper()
        {
            Table("card_bill");

            AutoMap();
        }
    }
}
