/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RSAUtil.cs
//  Info     : RSA加密辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Security.Cryptography;
using System.Text;

namespace Aya.Security
{
    public static class RSAUtil
    {
        #region Sign & Check String

        public static string Sign(string str)
        {
            return Sign(str, RSAKey.DefaultPrivateKey);
        }

        public static bool SignCheck(string str, string sign)
        {
            return SignCheck(str, sign, RSAKey.DefaultPublicKey);
        }

        public static string Sign(string str, string privateKey)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            var sha256 = new SHA256CryptoServiceProvider();
            var rgbHash = sha256.ComputeHash(bytes);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            var formatter = new RSAPKCS1SignatureFormatter(rsa);
            formatter.SetHashAlgorithm("SHA256");
            var inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }

        public static bool SignCheck(string str, string sign, string publicKey)
        {
            try
            {
                var bytes = Encoding.UTF8.GetBytes(str);
                var sha256 = new SHA256CryptoServiceProvider();
                var rgbHash = sha256.ComputeHash(bytes);
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(publicKey);
                var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                deformatter.SetHashAlgorithm("SHA256");
                var rgbSignature = Convert.FromBase64String(sign);
                return deformatter.VerifySignature(rgbHash, rgbSignature);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Sign & Check Bytes

        public static string Sign(byte[] bytes)
        {
            return Sign(bytes, RSAKey.DefaultPrivateKey);
        }

        public static bool SignCheck(byte[] bytes, string sign)
        {
            return SignCheck(bytes, sign, RSAKey.DefaultPublicKey);
        }

        public static string Sign(byte[] bytes, string privateKey)
        {
            var sha256 = new SHA256CryptoServiceProvider();
            var rgbHash = sha256.ComputeHash(bytes);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            var formatter = new RSAPKCS1SignatureFormatter(rsa);
            formatter.SetHashAlgorithm("SHA256");
            var inArray = formatter.CreateSignature(rgbHash);
            return Convert.ToBase64String(inArray);
        }

        public static bool SignCheck(byte[] bytes, string sign, string publicKey)
        {
            try
            {
                var sha256 = new SHA256CryptoServiceProvider();
                var rgbHash = sha256.ComputeHash(bytes);
                var rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(publicKey);
                var deformatter = new RSAPKCS1SignatureDeformatter(rsa);
                deformatter.SetHashAlgorithm("SHA256");
                var rgbSignature = Convert.FromBase64String(sign);
                return deformatter.VerifySignature(rgbHash, rgbSignature);
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Enc & Dec String

        public static string Encrypt(string data)
        {
            return Encrypt(data, RSAKey.DefaultPublicKey);
        }

        public static string Decrypt(string data)
        {
            return Decrypt(data, RSAKey.DefaultPrivateKey);
        }

        public static string Encrypt(string data, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                var bytes = Encoding.UTF8.GetBytes(data);
                rsa.FromXmlString(publicKey);
                var retBytes = rsa.Encrypt(bytes, false);
                var retStr = Convert.ToBase64String(retBytes);
                return retStr;
            }
        }

        public static string Decrypt(string data, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                var bytes = Convert.FromBase64String(data);
                rsa.FromXmlString(privateKey);
                var retBytes = rsa.Decrypt(bytes, false);
                var retStr = Encoding.UTF8.GetString(retBytes);
                return retStr;
            }
        }

        #endregion

        #region Enc & Dec Bytes

        public static byte[] Encrypt(byte[] bytes)
        {
            return Encrypt(bytes, RSAKey.DefaultPublicKey);
        }

        public static byte[] Decrypt(byte[] bytes)
        {
            return Decrypt(bytes, RSAKey.DefaultPrivateKey);
        }

        public static byte[] Encrypt(byte[] bytes, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                var retBytes = rsa.EncryptValue(bytes);
                return retBytes;
            }
        }

        public static byte[] Decrypt(byte[] bytes, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                var retBytes = rsa.DecryptValue(bytes);
                return retBytes;
            }
        }

        #endregion
    }
}