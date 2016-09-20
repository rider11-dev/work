using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Model
{
    /// <summary>
    /// 操作员
    /// </summary>
    public class CardOperator
    {
        public String id{ get;set; }
        public String realname{ get;set; }
        public String idcard{ get;set; }
        public String username{ get;set; }
        public String password{ get;set; }
        public Int32 type{ get;set; }
    }
}