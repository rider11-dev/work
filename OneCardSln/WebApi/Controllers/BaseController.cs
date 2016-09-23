using OneCardSln.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using OneCardSln.Model;
using System.Web.Http.Controllers;
using System.Net.Http;

namespace OneCardSln.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        public BaseController()
        {
        }

        protected TokenData ParseToken(HttpActionContext actionContext)
        {
            TokenData token = actionContext.ActionArguments["token"] as TokenData;

            return token;
        }
    }
}