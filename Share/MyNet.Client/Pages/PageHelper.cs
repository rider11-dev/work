using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Client.Pages
{
    public class PageHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageUri">格式：/MyNet.Client;component/Pages/Auth/UserMngPage.xaml</param>
        /// <returns></returns>
        public static Uri GetPageFullUri(string pageUri)
        {
            return new Uri("pack://application:,,," + pageUri, UriKind.Absolute);
        }
    }
}
