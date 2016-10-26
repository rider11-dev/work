using MyNet.ClientFrame.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MyNet.Components.Extensions;
using MyNet.Model.Auth;
using System.Windows;
using System.Windows.Input;

namespace MyNet.ClientFrame.Pages
{
    public abstract class BasePage : Page
    {
        protected Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();
        public string FuncId { get; set; }
        public BasePage()
            : base()
        {

        }

        protected void LoadButtons(Panel container, Style btnStyle = null)
        {
            if (container == null)
            {
                return;
            }
            var opts = Context.Pers.Where(p => p.per_parent == FuncId && p.PermType == PermType.PermTypeOpt);
            if (opts.Count() <= 0)
            {
                return;
            }
            Button btn = null;
            foreach (var opt in opts)
            {
                btn = new Button();
                btn.Name = opt.per_id;
                btn.Content = opt.per_name;
                btn.Style = btnStyle;
                if (Commands != null && Commands.Count > 0 && Commands.ContainsKey(opt.per_method))
                {
                    btn.Command = Commands[opt.per_method];
                }
                container.Children.Add(btn);
            }
        }
    }
}
