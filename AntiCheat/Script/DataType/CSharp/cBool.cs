/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cBool.cs
//  Info     : 反作弊 - bool 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;

namespace Aya.Security
{
    public struct cBool : IAntiCheatValue<bool>, IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
    {
        #region Value

        public bool Value
        {
            get => _value == (9642 ^ cGlobal.Key);
            set => _value = value ? (9642 ^ cGlobal.Key) : (3571 ^ cGlobal.Key);
        }

        private int _value;

        #endregion

        #region Parse

        public static cBool TryParseAntiCheat(bool value)
        {
            return (cBool) value;
        }

        public static bool TryParseSource(cBool value)
        {
            return (bool) value;
        }

        #endregion

        #region Override operator

        public static implicit operator cBool(bool value)
        {
            var ret = new cBool {Value = value};
            return ret;
        }

        public static implicit operator bool(cBool obj)
        {
            return obj.Value;
        }

        public static bool operator ==(cBool lhs, cBool rhs)
        {
            return lhs._value == rhs._value;
        }

        public static bool operator !=(cBool lhs, cBool rhs)
        {
            return lhs._value != rhs._value;
        }

        #endregion

        #region Override object

        public bool Equals(cBool obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cBool))
            {
                return false;
            }

            return this == (cBool) obj;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion

        #region ISerializable

        public cBool(SerializationInfo info, StreamingContext context)
        {
            _value = 0;
            Value = info.GetBoolean("Value");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(bool obj)
        {
            return Value.Equals(obj);
        }

        #endregion

        #region IComparable<T>

        public int CompareTo(bool other)
        {
            return Value.CompareTo(other);
        }

        #endregion

        #region IComparable

        public int CompareTo(object obj)
        {
            return Value.CompareTo(obj);
        }

        #endregion

        #region IConvertible

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Value;
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Value);
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(Value);
        }

        public double ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(Value);
        }

        public short ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(Value);
        }

        public int ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(Value);
        }

        public long ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Value);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(Value);
        }

        public string ToString(IFormatProvider provider)
        {
            return Convert.ToString(Value);
        }

        public ushort ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(Value);
        }

        public uint ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(Value);
        }

        public ulong ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(Value);
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new InvalidCastException();
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Value, conversionType);
        }

        #endregion
    }
}
