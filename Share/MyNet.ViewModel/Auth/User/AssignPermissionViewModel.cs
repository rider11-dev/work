using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyNet.ViewModel.Auth.User
{
    public class AssignPermissionViewModel
    {
        [Required(ErrorMessageResourceName = "UserId_Require", ErrorMessageResourceType = typeof(MyNet.ViewModel.ViewModelResource))]
        public string userId { get; set; }

        public List<string> perIds { get; set; }

        /// <summary>
        /// 是否分配所有权限
        /// </summary>
        public bool assignAll { get; set; }
    }
}