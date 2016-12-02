using MyNet.Components.Logger;
using MyNet.Components.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyNet.WebHostService
{
    /// <summary>
    /// 消息处理程序：记录每次请求、响应内容
    /// </summary>
    public class CustomMessageHandler : DelegatingHandler
    {
        ILogHelper<CustomMessageHandler> _logHelper = LogHelperFactory.GetLogHelper<CustomMessageHandler>();
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (HostContext.IsDebug)
            {
                //异步写日志——记录请求数据
                Task.Run(() =>
                {
                    string msg = string.Format("{0}本次请求:{0}{1}{0}", Environment.NewLine, request.ToString());

                    if (request.Content != null)
                    {
                        msg += string.Format("Content:{0}\t{1}", Environment.NewLine, request.Content.ReadAsStringAsync().Result);
                    }

                    _logHelper.LogInfo(msg);
                });
            }

            return base.SendAsync(request, cancellationToken)
                     .ContinueWith(
                         (task) =>
                         {
                             //debug模式或异常时，都记录日志
                             if (HostContext.IsDebug || !task.Result.IsSuccessStatusCode)
                             {
                                 _logHelper.LogInfo(string.Format("本次响应：{0}Content：{1}", Environment.NewLine, task.Result.Content.ReadAsStringAsync().Result));
                             }
                             //如果异常了，重新设置task.Result.Content，屏蔽异常详细信息
                             if (!task.Result.IsSuccessStatusCode)
                             {
                                 task.Result.Content = new ObjectContent<object>(new OptResult
                                 {
                                     code = ResultCode.Fail,
                                     msg = ((int)task.Result.StatusCode).ToString() + ":" + task.Result.ReasonPhrase
                                 }, HostContext.CurrentMediaTypeFormatter);
                             }
                             return task.Result;
                         }
                     );
        }
    }
}