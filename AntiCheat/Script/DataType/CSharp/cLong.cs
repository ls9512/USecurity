/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cLong.cs
//  Info     : 反作弊 - long 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;

namespace Aya.Security
{
    public struct cLong : IAntiCheatValue<long>, IComparable, IFormattable, IConvertible, IComparable<long>, IEquatable<long>
    {
        #region Value

        public long Value
        {
            get => this;
            set => this = value;
        }

        private long _value;

        #endregion

        #region Parse

        public static cLong TryParseAntiCheat(long value)
        {
            return (cLong) value;
        }

        public static long TryParseSource(cLong value)
        {
            return (long) value;
        }

        #endregion

        #region Enc & Dec

        public static cLong Encode(long value)
        {
            cLong i;
            i._value = ~value ^ cGlobal.Key;
            return i;
        }

        public static long Decode(cLong value)
        {
            return ~value._value ^ cGlobal.Key;
        }

        #endregion

        #region Override operator

        public static implicit operator cLong(long value)
        {
            return Encode(value);
        }

        public static implicit operator long(cLong obj)
        {
            return Decode(obj);
        }

        public static cLong operator ++(cLong lhs)
        {
            return Decode(lhs) + 1;
        }

        public static cLong operator --(cLong lhs)
        {
            return Decode(lhs) - 1;
        }

        public static bool operator ==(cLong lhs, cLong rhs)
        {
            return lhs._value == rhs._value;
        }

        public static bool operator !=(cLong lhs, cLong rhs)
        {
            return lhs._value != rhs._value;
        }

        #endregion

        #region Override object

        public bool Equals(cLong obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cLong))
            {
                return false;
            }

            return this == (cLong) obj;
        }

        public override string ToString()
        {
            return Decode(this).ToString();
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion

        #region ISerializable

        public cLong(SerializationInfo info, StreamingContext context)
        {
            _value = 0;
            Value = info.GetInt64("Value");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(long obj)
        {
            return Value.Equals(obj);
        }

        #endregion

        #region IComparable<T>

        public int CompareTo(long other)
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

        #region IFormattable

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return Value.ToString(format, formatProvider);
        }

        #endregion

        #region IConvertible

        public TypeCode GetTypeCode()
        {
            return TypeCode.Int32;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(Value);
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
            return Value;
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