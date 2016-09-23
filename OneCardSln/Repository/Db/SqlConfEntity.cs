using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Repository.Db
{
    public class SqlConfEntity
    {
        /// <summary>
        /// sql区域节点
        /// </summary>
        public string area { get; set; }
        /// <summary>
        /// sql组节点
        /// </summary>
        public string group { get; set; }

        /// <summary>
        /// sql名称
        /// </summary>
        public string name { get; set; }

        public bool Check()
        {
            return !string.IsNullOrEmpty(area) && !string.IsNullOrEmpty(group) && !string.IsNullOrEmpty(name);
        }
    }
}
