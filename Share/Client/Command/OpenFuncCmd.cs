using MyNet.Components.Logger;
using MyNet.Components.WPF.Windows;
using MyNet.Client.Pages;
using MyNet.Client.Public;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows;

namespace MyNet.Client.Command
{
    /// <summary>
    /// 打开功能菜单命令
    /// </summary>
    public class OpenFuncCmd : ICommand
    {
        private ILogHelper<OpenFuncCmd> _logHelper = LogHelperFactory.GetLogHelper<OpenFuncCmd>();
        public Frame Container { get; set; }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 记录上次加载完成时绑定的事件委托，用来在下次绑定之前取消，避免多次绑定
        /// </summary>
        static LoadCompletedEventHandler LastEventHandler = null;//
        public void Execute(object parameter)
        {
            var oldSrc = Container.Source;
            Container.Source = null;
            Container.LoadCompleted -= LastEventHandler;
            OpenFuncParam param = (OpenFuncParam)parameter;
            if (param == null)
            {
                _logHelper.LogError("parameter不是有效的OpenFuncParam类型");
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.OpenFunc, MsgConst.Msg_ViewAppLog);
                return;
            }
            if (string.IsNullOrEmpty(param.PageUri))
            {
                _logHelper.LogError("PageUri不能为空");
                MessageWindow.ShowMsg(MessageType.Error, OperationDesc.OpenFunc, MsgConst.Msg_ViewAppLog);
                return;
            }
            //需要在LoadCompleted事件中，才能获取到绑定到Frame的Page实例
            LastEventHandler = (o, e) =>
            {
                try
                {
                    var page = e.Content as BasePage;
                    if (page == null)
                    {
                        throw new Exception("未找到page，请检查uri是否正确或是否继承BasePage");
                    }
                    page.FuncCode = param.FuncCode;
                }
                catch (Exception ex)
                {
                    _logHelper.LogError("Container(Frame).LoadCompleted事件", ex);
                    throw new Exception("打开功能菜单错误", ex);
                }
            };
            Container.LoadCompleted += LastEventHandler;
            if (oldSrc != null && oldSrc.ToString() == param.PageUri.Trim('/'))
            {
                //相同uri，不会重新加载，需要实例化对象
                //“/Biz.PartyBuilding.YS.Client;component/Learn/PartyLearnPage.xaml”
                var strUri = param.PageUri.Trim('/');
                var start = strUri.LastIndexOf("/") + 1;
                var len = strUri.LastIndexOf(".xaml") - start;
                var className = strUri.Substring(start, len);
                var assName = strUri.Substring(0, strUri.IndexOf(';'));
                try
                {
                    var ass = AppDomain.CurrentDomain.GetAssemblies().Where(a => a.GetName().Name == assName).FirstOrDefault();
                    Type type = ass.GetTypes().Where(t => t.Name == className).FirstOrDefault();
                    BasePage page = Activator.CreateInstance(type) as BasePage;
                    Container.Navigate(page);
                }
                catch (Exception ex)
                {
                    _logHelper.LogError("OpenFuncCmd.Execute——相同uri Page实例化失败，uri:" + param.PageUri, ex);
                    throw new Exception("打开功能菜单失败，实例化page失败，请检查uri是否正确");
                }
            }
            else
            {
                Container.Navigate(PageHelper.GetPageFullUri(param.PageUri));
            }
        }


    }
}
