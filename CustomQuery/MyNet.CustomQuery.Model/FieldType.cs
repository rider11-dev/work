using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.CustomQuery.Model
{
    public enum FieldType
    {
        [Description("字符串类型")]
        String,
        [Description("数值类型")]
        Number,
        [Description("日期类型")]
        Date,
        [Description("时间类型")]
        Time,
        [Description("布尔类型")]
        Boolean
    }
}
