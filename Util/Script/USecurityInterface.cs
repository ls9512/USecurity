/////////////////////////////////////////////////////////////////////////////
//
//  Script   : USecurityInterface.cs
//  Info     : USecurity 外部定义接口
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using UnityEngine;

namespace Aya.Security
{
    public static class USecurityInterface
    {
        public static Func<string, UnityEngine.Object> Load { get; set; } = Resources.Load;
    }
}
