﻿using MyNet.Components.Extensions;
using MyNet.Components.Logger;
using MyNet.Components.WPF.Models;
using MyNet.Model.Auth;
using MyNet.ClientFrame.Models;
using MyNet.ClientFrame.Models.Auth;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;
using MyNet.Components;

namespace MyNet.ClientFrame.Public
{
    public class Context
    {
        static string _tokenTest = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpc3MiOiJhZG1pbiIsImlhdCI6MTQ3NjE2NTg2NywidXNlciI6bnVsbH0.vGbRZRUXqWMzHwapI1-C6iMu1d8KEGUte28ZgdVcqjw";
        static string _token;
        public static string Token
        {
            get
            {
#if DEBUG
                return _tokenTest;
#endif
                return _token;
            }
            set
            {
                _token = value;
            }
        }
        public static UserViewModel CurrentUser { get; set; }
        public static string ServerRoot
        {
            get
            {
                return ConfigurationManager.AppSettings["srvroot"];
            }
        }
        public static string BaseDirectory
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory;
            }
        }

        public static IEnumerable<Permission> Pers { get; set; }

        static string _sysName;
        public static string SysName
        {
            get
            {
                if (string.IsNullOrEmpty(_sysName))
                {
                    _sysName = AppSettingHelper.Get("sysname");
                }
                return _sysName;
            }
        }
        static Context()
        {

        }

    }
}