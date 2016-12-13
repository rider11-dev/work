using MyNet.Model.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyNet.Components.Extensions;

namespace MyNet.Repository.Card
{
    public class CardOperationRepository
    {
        static Dictionary<string, string> _dataSrc;

        public Dictionary<string, string> DataSrc
        {
            get
            {
                if (_dataSrc == null)
                {
                    _dataSrc = EnumExtension.ConvertEnumToDict<CardOperation>();
                }

                return _dataSrc;
            }
        }
    }
}
