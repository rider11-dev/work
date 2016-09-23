using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Components.Extensions
{
    public class DynamicExtension
    {
        /// <summary>
        /// 构建动态对象
        /// </summary>
        /// <param name="setter"></param>
        /// <returns></returns>
        public static ExpandoObject BuildDynamicObject(Action<dynamic> setter)
        {
            ExpandoObject obj = new ExpandoObject();
            if (setter == null)
            {
                return obj;
            }
            setter(obj);

            return obj;
        }
    }
}
