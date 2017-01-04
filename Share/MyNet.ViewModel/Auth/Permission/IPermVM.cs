using MyNet.Components.Extensions;
using MyNet.Components.Misc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.Permission
{
    public interface IPermVM : IValidateMeta, ICopyable
    {
        string per_id { get; set; }
        string per_code { get; set; }

        string per_name { get; set; }

        string per_type { get; set; }

        string per_uri { get; set; }

        string per_method { get; set; }

        string per_parent { get; set; }
        bool per_system { get; set; }

        string per_sort { get; set; }

        string per_remark { get; set; }
    }
}