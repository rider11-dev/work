using MyNet.Components.WPF.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Biz.PartyBuilding.YS.Client.Daily.Models
{
    public class TaskCompleteDetail
    {
        public string party { get; set; }
        public string comp_state { get; set; }
        public string comp_time { get; set; }
        public string comp_remark { get; set; }
        public string attach { get; set; }
    }
}
