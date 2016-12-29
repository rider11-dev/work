using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Model.Interface.Base
{
    /// <summary>
    /// 字典类型
    /// </summary>
    public interface IDictType
    {
        string type_code { get; set; }
        string type_name { get; set; }
        bool type_system { get; set; }
    }
}
