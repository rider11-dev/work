using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Extensions
{
    public static class EnumExtension
    {
        public static TEnum ToEnum<TEnum>(this string str)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), str);

        }

        public static Dictionary<string, string> ConvertEnumToDict<TEnum>()
        {
            if (!typeof(TEnum).IsEnum)
            {
                return null;
            }
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var arr = Enum.GetValues(typeof(TEnum));
            foreach (var item in arr)
            {
                dict.Add(item.ToString(), item.GetDescription());
            }
            return dict;
        }
    }
}
