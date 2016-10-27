using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Dispatcher;
using MyNet.Components.Extensions;

namespace MyNet.WebApi.Extensions
{
    public class ExtendedAssembliesResolver : DefaultAssembliesResolver
    {
        public ExtendedAssembliesResolver()
        {
            LoadPluginAssemblies();
        }

        public override ICollection<Assembly> GetAssemblies()
        {
            return base.GetAssemblies();
        }

        void LoadPluginAssemblies()
        {
            var assFiles = FileExtension.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "/bin/plugin", "^*.dll$");
            if (assFiles.Count > 0)
            {
                foreach (var file in assFiles)
                {
                    AssemblyName assName = AssemblyName.GetAssemblyName(file.FullName);
                    if (!AppDomain.CurrentDomain.GetAssemblies()
                        .Any(ass => AssemblyName.ReferenceMatchesDefinition(ass.GetName(), assName)))
                    {
                        AppDomain.CurrentDomain.Load(assName);
                    }
                }
            }

        }
    }
}