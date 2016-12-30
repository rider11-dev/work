using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.User
{
    public interface IUserDetailVM : ICopyable, IValidateMeta
    {
        string user_id { get; set; }
        string user_name { get; set; }

        string user_idcard { get; set; }

        string user_truename { get; set; }

        string user_regioncode { get; set; }

        string user_remark { get; set; }

        string user_group { get; set; }
    }
}