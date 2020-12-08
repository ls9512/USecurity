/////////////////////////////////////////////////////////////////////////////
//
//  Script   : DESFileUtil.cs
//  Info     : DES文件加密辅助类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System.IO;

namespace Aya.Security
{
	public static class DESFileUtil
	{
		#region Read & Write string

		public static string ReadAllText(string path, string key, string iv)
		{
			var text = File.ReadAllText(path);
			return DESUtil.Decrypt(text, key, iv);
		}

		public static void WriteAllText(string path, string text, string key, string iv)
		{
			var bs = DESUtil.Encrypt(text, key, iv);
			File.WriteAllText(path, bs);
		}

		#endregion

		#region Read & Write byte[]

		public static byte[] ReadAllBytes(string path, string key, string iv)
		{
			var text = File.ReadAllBytes(path);
			return DESUtil.Decrypt(text, key, iv);
		}

		public static void WriteAllBytes(string path, byte[] bytes, string key, string iv)
		{
			var bs = DESUtil.Encrypt(bytes, key, iv);
			File.WriteAllBytes(path, bs);
		}

		#endregion
	}
}
