using System;
using System.IO;
using System.Web.Http.Dispatcher;
using MyNet.Components.Extensions;

namespace MyNet.WebHostService
{
    public class ExtendedAssembliesResolver : DefaultAssembliesResolver
    {
        public ExtendedAssembliesResolver()
        {
            AssemblyExtention.LoadAssemblies(AppDomain.CurrentDomain.BaseDirectory, "^*.dll$", SearchOption.TopDirectoryOnly);
            AssemblyExtention.LoadAssemblies(AppDomain.CurrentDomain.BaseDirectory + "/apis", "^*.dll$");
        }
    }
}