using Newtonsoft.Json;
using OneCardSln.Components.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace OneCardSln.WebApi.Extensions
{
    public static class HttpUtils
    {
        public static async Task<TResult> ParseRequest<TResult>(this HttpRequestMessage request)
        {
            var stream = await request.Content.ReadAsStreamAsync();

            using (var reader = new StreamReader(stream))
            {
                var data = reader.ReadToEnd();
                TResult jsonObj = JsonConvert.DeserializeObject<TResult>(data);

                return jsonObj;
            }
        }
    }
}