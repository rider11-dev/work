using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneCardSln.Components.Extensions;
using OneCardSln.Model.Auth;

namespace OneCardSln.Repository.Auth
{
    public class PermTypeRepository
    {
        static Dictionary<string, string> _dataSrc;

        public Dictionary<string, string> DataSrc
        {
            get
            {
                if (_dataSrc == null)
                {
                    _dataSrc = EnumExtension.ToDict<PermType>();
                }

                return _dataSrc;
            }
        }
    }
}
