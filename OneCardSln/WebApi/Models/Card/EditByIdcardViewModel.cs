﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Models.Card
{
    public class EditByIdcardViewModel
    {
        [Required(ErrorMessageResourceName = "Idcard_Require", ErrorMessageResourceType = typeof(OneCardSln.Components.Resource.ViewModelResource))]
        public String idcard { get; set; }
    }
}