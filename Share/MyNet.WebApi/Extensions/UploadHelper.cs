using MyNet.Components;
using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Extensions
{
    public class UploadHelper
    {
        static string _uploadRootPath = ApiContext.RootDirectory + AppSettingUtils.Get("upload_path");

        public static void Upload(string subPath, string content)
        {
            var path = _uploadRootPath + "/" + subPath;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filePath = path + "/" + GuidExtension.GetOne();
            File.WriteAllText(filePath, content);
        }

        public static void Upload(string subPath, List<string> contents)
        {
            if (contents == null || contents.Count < 1)
            {
                return;
            }
            foreach (var content in contents)
            {
                Upload(subPath, content);
            }
        }

        public static List<string> GetContent(string subPath)
        {
            var path = _uploadRootPath + "/" + subPath;
            if (!Directory.Exists(path))
            {
                return null;
            }
            var files = new DirectoryInfo(path).GetFiles();
            List<string> contents = new List<string>();
            foreach (var file in files)
            {
                contents.Add(File.ReadAllText(file.FullName));
            }

            return contents;
        }
    }
}