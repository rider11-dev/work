using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Serialize
{
    /// <summary>
    /// 从当前应用程序域中查找目标类型
    /// http://blog.csdn.net/w21fanfan1314/article/details/8280582
    /// </summary>
    public class DomainSerializationBinder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            var ass = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName == assemblyName).FirstOrDefault();
            if (ass == null)
            {
                return null;
            }
            var type = ass.GetType(typeName);
            return type;
        }
    }
}
