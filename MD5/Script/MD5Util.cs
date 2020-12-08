/////////////////////////////////////////////////////////////////////////////
//
//  Script   : MD5Util.cs
//  Info     : MD5验证计算辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Aya.Security
{
    public static class MD5Util
    {
        public static string GetMd5FormFile(string filePath)
        {
            var file = new FileStream(filePath, FileMode.Open);
            var md5 = new MD5CryptoServiceProvider();
            var retVal = md5.ComputeHash(file);
            file.Close();
            var sb = new StringBuilder();
            for (var i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }

            return sb.ToString();
        }

        public static string GetMd5(byte[] bytes)
        {
            var md5 = new MD5CryptoServiceProvider();
            var retVal = md5.ComputeHash(bytes);
            var sb = new StringBuilder();
            for (var i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }

            return sb.ToString();
        }

        public static string GetMd5(string str)
        {
            return GetMd5(Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        /// 获取精简MD5 / 短链接<para/>
        /// 取MD5摘要的部分特征，有概率重复
        /// </summary>
        /// <param name="str">原文</param>
        /// <returns>摘要密文</returns>
        public static string GetShortMd5(string str)
        {
            // 将长内容 md5 生成 32 位签名串,分为 4 段, 每段 8 个字节
            // 对这四段循环处理, 取 8 个字节, 将他看成 16 进制串与 0x3fffffff(30位1) 与操作, 即超过 30 位的忽略处理
            // 这 30 位分成 6 段, 每 5 位的数字作为字母表的索引取得特定字符, 依次进行获得 6 位字符串
            // 总的 md5 串可以获得 4 个 6 位串,取里面的任意一个就可作为这个长 url 的短 url 地址
            var md5 = GetMd5(str);
            var codes = new char[]
            {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v'
            };
            var hexNumbers = new uint[4];
            var hexValues = new uint[4];
            var chars = new char[6];
            var shortMd5Result = new string[4];
            var index = 0;
            for (var i = 0; i <= 24; i += 8)
            {
                hexNumbers[index] = (uint) Math.Abs(int.Parse(md5.Substring(i, 8), System.Globalization.NumberStyles.HexNumber));
                hexValues[index] = 0x3fffffff & hexNumbers[index];
                var value = hexValues[index];
                chars[0] = codes[value << 0 >> 25];
                chars[1] = codes[value << 7 >> 27];
                chars[2] = codes[value << 12 >> 27];
                chars[3] = codes[value << 17 >> 27];
                chars[4] = codes[value << 22 >> 27];
                chars[5] = codes[value << 27 >> 27];
                var shortMd5 = new string(chars);
                shortMd5Result[index] = shortMd5;
                index++;
            }

            return shortMd5Result[0];
        }
    }
}