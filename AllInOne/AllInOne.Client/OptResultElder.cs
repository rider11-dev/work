using MyNet.Components.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllInOne.Client
{
    public class OptResultElder : OptResult
    {
        public int total { get; set; }
        public ElderInfoVM[] rows { get; set; }
    }
}
