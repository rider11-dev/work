using log4net;
using MyNet.Components.Result;
using MyNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace MyNet.WebApi.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response =
                actionExecutedContext.Request.CreateResponse<OptResult>(new OptResult { code = ResultCode.Fail, msg = actionExecutedContext.Exception.Message });

            base.OnException(actionExecutedContext);
        }
    }
}