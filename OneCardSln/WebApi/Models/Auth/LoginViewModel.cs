using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Auth
{
    public class LoginViewModel
    {
        public string username { get; set; }
        public string pwd { get; set; }
    }
}