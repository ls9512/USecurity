/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AESKey.cs
//  Info     : AES密钥
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Security
{
    [CreateAssetMenu(menuName = "USecurity/AESKey")]
    public class AESKey : ScriptableObject
    {
        private const string DEFAULT_KEY = "AESKey";

        public string key;
        public string iv;

        public void Reset()
        {
            CreateKey();
        }

        [ContextMenu("Create Key")]
        public void CreateKey()
        {
            key = USecurityUtil.RandHexString(16);
            iv = USecurityUtil.RandHexString(16);
        }

        public static string DefaultKey
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultKey))
                {
                    _defaultKey = Key(DEFAULT_KEY);
                }

                return _defaultKey;
            }
        }

        private static string _defaultKey;

        public static string DefaultIV
        {
            get
            {
                if (string.IsNullOrEmpty(_defaultIv))
                {
                    _defaultIv = IV(DEFAULT_KEY);
                }

                return IV(DEFAULT_KEY);
            }
        }

        private static string _defaultIv;

        public static string Key(string keyName)
        {
            var asset = USecurityInterface.Load(keyName) as AESKey;
            if (asset == null)
            {
                throw new NullReferenceException();
            }

            return asset.key;
        }

        public static string IV(string keyName)
        {
            var asset = USecurityInterface.Load(keyName) as AESKey;
            if (asset == null)
            {
                throw new NullReferenceException();
            }

            return asset.iv;
        }
    }
}