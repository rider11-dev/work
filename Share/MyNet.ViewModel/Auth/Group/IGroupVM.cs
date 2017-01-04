using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.Group
{
    public interface IGroupVM : IValidateMeta, ICopyable
    {
        string gp_id { get; set; }
        string gp_code { get; set; }
        string gp_name { get; set; }
        bool gp_system { get; set; }
        string gp_parent { get; set; }
        string gp_sort { get; set; }
    }
}