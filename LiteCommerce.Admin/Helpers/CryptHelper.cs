using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace LiteCommerce.Admin.Helpers
{
    public class CryptHelper
    {
        /// <summary>
        /// Hàm mã hoá 1 chiều
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Md5(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            UTF8Encoding encoder = new UTF8Encoding();
            Byte[] originalBytes = encoder.GetBytes(text);
            Byte[] encondedBytes = md5.ComputeHash(originalBytes);
            text = BitConverter.ToString(encondedBytes).Replace("-", "");
            var result = text.ToLower();
            return result;
        }
    }
}