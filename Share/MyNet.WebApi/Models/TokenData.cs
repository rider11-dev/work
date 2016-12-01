using MyNet.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyNet.WebApi.Models
{
    /// <summary>
    /// 参考jwt：http://www.tuicool.com/articles/7BfqYre
    /// </summary>
    public class TokenData
    {
        /// <summary>
        /// 该jwt签发者（用户id）
        /// </summary>
        public string iss { get; set; }
        /// <summary>
        /// issued at，签发时间（Unix时间戳）
        /// </summary>
        public int iat { get; set; }

        /// <summary>
        /// token所指用户详细信息
        /// </summary>
        public User user { get; set; }
    }
}