using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.User
{
    public interface ILoginVM : IValidateMeta
    {
        string username { get; set; }

        string pwd { get; set; }
    }
}