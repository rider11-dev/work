using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyNet.Client.Public
{
    public class NotifyHelper
    {
        static TaskbarNotifier notifier = new TaskbarNotifier(null, null);

        public static void Notify(string title, string content)
        {
            notifier.titleLb.Content = title;
            notifier.contentTxt.Text = content;

            if (notifier.Visibility != Visibility.Visible)
            {
                notifier.Show();
            }
        }
    }
}
