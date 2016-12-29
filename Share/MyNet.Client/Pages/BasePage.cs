using MyNet.Client.Public;
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
using MyNet.Model.Interface.Auth;

namespace MyNet.Client.Pages
{
    public abstract class BasePage : Page
    {
        protected Dictionary<string, ICommand> Commands = new Dictionary<string, ICommand>();
        public string FuncCode { get; set; }
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
            var opts = ClientContext.Pers.Where(p => p.per_parent == FuncCode && p.PermType == PermType.Opt);
            if (opts.Count() <= 0)
            {
                return;
            }
            Button btn = null;
            foreach (var opt in opts)
            {
                btn = new Button();
                btn.Uid = opt.per_id;
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
