/////////////////////////////////////////////////////////////////////////////
//
//  Script   : PlayerPrefsAES.cs
//  Info     : PlayerPrefs加密存储辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System.Globalization;
using UnityEngine;

namespace Aya.Security
{
	public static class PlayerPrefsAES
	{
		#region string

		public static void SetString(string key, string value)
		{
			PlayerPrefs.SetString(MD5Util.GetMd5(key), AESUtil.Encrypt(value, AESKey.DefaultKey, AESKey.DefaultIV));
		}

		public static string GetString(string key, string defaultValue = default(string))
		{
			var result = PlayerPrefs.GetString(MD5Util.GetMd5(key));
			return string.IsNullOrEmpty(result) ? defaultValue : AESUtil.Decrypt(result, AESKey.DefaultKey, AESKey.DefaultIV);
		}

		#endregion

		#region int

		public static void SetInt(string key, int value)
		{
			SetString(key, value.ToString());
		}

		public static int GetInt(string key, int defaultValue = default(int))
		{
			var s = GetString(key);
		    return int.TryParse(s, out var ret) ? ret : defaultValue;
		}

		#endregion

		#region long

		public static void SetLong(string key, long value)
		{
			SetString(key, value.ToString());
		}

		public static long GetLong(string key, long defaultValue = default(long))
		{
			var s = GetString(key);
            return long.TryParse(s, out var ret) ? ret : defaultValue;
		}

		#endregion

		#region float

		public static void SetFloat(string key, float value)
		{
			SetString(key, value.ToString(CultureInfo.InvariantCulture));
		}

		public static float GetFloat(string key, float defaultValue = default(float))
		{
			var s = GetString(key);
            return float.TryParse(s, out var ret) ? ret : defaultValue;
		}

		#endregion

		#region double

		public static void SetDouble(string key, double value)
		{
			SetString(key, value.ToString(CultureInfo.InvariantCulture));
		}

		public static double GetDouble(string key, double defaultValue = default(int))
		{
			var s = GetString(key);
            return double.TryParse(s, out var ret) ? ret : defaultValue;
		}

		#endregion

		#region bool

		public static void SetBool(string key, bool value)
		{
			SetString(key, value.ToString());
		}

		public static bool GetBool(string key, bool defaultValue = default(bool))
		{
			var s = GetString(key);
            return bool.TryParse(s, out var ret) ? ret : defaultValue;
		}

        #endregion

        #region Has

        public static bool HasKey(string key)
        {
            return PlayerPrefs.HasKey(MD5Util.GetMd5(key));
        }

        #endregion

        #region Delete

        public static void DeleteKey(string key)
		{
			PlayerPrefs.DeleteKey(MD5Util.GetMd5(key));
		}

		public static void DeleteAll()
		{
			PlayerPrefs.DeleteAll();
		}

        #endregion

        #region Save

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        #endregion
    }
}
