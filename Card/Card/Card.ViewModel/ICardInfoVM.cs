using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.ViewModel
{
    public interface ICardInfoVM
    {
        string id { get; set; }
        string idcard { get; set; }
        string number { get; set; }
        string state { get; set; }
        DateTime issuetime { get; set; }
    }
}
