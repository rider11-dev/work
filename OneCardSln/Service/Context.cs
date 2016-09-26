using OneCardSln.Components.Extensions;
using OneCardSln.Service.Card.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OneCardSln.Service
{
    public class Context
    {
        public static List<API> Apis { get; private set; }

        /// <summary>
        /// 商城api是否启用
        /// </summary>
        public static bool MallApiEnable { get; private set; }

        static Context()
        {
            GetApis();
        }

        static void GetApis()
        {
            string _configFile = "api_card.config";
            try
            {
                string configFileFullPath = string.Empty;
                var rst = FileExtension.GetFileFullPath(AppDomain.CurrentDomain.BaseDirectory, _configFile, out configFileFullPath);
                if (rst == false)
                {
                    throw new Exception("获取API错误，未找到配置文件" + _configFile);
                }
                //加载所有api配置
                XDocument doc = XDocument.Load(configFileFullPath);
                var apisNode = doc.Descendants("apis").FirstOrDefault();
                MallApiEnable = Convert.ToBoolean(apisNode.Attribute("enable").Value);

                if (!MallApiEnable)
                {
                    return;
                }
                var apis = (from a in apisNode.Descendants("api")
                            select new API
                            {
                                Name = a.Attribute("name").Value,
                                Url = a.Attribute("url").Value,
                                Provider = a.Attribute("provider").Value
                            }).ToList();
                Apis = apis;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("读取API配置文件{0}错误，{1}", _configFile, ex.Message), ex);
            }
        }
    }
}
