using MyNet.Components.Logger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Drawing;
using MyNet.Components.Result;
using MyNet.Components.Extensions;

namespace MyNet.Components
{
    public class HttpHelper
    {
        static ILogHelper<HttpHelper> _logHelper = LogHelperFactory.GetLogHelper<HttpHelper>();

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        /// <summary>  
        /// 创建GET方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <param name="token">token</param>  
        /// <returns></returns>  
        public static HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, CookieCollection cookies, string token = "")
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = DefaultUserAgent;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            if (!string.IsNullOrEmpty(token))
            {
                AddToken(request, token);
            }
            return request.GetResponse() as HttpWebResponse;
        }
        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="jsonData">随同请求POST的参数:json对象</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <param name="token">token</param>  
        /// <returns></returns>  
        public static HttpWebResponse CreatePostHttpResponse(string url, object jsonData, int? timeout = null, string userAgent = null, Encoding requestEncoding = null, CookieCollection cookies = null, string token = "")
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (requestEncoding == null)
            {
                requestEncoding = Encoding.UTF8;
            }

            HttpWebRequest request = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.ContentType = "application/json;charset=utf-8";

            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            else
            {
                request.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            if (token.IsNotEmpty())
            {
                AddToken(request, token);
            }
            //如果需要POST数据  
            if (jsonData != null)
            {
                string jsonStr = JsonConvert.SerializeObject(jsonData);
                byte[] data = requestEncoding.GetBytes(jsonStr);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            else
            {
                //由于IIS7中POST请求限制的原因造成的,在IIS7中站点被以POST方式请求时,必须要求传递参数,如果调用的API无须传递参数,那么请加上一句即可解决411异常
                request.ContentLength = 0;
            }

            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// 发起post请求，返回响应字符串数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonData"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string Post(string url, object jsonData, string token = "")
        {
            try
            {
                var reqEncoding = Encoding.UTF8;

                var response = CreatePostHttpResponse(url, jsonData, null, null, reqEncoding, null, token);
                using (var stream = response.GetResponseStream())
                {
                    var bytes = GetBytes(stream);
                    var strResponse = reqEncoding.GetString(bytes, 0, bytes.Length);
                    //记录本次请求信息
                    _logHelper.LogInfo(string.Format("request:{2}\turl,{0}{2}\tjsonData,{1}{2}response:{2}\t{3}", url, JsonConvert.SerializeObject(jsonData), Environment.NewLine, strResponse));
                    return strResponse;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("http请求错误：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 发起get请求，返回响应字符串数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string Get(string url, string token = "")
        {
            if (url.IsEmpty())
            {
                return "";
            }
            try
            {
                var reqEncoding = Encoding.UTF8;

                var response = CreateGetHttpResponse(url, null, "", null, token);
                using (var stream = response.GetResponseStream())
                {
                    var bytes = GetBytes(stream);
                    var strResponse = reqEncoding.GetString(bytes, 0, bytes.Length);
                    //记录本次请求信息
                    _logHelper.LogInfo(string.Format("request:{0}\turl,{1}{0}\t{1}{0}response:{0}\t{2}", Environment.NewLine, url, strResponse));
                    return strResponse;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("http请求错误：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 发起Post请求，返回OptResult对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="jsonData"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static OptResult GetResultByPost(string url, object jsonData = null, string token = "")
        {
            var strResponse = Post(url, jsonData, token);
            try
            {
                return JsonConvert.DeserializeObject<OptResult>(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("解析http响应错误：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 发起Get请求，返回OptResult对象
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static OptResult GetResultByGet(string url, string token = "")
        {
            var strResponse = Get(url, token);
            try
            {
                return JsonConvert.DeserializeObject<OptResult>(strResponse);
            }
            catch (Exception ex)
            {
                throw new Exception("解析http响应错误：" + ex.Message, ex);
            }
        }

        /// <summary>
        /// 通过get方式获取图片
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Image GetImage(string url)
        {
            try
            {
                var reqEncoding = Encoding.UTF8;
                var response = CreateGetHttpResponse(url, null, null, null);
                using (var stream = response.GetResponseStream())
                {
                    return Image.FromStream(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("http请求错误：" + ex.Message, ex);
            }
        }

        private static void AddToken(HttpWebRequest request, string token)
        {
            request.Headers.Add("token", token);
        }
        private static byte[] GetBytes(Stream stream)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
    }
}
