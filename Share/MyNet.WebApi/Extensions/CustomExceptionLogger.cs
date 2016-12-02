using System.Web.Http.ExceptionHandling;
using System.Threading.Tasks;
using MyNet.Components.Logger;
using System.Web.Http;

namespace MyNet.WebApi.Extensions
{
    /// <summary>
    /// 自定义异常日志
    /// </summary>
    public class CustomExceptionLogger : ExceptionLogger
    {
        ILogHelper<CustomExceptionLogger> _logHelper = LogHelperFactory.GetLogHelper<CustomExceptionLogger>();
        public override void Log(ExceptionLoggerContext context)
        {
            //异步日志
            Task.Run(() =>
            {
                string msg = "";
                var responseException = context.Exception as HttpResponseException;
                if (responseException != null && responseException.Response != null && responseException.Response.RequestMessage != null)
                {
                    msg = "exception request message:" + responseException.Response.RequestMessage.ToString();
                }
                _logHelper.LogError(msg, context.Exception);
            });

            //base.Log(context);
        }
    }
}