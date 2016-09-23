using OneCardSln.Model;
using OneCardSln.Model.Auth;
using OneCardSln.Service.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OneCardSln.WebApi.Extensions.Validation
{
    /// <summary>
    /// 权限类别验证属性
    /// 这里只做实验用，对于权限，应该从数据库查询基础字典判断，这个类暂时先不用了
    /// </summary>
    public class PermissionTypeAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value != null)
            {
                var type = value.ToString();
                return new string[] { "0", "1" }.Contains(type);
            }

            return base.IsValid(value);
        }
    }
}