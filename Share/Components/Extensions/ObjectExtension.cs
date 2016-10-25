using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Extensions
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 获取对象的DescriptionAttribute值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this object value)
        {
            string str = value.ToString();
            var field = value.GetType().GetField(str);
            object[] attrs = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attrs == null || attrs.Length < 1)
            {
                return str;
            }
            var da = (DescriptionAttribute)attrs[0];
            if (da == null)
            {
                return str;
            }
            return da.Description;
        }

        /// <summary>
        /// 判断是否为空字符串
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool IsEmpty(this object val)
        {
            if (val == null)
            {
                return true;
            }
            return string.IsNullOrWhiteSpace(val.ToString());
        }

        public static bool IsNotEmpty(this object val)
        {
            return !IsEmpty(val);
        }
    }
}
