/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RSAKey.cs
//  Info     : RSA密钥
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Security.Cryptography;
using UnityEngine;

namespace Aya.Security
{
    [CreateAssetMenu(menuName = "USecurity/RSAKey")]
    public class RSAKey : ScriptableObject
    {
        private const string DEFAULT_KEY = "RSAKey";

        [TextArea] public string publicKey;
        [TextArea] public string privateKey;

        public void Reset()
        {
            CreateKey();
        }

        [ContextMenu("Create Key")]
        public void CreateKey()
        {
            var rsa = new RSACryptoServiceProvider();
            var rsaPublic = rsa.ToXmlString(false);
            var rsaPrivate = rsa.ToXmlString(true);

            publicKey = rsaPrivate;
            privateKey = rsaPublic;
        }

        public static string DefaultPublicKey
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultPublicKey))
                {
                    _defaultPublicKey = PublicKey(DEFAULT_KEY);
                }

                return _defaultPublicKey;
            }
        }

        private static string _defaultPublicKey;

        public static string DefaultPrivateKey
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultPrivateKey))
                {
                    _defaultPrivateKey = PrivateKey(DEFAULT_KEY);
                }

                return PrivateKey(DEFAULT_KEY);
            }
        }

        private static string _defaultPrivateKey;

        public static string PublicKey(string keyName)
        {
            var asset = USecurityInterface.Load(keyName) as RSAKey;
            if (asset == null)
            {
                throw new NullReferenceException();
            }

            return asset.publicKey;
        }

        public static string PrivateKey(string keyName)
        {
            var asset = USecurityInterface.Load(keyName) as RSAKey;
            if (asset == null)
            {
                throw new NullReferenceException();
            }

            return asset.privateKey;
        }
    }
}
