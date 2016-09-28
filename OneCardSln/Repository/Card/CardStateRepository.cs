using OneCardSln.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneCardSln.Components.Extensions;

namespace OneCardSln.Repository.Card
{
    public class CardStateRepository
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
            var arr = Enum.GetValues(typeof(CardState));
            foreach (var item in arr)
            {
                dict.Add(item.ToString(), item.ToString().ToEnum<CardState>().GetDescription());
            }

            return dict;
        }

    }
}
