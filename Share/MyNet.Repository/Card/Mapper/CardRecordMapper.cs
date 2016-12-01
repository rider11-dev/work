using DapperExtensions.Mapper;
using MyNet.Model.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Repository.Card.Mapper
{
    public class CardRecordMapper:ClassMapper<CardRecord>
    {
        public CardRecordMapper()
        {
            Table("card_record");

            AutoMap();
        }
    }
}
