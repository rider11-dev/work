using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Model
{
    /// <summary>
    /// 身份证信息实体类
    /// </summary>
    public class IDCardInfo
    {
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string CardNo;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name;
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex;
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday;
        /// <summary>
        /// 地址
        /// </summary>
        public string Address;
        /// <summary>
        /// 追加地址
        /// </summary>
        public string AddressEx;
        /// <summary>
        /// 发卡机关
        /// </summary>
        public string Department;
        /// <summary>
        /// 证件开始日期
        /// </summary>
        public string StartDate;
        /// <summary>
        /// 证件结束日期
        /// </summary>
        public string EndDate;
        /// <summary>
        /// 民族
        /// </summary>
        public string Nation;
        /// <summary>
        /// 相片路径
        /// </summary>
        public string PhotoPath;
        /// <summary>
        /// 相片的字节信息
        /// </summary>
        public byte[] ArrPhotoByte;
        /// <summary>
        /// 时时图片字节信息
        /// </summary>
        public byte[] PhoTimeByte;

    }
}
