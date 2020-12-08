/////////////////////////////////////////////////////////////////////////////
//
//  Script   : AntiCheatExtension.cs
//  Info     : 反作弊 扩展方法
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Aya.Security
{
    public static class AntiCheatExtension
    {
        #region Cache

        internal static readonly Dictionary<Type, Type> AntiCheatTypeDic = new Dictionary<Type, Type>()
        {
            {typeof(bool), typeof(cBool)},
            {typeof(byte), typeof(cByte)},
            {typeof(char), typeof(cChar)},
            {typeof(decimal), typeof(cDecimal)},
            {typeof(double), typeof(cDouble)},
            {typeof(float), typeof(cFloat)},
            {typeof(int), typeof(cInt)},
            {typeof(long), typeof(cLong)},
            {typeof(short), typeof(cShort)},
            {typeof(string), typeof(cString)},
            {typeof(Color), typeof(cColor)},
            {typeof(Quaternion), typeof(cQuaternion)},
            {typeof(Vector2), typeof(cVector2)},
            {typeof(Vector3), typeof(cVector3)},
            {typeof(Vector4), typeof(cVector4)},
        };

        internal static readonly Dictionary<Type, Type> SourceTypeDic = new Dictionary<Type, Type>()
        {
            {typeof(cBool), typeof(bool)},
            {typeof(cByte), typeof(byte)},
            {typeof(cChar), typeof(char)},
            {typeof(cDecimal), typeof(decimal)},
            {typeof(cDouble), typeof(double)},
            {typeof(cFloat), typeof(float)},
            {typeof(cInt), typeof(int)},
            {typeof(cLong), typeof(long)},
            {typeof(cShort), typeof(short)},
            {typeof(cString), typeof(string)},
            {typeof(cColor), typeof(Color)},
            {typeof(cQuaternion), typeof(Quaternion)},
            {typeof(cVector2), typeof(Vector2)},
            {typeof(Vector3), typeof(cVector3)},
            {typeof(cVector4), typeof(Vector4)},
        };

        static AntiCheatExtension()
        {

        }

        #endregion

        /// <summary>
        /// 是否是反作弊数据类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static bool IsAntiCheatValue(this Type type)
        {
            var result = typeof(IAntiCheatValue).IsAssignableFrom(type);
            return result;
        }

        /// <summary>
        /// 获取反作弊类型的原始默认值，需提前判定是否为反作弊类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static object DefaultSourceValue(this Type type)
        {
            if (SourceTypeDic.TryGetValue(type, out var sourceType))
            {
                return Activator.CreateInstance(sourceType);
            }

            return null;
        }

        /// <summary>
        /// 获取反作弊类型的默认值，需提前判定是否为反作弊类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static object DefaultAntiCheatValue(this Type type)
        {
            if (SourceTypeDic.TryGetValue(type, out var sourceType))
            {
                var instance = Activator.CreateInstance(type);
                var property = type.GetProperty("Value");
                property?.SetValue(instance, Activator.CreateInstance(sourceType));
                return instance;
            }

            return null;
        }

        /// <summary>
        /// 获取反作弊类型的默认值类型，需提前判定是否为反作弊类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static Type SourceType(this Type type)
        {
            if (SourceTypeDic.TryGetValue(type, out var sourceType))
            {
                return sourceType;
            }

            return null;
        }

        /// <summary>
        /// 获取原生数据类型对应的反作弊类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>结果</returns>
        public static Type AntiCheatType(this Type type)
        {
            if (AntiCheatTypeDic.TryGetValue(type, out var antiCheatType))
            {
                return antiCheatType;
            }

            return null;
        }

        /// <summary>
        /// 获取反作弊类型的默认值类型，需提前判定是否为反作弊类型
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>结果</returns>
        public static Type SourceType(this IAntiCheatValue value)
        {
            if (SourceTypeDic.TryGetValue(value.GetType(), out var sourceType))
            {
                return sourceType;
            }

            return null;
        }

        /// <summary>
        /// 获取原生数据类型对应的反作弊类型
        /// </summary>
        /// <param name="value">数值</param>
        /// <returns>结果</returns>
        public static Type AntiCheatType(this object value)
        {
            if (AntiCheatTypeDic.TryGetValue(value.GetType(), out var antiCheatType))
            {
                return antiCheatType;
            }

            return null;
        }

        /// <summary>
        /// 尝试转换为非反作弊类型，需要确定是反作弊类型
        /// </summary>
        /// <param name="obj">值</param>
        /// <returns>值</returns>
        public static object TryParseSourceValue(this object obj)
        {
            var type = obj.GetType();
            if (!type.IsAntiCheatValue()) return obj;
            var defaultType = type.SourceType();
            var defaultValue = Convert.ChangeType(obj, defaultType, CultureInfo.InvariantCulture);
            var ins = Activator.CreateInstance(type);
            var method = ins.GetType().GetMethod("TryParseSource");
            var result = method?.Invoke(ins, new object[] {defaultValue});
            return result;
        }

        /// <summary>
        /// 尝试转换为反作弊类型，需要确定是普通类型
        /// </summary>
        /// <param name="obj">值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>值</returns>
        public static object TryParseAntiCheatValue(this object obj, Type targetType)
        {
            if (!SourceTypeDic.TryGetValue(targetType, out var sourceType))
            {
                return obj;
            }

            var sourceValue = Convert.ChangeType(obj, sourceType, CultureInfo.InvariantCulture);
            var result = sourceValue.TryParseAntiCheatValue();
            return result;
        }

        /// <summary>
        /// 尝试转换为反作弊类型，需要确定是普通类型
        /// </summary>
        /// <param name="obj">值</param>
        /// <returns>值</returns>
        public static object TryParseAntiCheatValue(this object obj)
        {
            var type = obj.GetType();
            if (!AntiCheatTypeDic.TryGetValue(type, out var antiCheatType))
            {
                return obj;
            }

            var ins = Activator.CreateInstance(antiCheatType);
            var method = ins.GetType().GetMethod("TryParseAntiCheat");
            var result = method?.Invoke(ins, new object[] {obj});
            return result;
        }
    }
}