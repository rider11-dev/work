﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Dto.Auth
{
    public class DictDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public string Order { get; set; }
        public bool IsDefault { get; set; }
    }
}