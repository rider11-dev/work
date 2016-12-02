using MyNet.Components.Result;
using System.Net.Http;
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