﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Misc
{
    public interface IBaseModel
    {
        bool CanValidate { get; set; }
        bool IsValid { get; }
        string Error { get; }
    }
}
