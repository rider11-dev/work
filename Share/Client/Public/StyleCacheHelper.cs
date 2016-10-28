using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyNet.Client.Public
{
    public class StyleCacheHelper
    {
        static Style _mngBtnStyle;
        public static Style MngBtnStyle
        {
            get
            {
                if (_mngBtnStyle == null)
                {
                    _mngBtnStyle = Application.Current.FindResource("mngBtnStyle") as Style;
                }
                return _mngBtnStyle;
            }
        }
    }
}
