using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Dispatcher;
using MyNet.Components.Extensions;

namespace MyNet.WebHostService
{
    public class ExtendedAssembliesResolver : DefaultAssembliesResolver
    {
        public ExtendedAssembliesResolver()
        {
            AssemblyExtention.LoadAssemblies(AppDomain.CurrentDomain.BaseDirectory + "/apis", "^*.dll$");
        }
    }
}