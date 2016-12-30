using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.User
{
    public interface IChgPwdVM : IValidateMeta
    {
        string userid { get; set; }

        string oldpwd { get; set; }

        string newpwd { get; set; }

        string newpwd2 { get; set; }
    }
}