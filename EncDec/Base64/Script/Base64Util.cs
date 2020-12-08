/////////////////////////////////////////////////////////////////////////////
//
//  Script   : Base64Util.cs
//  Info     : Base64 加密辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Text;

namespace Aya.Security
{
    public static class Base64Util
    {
        public static string Encode(string str)
        {
            var mod4 = str.Length % 4;
            if (mod4 > 0)
            {
                str += new string('=', 4 - mod4);
            }
            var plainTextBytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Decode(string str)
        {
            var base64EncodedBytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}