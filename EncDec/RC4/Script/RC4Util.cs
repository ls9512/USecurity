/////////////////////////////////////////////////////////////////////////////
//
//  Script   : RC4Util.cs
//  Info     : RC4加密类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System.Text;

namespace Aya.Security
{
    public static class RC4Util
    {
        #region Enc & Dec string

        public static string Encrypt(string str)
        {
            return Encrypt(str, RC4Key.DefaultKey);
        }

        public static string Decrypt(string str)
        {
            return Decrypt(str, RC4Key.DefaultKey);
        }

        public static string Encrypt(string data, string key)
        {
            return RC4Encode(data, key);
        }

        public static string Decrypt(string data, string key)
        {
            return RC4Encode(data, key);
        }

        #endregion

        #region Private

        private static readonly StringBuilder StreamBuilder = new StringBuilder();

        private static string RC4Encode(string input, string key)
        {
            StreamBuilder.Remove(0, StreamBuilder.Length);
            int x, y, j = 0;
            var box = new int[256];
            for (var i = 0; i < 256; i++)
            {
                box[i] = i;
            }

            for (var i = 0; i < 256; i++)
            {
                j = (key[i % key.Length] + box[i] + j) % 256;
                x = box[i];
                box[i] = box[j];
                box[j] = x;
            }

            for (var i = 0; i < input.Length; i++)
            {
                y = i % 256;
                j = (box[y] + j) % 256;
                x = box[y];
                box[y] = box[j];
                box[j] = x;

                StreamBuilder.Append((char) (input[i] ^ box[(box[y] + box[j]) % 256]));
            }

            return StreamBuilder.ToString();
        }

        #endregion
    }
}
