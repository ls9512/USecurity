/////////////////////////////////////////////////////////////////////////////
//
//  Script   : SecurityExtension.cs
//  Info     : 加密操作扩展类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
//  Example  : string str = "Test";
//			   str = str.Compress().EncryptAES();
//			   str = str.DecryptAES().Decompress();
//
/////////////////////////////////////////////////////////////////////////////
using Aya.Security;

public static class SecurityExtension
{
    #region AES

    public static string EncryptAES(this string str)
    {
        return AESUtil.Encrypt(str);
    }

    public static string DecryptAES(this string str)
    {
        return AESUtil.Decrypt(str);
    }

    #endregion

    #region DES

    public static string EncryptDES(this string str)
    {
        return DESUtil.Encrypt(str);
    }

    public static string DecryptDES(this string str)
    {
        return DESUtil.Decrypt(str);
    }

    #endregion

    #region RC4

    public static string EncryptRC4(this string str)
    {
        return RC4Util.Encrypt(str);
    }

    public static string DecryptRC4(this string str)
    {
        return RC4Util.Decrypt(str);
    }

    #endregion

    #region RSA

    public static string EncryptRSA(this string str)
    {
        return RSAUtil.Encrypt(str);
    }

    public static string DecryptRSA(this string str)
    {
        return RSAUtil.Decrypt(str);
    }

    #endregion
}