/////////////////////////////////////////////////////////////////////////////
//
//  Script   : IAntiCheatValue.cs
//  Info     : 反作弊数值类型接口
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System.Runtime.Serialization;

namespace Aya.Security
{
    public interface IAntiCheatValue : ISerializable
    {

    }

    public interface IAntiCheatValue<out TSource> : IAntiCheatValue
    {
        TSource Value { get; }
    }
}