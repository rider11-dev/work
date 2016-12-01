using MyNet.Model.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Service.Card.Models
{
    public class SetMoneySingleProcessParam : SingleProcessParam
    {
        public MoneyEnum moneyType { get; set; }

        public decimal money { get; set; }
    }
}
