using MyNet.Components.Extensions;
using MyNet.Components.Logger;
using MyNet.Client.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;

namespace MyNet.Client.Public
{
    public class ApiHelper
    {
        const string ApiFile = "api.config";
        const string Key_ApiProvider_Frame = "frame";

        public static List<Api> Apis { get; private set; }
        public static bool ApiEnabled { get; private set; }

        static ApiHelper()
        {
            LoadApis();
        }

        static void LoadApis()
        {
            Apis = new List<Api>();
            var files = FileExtension.GetFiles(MyContext.BaseDirectory, "api.config", SearchOption.AllDirectories);
            try
            {
                foreach (var file in files)
                {
                    //加载所有api配置
                    XDocument doc = XDocument.Load(file.FullName);
                    var apisNode = doc.Descendants("apis").FirstOrDefault();
                    ApiEnabled = Convert.ToBoolean(apisNode.Attribute("enable").Value);

                    if (!ApiEnabled)
                    {
                        continue;
                    }
                    var apis = (from a in apisNode.Descendants("api")
                                select new Api
                                {
                                    Name = a.Attribute("name").Value,
                                    RelativeUrl = a.Attribute("url").Value,
                                    Provider = a.Attribute("provider").Value
                                }).ToList();
                    Apis.AddRange(apis);
                }

            }
            catch (Exception ex)
            {
                string msg = "读取配置文件" + ApiFile + "错误";
                throw new Exception(msg, ex);
            }
        }

        /// <summary>
        /// 获取Api url
        /// </summary>
        /// <param name="apiName">api key，参考api.config</param>
        /// <param name="apiProvider">api提供者，默认frame，参考api.config</param>
        /// <returns></returns>
        public static string GetApiUrl(string apiName, string apiProvider = Key_ApiProvider_Frame)
        {
            var api = ApiHelper.Apis.Find(a =>
                string.Equals(a.Name, apiName, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(a.Provider, apiProvider, StringComparison.CurrentCultureIgnoreCase));
            if (api == null || string.IsNullOrEmpty(api.RelativeUrl))
            {
                string msg = "未找到api接口信息或接口url不存在，请检查配置是否正确";
                throw new Exception(msg);
            }

            return api.AbsoluteUrl;
        }

    }
}
