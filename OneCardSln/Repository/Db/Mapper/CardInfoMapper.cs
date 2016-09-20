using DapperExtensions.Mapper;
using OneCardSln.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Db.Mapper
{
    public class CardInfoMapper : ClassMapper<CardInfo>
    {
        const string _tbName = "card_info";
        public CardInfoMapper()
        {
            Table(_tbName);

            //Map(t => t.id).Key(KeyType.Assigned);

            AutoMap();
        }
    }
}
