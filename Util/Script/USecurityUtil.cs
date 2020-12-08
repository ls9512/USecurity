/////////////////////////////////////////////////////////////////////////////
//
//  Script   : USecurityUtil.cs
//  Info     : USecurity 工具类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Text;

namespace Aya.Security
{
    public static class USecurityUtil
    {
        internal static readonly Random Rand = new Random(DateTime.Now.Year * DateTime.Now.Month * DateTime.Now.Day * DateTime.Now.Millisecond);

        internal static string RandHexString(int length)
        {
            var str = new StringBuilder();
            var chars = new[]
            {
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e","f",
                "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"
            };

            for (var i = 0; i < length; i++)
            {
                var index = Rand.Next(0, 16);
                var strTemp = chars[index];
                str.Append(strTemp);
            }

            return str.ToString();
        }
    }
}
