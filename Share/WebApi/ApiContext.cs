using Autofac;
using MyNet.Components;
using MyNet.Components.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNet.WebApi
{
    public class ApiContext
    {
        const string JwtRawKey = "sddfh_one_card";
        const int DefaultTokenExpire = 30;//默认token失效时间：30分钟
        public static readonly string JwtSecretKey;

        static ApiContext()
        {
            JwtSecretKey = EncryptionExtension.EncodeBase64(JwtRawKey);
        }

        /// <summary>
        /// token失效时间：单位，分钟，默认30
        /// </summary>
        public static int TokenExpire
        {
            get
            {
                int val = 0;
                if (!int.TryParse(AppSettingHelper.Get("token_expire"), out val))
                {
                    val = DefaultTokenExpire;
                }

                return val;
            }
        }

        public static string RootDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static bool IsDebug
        {
            get
            {
                bool debug = false;
                bool.TryParse(AppSettingHelper.Get("debug"), out debug);
                return debug;
            }
        }
    }
}