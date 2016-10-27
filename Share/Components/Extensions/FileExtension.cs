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
    public static class FileExtension
    {
        public static bool GetFileFullPath(string path, string fileName, out string fileFullName)
        {
            bool result = false;
            fileFullName = string.Empty;
            if (string.IsNullOrEmpty(fileName))
            {
                return result;
            }
            var dir = new DirectoryInfo(path);
            result = GetFileFullPath(dir, fileName, out fileFullName);

            return result;
        }

        public static bool GetFileFullPath(DirectoryInfo dir, string fileName, out string fileFullName)
        {
            bool result = false;
            fileFullName = string.Empty;
            if (dir == null || !dir.Exists || string.IsNullOrEmpty(fileName))
            {
                return result;
            }
            //1、查找当前目录所有文件
            var results = dir.GetFiles(fileName, SearchOption.TopDirectoryOnly);
            if (results != null && results.Length > 0)
            {
                fileFullName = results[0].FullName;
                result = true;
                return result;
            }
            //2、查找子目录文件
            foreach (var sub in dir.GetDirectories())
            {
                result = GetFileFullPath(sub, fileName, out fileFullName);
                if (result == true)
                {
                    return result;
                }
            }

            return result;
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

        public static List<FileInfo> GetFiles(string path, string searchPattern = "", SearchOption searchOpt = SearchOption.AllDirectories)
        {
            List<FileInfo> files = new List<FileInfo>();
            var dir = new DirectoryInfo(path);
            if (!dir.Exists)
            {
                return files;
            }
            files = GetFiles(dir, searchPattern, searchOpt);
            return files;
        }

        public static List<FileInfo> GetFiles(this DirectoryInfo dir, string searchPattern = "", SearchOption searchOpt = SearchOption.AllDirectories)
        {
            var files = dir.GetFiles("*", searchOpt)
                .Where(f => Regex.IsMatch(f.Name, string.IsNullOrEmpty(searchPattern) ? "^.*$" : searchPattern));
            return files.ToList();
        }
    }
}
