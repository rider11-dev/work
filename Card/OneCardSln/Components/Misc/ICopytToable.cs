using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Misc
{
    public interface ICopytToable
    {
        void CopyTo(IBaseModel targetModel);
    }
}
