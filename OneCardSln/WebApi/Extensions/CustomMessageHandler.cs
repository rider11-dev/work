using OneCardSln.Components.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace OneCardSln.WebApi.Extensions
{
    /// <summary>
    /// 消息处理程序：记录每次请求、响应内容
    /// </summary>
    public class CustomMessageHandler : DelegatingHandler
    {
        ILogHelper<CustomMessageHandler> _logHelper = LogHelperFactory.GetLogHelper<CustomMessageHandler>();
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Content != null)
            {
                string msg = string.Format("本次请求:{0}\tUrl:{1}{0}\tMethod:{2}{0}\tContent：{3}",
                    Environment.NewLine,
                    request.RequestUri.ToString(),
                    request.Method.ToString(),
                    request.Content.ReadAsStringAsync().Result);
                _logHelper.LogInfo(msg);
            }

            return base.SendAsync(request, cancellationToken).ContinueWith<HttpResponseMessage>(
                    (task) =>
                    {
                        _logHelper.LogInfo(string.Format("本次响应Content：{0}", task.Result.Content.ReadAsStringAsync().Result));
                        return task.Result;
                    }
                );
        }
    }
}