using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.ViewModel.CustomQuery
{
    public interface IFieldVM
    {
        string id { get; set; }
        string tbid { get; set; }
        string fieldname { get; set; }
        string fieldtype { get; set; }
        string remark { get; set; }
        bool visible { get; set; }
    }
}
