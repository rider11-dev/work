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
                    _dataSrc = GetSrc();
                }

                return _dataSrc;
            }
        }

        static Dictionary<string, string> GetSrc()
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            var arr = Enum.GetValues(typeof(PermType));
            foreach (var item in arr)
            {
                dict.Add(item.ToString(), item.ToString().ToEnum<PermType>().GetDescription());
            }

            return dict;
        }
    }
}
