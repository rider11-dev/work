using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string remark { get; set; }
        public bool visible { get; set; }
    }

    public enum FieldType
    {
        [Description("字符串")]
        String,
        [Description("数值")]
        Number,
        [Description("日期")]
        Date,
        [Description("时间")]
        Time,
        [Description("布尔")]
        Boolean
    }
}
