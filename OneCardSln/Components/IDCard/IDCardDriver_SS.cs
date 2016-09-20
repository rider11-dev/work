using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneCardSln.Components.IDCard
{
    /// <summary>
    /// 深思读卡器
    /// </summary>
    public class IDCardDriver_SS
    {
        /// <summary>
        /// 二代居民身份证读卡接口函数
        /// 1、*pCmd:
        ///     0x41: 初始化端口，
        ///         *parg0 ：串口号，取值 1~16;USB口 取值 1001~1016;*parg0=0时，自动查找端口范围串口1~8，USB1001~1016
        ///     0x42: 关闭端口
        ///         *parg0 无效
        ///     0x43: 验证卡
        ///         *parg0 无效
        ///     0x44: 读基本信息
        ///     0x45: 读最新住址信息
        ///     0x46: 仅读文字信息
        ///     0x47: 读基本信息但不进行图像解码
        /// 2、端口初始化后，在退出程序时必须调用关闭端口
        /// 3、要读取卡内信息，必须先认证卡，成功后才能够读取
        /// </summary>
        /// <param name="pCmd"></param>
        /// <param name="parg0"></param>
        /// <param name="parg1"></param>
        /// <param name="parg2">当*parg2为 char * 时，指定二代证的存储文件</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("RdCard.dll")]
        public static extern int UCommand1(ref byte pCmd, ref  int parg0, ref int parg1, byte[] parg2);


        /// <summary>
        /// 错误信息字典
        /// </summary>
        public static Dictionary<int, string> ErrorMsg = new Dictionary<int, string>()
        {
            {-1,"相片解码错误"},
            {-2,"Wlt文件后缀错误"},
            {-3,"Wlt文件打开错误"},
            {-4,"Wlt文件格式错误"},
            {-5,"软件未授权"},
            {-6,"设备连接错误"},
            {-7,"设备不正确（非法设备）"},
            {-8,"文件存储失败"},
            {-9,"加载通讯函数错误"},
            {-10,"端口操作失败"},
            {-11,"解码失败"},
            {2,"数据接收超时"},
            {0,"未知错误"}
        };
    }
}
