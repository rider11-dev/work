using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Extensions
{
    public class FileExtension
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
    }
}
