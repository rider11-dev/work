using DapperExtensions.Mapper;
using OneCardSln.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Card.Mapper
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
