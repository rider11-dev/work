using System.Windows.Controls;

namespace MyNet.Components.WPF.Extension
{
    public static class WebBrowserExtension
    {
        /// <summary>
        /// 屏蔽页面错误
        /// </summary>
        /// <param name="browser"></param>
        /// <param name="hide"></param>
        public static void SuppressScriptErrors(this WebBrowser browser, bool hide = true)
        {
            var fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (fiComWebBrowser == null)
                return;

            object objComWebBrowser = fiComWebBrowser.GetValue(browser);
            if (objComWebBrowser == null)
                return;

            objComWebBrowser.GetType().InvokeMember("Silent", System.Reflection.BindingFlags.SetProperty, null, objComWebBrowser, new object[] { hide });
        }
        #region 以下功能，需要引用Microsoft.mshtml.dll
        ///// <summary>
        ///// 只在当前窗口打开页面
        ///// </summary>
        ///// <param name="browser"></param>
        //public static void OpenInSelfWindowOnly(this WebBrowser browser)
        //{
        //    browser.LoadCompleted -= WebBrowser_LoadCompleted;
        //    browser.LoadCompleted += WebBrowser_LoadCompleted;
        //    browser.Navigated -= WebBrowser_Navigated;
        //    browser.Navigated += WebBrowser_Navigated;
        //}

        //private static void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        //{
        //    var browser = sender as WebBrowser;
        //    ReplaceLinkTarget((HTMLDocumentClass)browser.Document);
        //}

        //private static void ReplaceLinkTarget(IHTMLDocument2 doc, bool inIframe = false)
        //{
        //    if (doc == null)
        //    {
        //        return;
        //    }
        //    //将所有链接的目标，指向本窗体
        //    if (doc.links.IsNotEmpty())
        //    {
        //        foreach (IHTMLElement link in doc.links)
        //        {
        //            link.setAttribute("target", inIframe ? "_top" : "_self");
        //        }
        //    }
        //    //将所有Form的提交目标，指向本窗体
        //    if (doc.forms.IsNotEmpty())
        //    {
        //        foreach (IHTMLElement form in doc.forms)
        //        {
        //            form.setAttribute("target", inIframe ? "_top" : "_self");
        //        }
        //    }
        //    //处理所有iframe内部的超链接
        //    if (doc.frames.IsNotEmpty())
        //    {
        //        object j;
        //        for (int i = 0; i < doc.frames.length; i++)
        //        {
        //            j = i;
        //            HTMLWindow2Class frame = doc.frames.item(ref j) as HTMLWindow2Class;
        //            ReplaceLinkTarget(frame.document, true);
        //        }
        //    }
        //}

        //private static void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        //{
        //    var browser = sender as WebBrowser;

        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("function alert(){return ;}");
        //    sb.AppendLine("function confirm(){return ;}");
        //    sb.AppendLine("function showModalDialog(){return ;}");
        //    sb.AppendLine("function prompt(){return ;}");
        //    sb.AppendLine("function window.open(){return ;}");//屏蔽window.open
        //    string strJs = sb.ToString();
        //    IHTMLWindow2 win = (IHTMLWindow2)(browser.Document as IHTMLDocument2).parentWindow;
        //    if (win == null)
        //    {
        //        return;
        //    }
        //    win.execScript(strJs, "JavaScript");
        //    win = null;
        //}
        #endregion
    }
}
