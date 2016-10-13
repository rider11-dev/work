﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Base
{
    public class AddDictTypeViewModel
    {
        [Required(ErrorMessageResourceName = "Code_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        [MaxLength(10, ErrorMessageResourceName = "DictType_Code_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string type_code { get; set; }

        [MaxLength(20, ErrorMessageResourceName = "DictType_Name_Length", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public string type_name { get; set; }

        public bool type_system { get; set; }
    }
}