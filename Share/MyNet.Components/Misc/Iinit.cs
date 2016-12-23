using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Misc
{
    public interface Iinit
    {
        void Init(ContainerBuilder builder);
    }
}
