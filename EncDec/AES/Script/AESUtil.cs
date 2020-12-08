/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AESUtil.cs
//  Info     : AES加密辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Security.Cryptography;
using System.Text;

namespace Aya.Security
{
    public static class AESUtil
    {
        #region Enc & Dec string

        public static string Encrypt(string str)
        {
            return Encrypt(str, AESKey.DefaultKey, AESKey.DefaultIV);
        }

        public static string Decrypt(string str)
        {
            return Decrypt(str, AESKey.DefaultKey, AESKey.DefaultIV);
        }

        public static string Encrypt(string data, string key, string iv)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var result = Encrypt(bytes, key, iv);
            return Encoding.UTF8.GetString(result);
        }

        public static string Decrypt(string data, string key, string iv)
        {
            var result = DecryptInternal(data, key, iv);
            return Encoding.UTF8.GetString(result);
        }

        #endregion

        #region Enc & Dec byte[]

        public static byte[] Encrypt(byte[] bytes)
        {
            return Encrypt(bytes, AESKey.DefaultKey, AESKey.DefaultIV);
        }

        public static byte[] Decrypt(byte[] bytes)
        {
            return Decrypt(bytes, AESKey.DefaultKey, AESKey.DefaultIV);
        }

        public static byte[] Encrypt(byte[] data, string key, string iv)
        {
            var bKey = Encoding.UTF8.GetBytes(key);
            var ivArray = Encoding.UTF8.GetBytes(iv);

            var rDel = new RijndaelManaged
            {
                Key = bKey,
                IV = ivArray,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros
            };
            var cTransform = rDel.CreateEncryptor();
            var result = cTransform.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetBytes(Convert.ToBase64String(result, 0, result.Length));
        }

        public static byte[] Decrypt(byte[] data, string key, string iv)
        {
            return DecryptInternal(Encoding.UTF8.GetString(data), key, iv);
        }

        #endregion

        #region Private

        private static byte[] DecryptInternal(string data, string key, string iv)
        {
            var toDecrypt = Convert.FromBase64String(data);
            var keyArray = Encoding.UTF8.GetBytes(key);
            var ivArray = Encoding.UTF8.GetBytes(iv);

            var rDel = new RijndaelManaged
            {
                Key = keyArray,
                IV = ivArray,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros
            };

            var cTransform = rDel.CreateDecryptor();
            var result = cTransform.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
            // 移除解码后的尾部的空字节
            var i = result.Length - 1;
            while (i >= 0 && result[i] == 0)
            {
                --i;
            }

            var trimed = new byte[i + 1];
            Array.Copy(result, trimed, i + 1);
            return trimed;
        }

        #endregion
    }
}