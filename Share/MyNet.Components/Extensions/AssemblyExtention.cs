using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyNet.Components.Extensions
{
    public static class AssemblyExtention
    {
        /// <summary>
        /// 加载程序集到应用程序域
        /// </summary>
        /// <param name="assFiles"></param>
        public static void LoadAssemblies(List<FileInfo> assFiles)
        {
            if (assFiles.Count > 0)
            {
                foreach (var file in assFiles)
                {
                    try
                    {
                        AssemblyName assName = AssemblyName.GetAssemblyName(file.FullName);
                        if (!AppDomain.CurrentDomain.GetAssemblies()
                            .Any(ass => AssemblyName.ReferenceMatchesDefinition(ass.GetName(), assName)))
                        {
                            AppDomain.CurrentDomain.Load(assName);
                        }
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// 加载指定目录指定过滤条件的程序集文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchPattern"></param>
        /// <param name="searchOpt"></param>
        public static void LoadAssemblies(string path, string searchPattern = "", SearchOption searchOpt = SearchOption.AllDirectories)
        {
            var files = FileExtension.GetFiles(path, searchPattern, searchOpt);
            LoadAssemblies(files);
        }

        public static List<Assembly> GetAssemblies(string path, string searchPattern = "", SearchOption searchOpt = SearchOption.AllDirectories)
        {
            List<Assembly> assList = new List<Assembly>();
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                return assList;
            }
            assList = GetAssemblies(dir, searchPattern, searchOpt);
            return assList;
        }


        public static List<Assembly> GetAssemblies(this DirectoryInfo dir, string searchPattern = "", SearchOption searchOpt = SearchOption.AllDirectories)
        {
            var target = new List<Assembly>();
            //当前目录及其子目录下所有符合的文件
            var files = dir.GetFiles("*.dll", searchOpt)
                .Where(f => Regex.IsMatch(f.Name, searchPattern));
            foreach (var file in files)
            {
                target.Add(Assembly.LoadFile(file.FullName));
            }
            return target;
        }

        public static string GetAssemblyDirectory(this Assembly ass)
        {
            if (ass == null)
            {
                return null;
            }
            return new FileInfo(ass.Location).Directory.FullName.TrimEnd('/', '\\');
        }

        public static IEnumerable<Type> GetTypes(this Assembly ass)
        {
            if (ass == null)
            {
                yield break;
            }
            Type[] types;
            try
            {
                types = ass.GetTypes();
            }
            catch
            {
                yield break;
            }
            foreach (var t in types)
            {
                yield return t;
            }
        }

        public static IEnumerable<Type> GetLoadedTypes(this AppDomain appdomain)
        {
            if (appdomain == null)
            {
                yield break;
            }
            foreach (var ass in appdomain.GetAssemblies())
            {
                foreach (var t in ass.GetTypes())
                {
                    yield return t;
                }
            }
        }
    }
}
