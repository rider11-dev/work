using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Components.Extensions
{
    public class RegexExtension
    {
        public const string Regex_Idcard = @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$";
        public const string Regex_Sort = @"^\d{0,10}$";
        public const string Regex_Pwd = @"^(\d|[a-zA-Z]|_){6,10}$";
        public const string Regex_CellPhone = @"^\d{11}$";
    }
}
