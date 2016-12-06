using MyNet.Components.Result;
using MyNet.WebApi.Extensions;
using MyNet.WebHostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace MyNet.WebApi.Filters
{
    public class ValidateModelFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new ObjectContent<object>(OptResult.Build(ResultCode.ParamError, actionContext.ModelState.Parse()), HostContext.CurrentMediaTypeFormatter)
                };
                return;
            };
            base.OnActionExecuting(actionContext);
        }

    }
}
