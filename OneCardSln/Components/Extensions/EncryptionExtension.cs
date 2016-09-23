using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OneCardSln.Components.Extensions
{
    /// <summary>
    /// 加密算法辅助类
    /// </summary>
    public class EncryptionExtension
    {
        /// <summary>
        /// 返回指定字符串的Md5(32位)
        /// </summary>
        /// <param name="strInput">指定字符串</param>
        /// <returns>返回字符串的Md5</returns>
        public static string GetMd5Hash(string strInput)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] btData = md5Hasher.ComputeHash(Encoding.Default.GetBytes(strInput));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < btData.Length; i++)
            {
                sBuilder.Append(btData[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        /// <summary>
        /// 检查一个普通字符串的Md5，与传递的Md5字符串是否相同
        /// </summary>
        /// <param name="strInput">普通字符串</param>
        /// <param name="strHash">Md5字符串</param>
        /// <returns>返回是否相同</returns>
        public static bool VerifyMd5Hash(string strInput, string strHash)
        {
            // Hash the input.
            string strhashOfInput = GetMd5Hash(strInput);

            // Create a StringComparer an comare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(strhashOfInput, strHash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string EncodeBase64(Encoding encode, string src)
        {
            string result = "";
            byte[] bytes = encode.GetBytes(src);
            try
            {
                result = Convert.ToBase64String(bytes);
            }
            catch
            {
                result = src;
            }
            return result;
        }

        /// <summary>
        /// utf8
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string EncodeBase64(string src)
        {
            return EncodeBase64(Encoding.UTF8, src);
        }

        public static string DecodeBase64(Encoding encode, string src)
        {
            string result = "";
            byte[] bytes = Convert.FromBase64String(src);
            try
            {
                result = encode.GetString(bytes);
            }
            catch
            {
                result = src;
            }
            return result;
        }

        /// <summary>
        /// utf8
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string DecodeBase64(string src)
        {
            return DecodeBase64(Encoding.UTF8, src);
        }
    }
}
