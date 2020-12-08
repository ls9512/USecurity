/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cDecimal.cs
//  Info     : 反作弊 - decimal 等效加密类型，非必要情况下，尽可能使用cFloat
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Aya.Security
{
    public struct cDecimal : IAntiCheatValue<decimal>, IFormattable, IComparable, IConvertible, IComparable<Decimal>, IEquatable<Decimal>
    {
        #region Value

        public decimal Value
        {
            get
            {
                var v = new DecimalIntBytesUnion
                {
                    a = ~_value.a ^ cGlobal.Key,
                    b = ~_value.b ^ cGlobal.Key
                };
                return v.d;
            }
            set
            {
                var v = new DecimalIntBytesUnion {d = value};
                _value.a = ~v.a ^ cGlobal.Key;
                _value.b = ~v.b ^ cGlobal.Key;
            }
        }

        #endregion

        #region Parse

        public static cDecimal TryParseAntiCheat(decimal value)
        {
            return (cDecimal) value;
        }

        public static decimal TryParseSource(cDecimal value)
        {
            return (decimal) value;
        }

        #endregion

        #region Enc & Dec

        [StructLayout(LayoutKind.Explicit)]
        private struct DecimalIntBytesUnion
        {
            [FieldOffset(0)] public decimal d;
            [FieldOffset(0)] public long a;
            [FieldOffset(0)] public byte b1;
            [FieldOffset(1)] public byte b2;
            [FieldOffset(2)] public byte b3;
            [FieldOffset(3)] public byte b4;
            [FieldOffset(4)] public byte b5;
            [FieldOffset(5)] public byte b6;
            [FieldOffset(6)] public byte b7;
            [FieldOffset(7)] public byte b8;
            [FieldOffset(8)] public long b;
            [FieldOffset(8)] public byte b9;
            [FieldOffset(9)] public byte b10;
            [FieldOffset(10)] public byte b11;
            [FieldOffset(11)] public byte b12;
            [FieldOffset(12)] public byte b13;
            [FieldOffset(13)] public byte b14;
            [FieldOffset(14)] public byte b15;
            [FieldOffset(15)] public byte b16;
        }

        private DecimalIntBytesUnion _value;

        #endregion

        #region Override operator

        public static implicit operator cDecimal(decimal value)
        {
            var ret = new cDecimal {Value = value};
            return ret;
        }

        public static implicit operator decimal(cDecimal obj)
        {
            return obj.Value;
        }

        public static cDecimal operator ++(cDecimal lhs)
        {
            return lhs.Value + 1;
        }

        public static cDecimal operator --(cDecimal lhs)
        {
            return lhs.Value - 1;
        }

        public static bool operator ==(cDecimal lhs, cDecimal rhs)
        {
            return Math.Abs(lhs.Value - rhs.Value) < 1e-6m;
        }

        public static bool operator !=(cDecimal lhs, cDecimal rhs)
        {
            return Math.Abs(lhs.Value - rhs.Value) > 1e-6m;
        }

        #endregion

        #region Override object

        public bool Equals(cDecimal obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cDecimal))
            {
                return false;
            }

            return this == (cDecimal) obj;
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }

        public string ToString(string format)
        {
            return Value.ToString(format);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion

        #region ISerializable

        public cDecimal(SerializationInfo info, StreamingContext context)
        {
            _value = new DecimalIntBytesUnion();
            Value = info.GetDecimal("Value");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(decimal obj)
        {
            return Value.Equals(obj);
        }

        #endregion

        #region IComparable<T>

        public int CompareTo(decimal other)
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
            return Value;
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
            return Convert.ToString(Value, CultureInfo.InvariantCulture);
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