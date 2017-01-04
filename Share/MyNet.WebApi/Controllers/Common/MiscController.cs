using MyNet.Components;
using MyNet.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace MyNet.WebApi.Controllers.Common
{
    /// <summary>
    /// 杂项控制器
    /// </summary>
    [RoutePrefix("api/common/misc")]
    public class MiscController : BaseController
    {
        [HttpGet]
        [Route("verifycode")]
        public HttpResponseMessage GetVerifyCode()
        {
            var verifyCode = VerificationCodeUtils.Create(4);
            if (!verifyCode.Check())
            {
                return null;
            }
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(verifyCode.ImageBytes)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            return response;
        }
    }
}