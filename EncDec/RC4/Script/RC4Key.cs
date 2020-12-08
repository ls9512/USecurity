/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RC4Key.cs
//  Info     : RC4密钥
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Security
{
    [CreateAssetMenu(menuName = "USecurity/RC4Key")]
    public class RC4Key : ScriptableObject
	{
		public const string DEFAULT_KEY = "RC4Key";

		public string key;

	    public void Reset()
	    {
	        CreateKey();
	    }

	    [ContextMenu("Create Key")]
	    public void CreateKey()
	    {
	        key = USecurityUtil.RandHexString(16);
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

		public static string Key(string keyName)
		{
            var asset = USecurityInterface.Load(keyName) as RC4Key;
            if (asset == null)
            {
                throw new NullReferenceException();
            }

            return asset.key;
		}
	}
}