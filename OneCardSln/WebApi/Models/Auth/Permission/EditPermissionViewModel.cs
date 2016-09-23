using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Auth.Permission
{
    public class EditPermissionViewModel
    {
        [Required(ErrorMessageResourceName = "Edit_By_Id", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string per_id { get; set; }

        [MaxLength(40, ErrorMessageResourceName = "Per_Name_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string per_name { get; set; }

        [Required(ErrorMessageResourceName = "Per_Type_Require", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string per_type { get; set; }
        public string per_parent { get; set; }

        [MaxLength(200, ErrorMessageResourceName = "Remark_Length", ErrorMessageResourceType = typeof(Resources.Resource))]
        public string per_remark { get; set; }
    }
}