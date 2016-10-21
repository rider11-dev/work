using MyNet.Components.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyNet.WebApi.Extensions
{
    /// <summary>
    /// 消息处理程序：记录每次请求、响应内容
    /// </summary>
    public class CustomMessageHandler : DelegatingHandler
    {
        ILogHelper<CustomMessageHandler> _logHelper = LogHelperFactory.GetLogHelper<CustomMessageHandler>();
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //异步写日志
            Task.Run(() =>
            {
                string msg = string.Format("{0}本次请求:{0}{1}{0}", Environment.NewLine, request.ToString());

                if (request.Content != null)
                {
                    msg += string.Format("Content:{0}\t{1}", Environment.NewLine, request.Content.ReadAsStringAsync().Result);
                }
                
                _logHelper.LogInfo(msg);
            });

            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(
                    (task) =>
                    {
                        _logHelper.LogInfo(string.Format("本次响应：{0}Content：{1}", Environment.NewLine, task.Result.Content.ReadAsStringAsync().Result));
                        return task.Result;
                    }
                );
        }
    }
}