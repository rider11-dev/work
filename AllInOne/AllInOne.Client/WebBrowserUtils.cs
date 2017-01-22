using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOne.Client
{
    public static class WebBrowserUtils
    {
        public static void Init()
        {
            if (!Cef.IsInitialized)
            {
                var settings = new CefSettings();
                //启用flash插件
                settings.CefCommandLineArgs["enable-system-flash"] = "1";
                settings.CefCommandLineArgs.Add("ppapi-flash-path", "pepflashplayer.dll");

                Cef.Initialize(settings, true, null);
            }
        }

        public static void SetOpenInSelfWindowOnly(this ChromiumWebBrowser webBrowser)
        {
            webBrowser.FrameLoadEnd -= WebBrowser_FrameLoadEnd;
            webBrowser.FrameLoadEnd += WebBrowser_FrameLoadEnd;
        }

        private static void WebBrowser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            ReplaceTargetOfLinks(sender as ChromiumWebBrowser);
        }

        private static void ReplaceTargetOfLinks(ChromiumWebBrowser webBrowser)
        {
            if (webBrowser == null)
            {
                return;
            }
            //修改所有超链接，在当前页打开
            var script = @"function replaceTargetOfLinks(doc) {
                                if (doc == null) {
                                    return;
                                }
                                var links = doc.getElementsByTagName('a');
                                for (var idx = 0; idx < links.length; idx++) {
                                    links[idx].target = '_top';
                                }
                                var frames = doc.getElementsByTagName('iframe');
                                for (var idx = 0; idx < frames.length; idx++) {
                                    replaceTargetOfLinks(frames[idx].contentDocument);
                                }
                            }
                            replaceTargetOfLinks(document);";
            webBrowser.ExecuteScriptAsync(script);
        }
    }
}
