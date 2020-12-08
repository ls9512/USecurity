/////////////////////////////////////////////////////////////////////////////
//
//  Script   : DESUtil.cs
//  Info     : DES加密辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Aya.Security
{
    public static class DESUtil
    {
        #region Enc & Dec string

        public static string Encrypt(string str)
        {
            return Encrypt(str, DESKey.DefaultKey, DESKey.DefaultIV);
        }

        public static string Decrypt(string str)
        {
            return Decrypt(str, DESKey.DefaultKey, DESKey.DefaultIV);
        }

        public static string Encrypt(string text, string key, string iv)
        {
            using (var provider = new DESCryptoServiceProvider())
            {
                var bytes = Encoding.UTF8.GetBytes(text);
                provider.Key = Encoding.ASCII.GetBytes(key);
                provider.IV = Encoding.ASCII.GetBytes(iv);
                var stream = new MemoryStream();
                using (var stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.FlushFinalBlock();
                    stream2.Close();
                }

                var str = Convert.ToBase64String(stream.ToArray());
                stream.Close();
                return str;
            }
        }

        public static string Decrypt(string text, string key, string iv)
        {
            using (var provider = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key),
                IV = Encoding.ASCII.GetBytes(iv)
            })
            {
                var stream = new MemoryStream();
                using (var stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    var buffer = Convert.FromBase64String(text);
                    stream2.Write(buffer, 0, buffer.Length);
                    stream2.FlushFinalBlock();
                    stream2.Close();
                }

                var str = Encoding.UTF8.GetString(stream.ToArray());
                stream.Close();
                return str;
            }
        }

        #endregion

        #region Enc & Dec byte[]

        public static byte[] Encrypt(byte[] bytes)
        {
            return Encrypt(bytes, DESKey.DefaultKey, DESKey.DefaultIV);
        }

        public static byte[] Decrypt(byte[] bytes)
        {
            return Decrypt(bytes, DESKey.DefaultKey, DESKey.DefaultIV);
        }

        public static byte[] Encrypt(byte[] bytes, string key, string iv)
        {
            using (var provider = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key),
                IV = Encoding.ASCII.GetBytes(iv)
            })
            {
                var stream = new MemoryStream();
                using (var stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.FlushFinalBlock();
                    stream2.Close();
                }

                var convertByte = new byte[stream.ToArray().Length];
                Array.Copy(stream.ToArray(), convertByte, stream.ToArray().Length);
                stream.Close();
                return convertByte;
            }
        }

        public static byte[] Decrypt(byte[] bytes, string key, string iv)
        {
            using (var provider = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(key),
                IV = Encoding.ASCII.GetBytes(iv)
            })
            {
                var stream = new MemoryStream();
                using (var stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    stream2.Write(bytes, 0, bytes.Length);
                    stream2.FlushFinalBlock();
                    stream2.Close();
                }

                var convertByte = new byte[stream.ToArray().Length];
                Array.Copy(stream.ToArray(), convertByte, stream.ToArray().Length);
                stream.Close();
                return convertByte;
            }
        }

        #endregion
    }
}