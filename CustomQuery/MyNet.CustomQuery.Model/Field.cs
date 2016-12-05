using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    public class Field
    {
        public string id { get; set; }
        public string tbid { get; set; }
        public string fieldname { get; set; }
        public string displayname { get; set; }
        public string fieldtype { get; set; }
        public FieldType FieldType
        {
            get
            {
                if (fieldtype.IsEmpty())
                {
                    return FieldType.String;
                }
                return fieldtype.ToEnum<FieldType>();
            }
        }
        public string remark { get; set; }
    }
}
