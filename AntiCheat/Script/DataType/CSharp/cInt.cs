/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cInt.cs
//  Info     : 反作弊 - int 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;

namespace Aya.Security
{
    public struct cInt : IAntiCheatValue<int>, IComparable, IFormattable, IConvertible, IComparable<int>, IEquatable<int>
    {
        #region Value

        public int Value
        {
            get => this;
            set => this = value;
        }

        private int _value;

        #endregion

        #region Parse

        public static cInt TryParseAntiCheat(int value)
        {
            return (cInt) value;
        }

        public static int TryParseSource(cInt value)
        {
            return (int) value;
        }

        #endregion

        #region Enc & Dec

        public static cInt Encode(int value)
        {
            cInt i;
            i._value = InternalEncode(value);
            return i;
        }

        public static int Decode(cInt value)
        {
            return InternalDecode(value._value);
        }

        internal static int InternalEncode(int value)
        {
            return ~value ^ cGlobal.Key;
        }

        internal static int InternalDecode(int value)
        {
            return ~value ^ cGlobal.Key;
        }

        #endregion

        #region Override operator

        public static implicit operator cInt(int value)
        {
            return Encode(value);
        }

        public static implicit operator int(cInt obj)
        {
            return Decode(obj);
        }

        public static cInt operator ++(cInt lhs)
        {
            return Decode(lhs) + 1;
        }

        public static cInt operator --(cInt lhs)
        {
            return Decode(lhs) - 1;
        }

        public static bool operator ==(cInt lhs, cInt rhs)
        {
            return lhs._value == rhs._value;
        }

        public static bool operator !=(cInt lhs, cInt rhs)
        {
            return lhs._value != rhs._value;
        }

        #endregion

        #region Override object

        public bool Equals(cInt obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cInt))
            {
                return false;
            }

            return this == (cInt) obj;
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

        public cInt(SerializationInfo info, StreamingContext context)
        {
            _value = 0;
            Value = info.GetInt32("Value");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(int obj)
        {
            return Value.Equals(obj);
        }

        #endregion

        #region IComparable<T>

        public int CompareTo(int other)
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
            return Value;
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