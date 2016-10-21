using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.OneCardClient.Pages
{
    public class PageHelper
    {
        public static Uri GetPageFullUri(string pageUri)
        {
            return new Uri("pack://application:,,,/Pages/" + pageUri, UriKind.Absolute);
        }
    }
}
