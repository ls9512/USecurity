/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cGlobal.cs
//  Info     : 反作弊 - 全局配置类
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;

namespace Aya.Security
{
    internal class cGlobal
    {
        /// <summary>
        /// 随机数字密钥
        /// </summary>
        public static int Key { get; } = USecurityUtil.Rand.Next(100000, 999999);

        /// <summary>
        /// 随机字符串密钥
        /// </summary>
        public static readonly string KeyStr = Guid.NewGuid().ToString().Substring(0, 16);
    }
}