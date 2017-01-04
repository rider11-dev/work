using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.ViewModel
{
    public interface ITableVM
    {
        string id { get; set; }
        string tbname { get; set; }
        string alias { get; set; }
        string comment { get; set; }
    }
}
