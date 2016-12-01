using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Misc
{
    public class NumberRange
    {
        public double? Max { get; set; }
        public double? Min { get; set; }
        public string Unit { get; set; }
        public string Title
        {
            get
            {
                string title = "";
                if (Max.HasValue || Min.HasValue)
                {
                    if (Max.HasValue && Min.HasValue)
                    {
                        title = string.Format("{0}~{1}{2}", Min, Max, Unit);
                    }
                    else if (Max.HasValue)
                    {
                        title = string.Format("{0}{1}以下", Max, Unit);
                    }
                    else if (Min.HasValue)
                    {
                        title = string.Format("{0}{1}以上", Min, Unit);
                    }
                }

                return title;
            }
        }
    }
}
