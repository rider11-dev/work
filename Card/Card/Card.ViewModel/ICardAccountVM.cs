using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.ViewModel
{
    public interface ICardAccountVM : ICopyable
    {
        string id { get; set; }
        string number { get; set; }
        string username { get; set; }
        string idcard { get; set; }
        decimal govmoney { get; set; }
        decimal mymoney { get; set; }
        string state { get; set; }
        string @operator { get; set; }
        string phone { get; set; }
        string remark { get; set; }
    }
}
