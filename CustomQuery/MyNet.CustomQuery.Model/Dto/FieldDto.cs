using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model.Dto
{
    public class FieldDto : Field
    {
        public string tbname { get; set; }
        public string fieldtype_name { get; set; }
    }
}
