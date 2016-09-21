using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using log4net;
using System.Threading;
using System.Threading.Tasks;
using OneCardSln.Components.Logger;

namespace OneCardSln.WebApi.Extensions
{
    /// <summary>
    /// 自定义异常日志
    /// </summary>
    public class CustomExceptionLogger : ExceptionLogger
    {
        ILogHelper<CustomExceptionLogger> _logHelper = LogHelperFactory.GetLogHelper<CustomExceptionLogger>();
        public override void Log(ExceptionLoggerContext context)
        {
            Task.Run(() =>
            {
                _logHelper.LogError(context.Exception);
            });

            //base.Log(context);
        }
    }
}