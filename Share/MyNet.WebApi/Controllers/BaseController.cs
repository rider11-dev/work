using MyNet.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MyNet.Model;
using System.Web.Http.Controllers;
using System.Net.Http;
using MyNet.Components.Extensions;

namespace MyNet.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        public BaseController()
        {
            var dd = 0;
        }

        protected TokenData ParseToken(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.IsEmpty() || !actionContext.ActionArguments.ContainsKey("token"))
            {
                return null;
            }
            TokenData token = actionContext.ActionArguments["token"] as TokenData;

            return token;
        }
    }
}