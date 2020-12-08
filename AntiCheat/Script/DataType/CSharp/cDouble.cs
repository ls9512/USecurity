/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cDouble.cs
//  Info     : 反作弊 - double 等效加密类型，非必要情况下，尽可能使用cFloat
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
    public struct cDouble : IAntiCheatValue<double>, IComparable, IFormattable, IConvertible, IComparable<double>, IEquatable<double>
    {
        #region Value

        public double Value
        {
            get
            {
                var v = new DoubleIntBytesUnion {i = ~_value.i ^ cGlobal.Key};
                return v.d;
            }
            set
            {
                var v = new DoubleIntBytesUnion {d = value};
                _value.i = ~v.i ^ cGlobal.Key;
            }
        }

        #endregion

        #region Parse

        public static cDouble TryParseAntiCheat(double value)
        {
            return (cDouble) value;
        }

        public static double TryParseSource(cDouble value)
        {
            return (double) value;
        }

        #endregion

        #region Enc & Dec

        [StructLayout(LayoutKind.Explicit)]
        private struct DoubleIntBytesUnion
        {
            [FieldOffset(0)] public double d;
            [FieldOffset(0)] public long i;
            [FieldOffset(0)] public byte b1;
            [FieldOffset(1)] public byte b2;
            [FieldOffset(2)] public byte b3;
            [FieldOffset(3)] public byte b4;
            [FieldOffset(4)] public byte b5;
            [FieldOffset(5)] public byte b6;
            [FieldOffset(6)] public byte b7;
            [FieldOffset(7)] public byte b8;
        }

        private DoubleIntBytesUnion _value;

        #endregion

        #region Override operator

        public static implicit operator cDouble(double value)
        {
            var ret = new cDouble {Value = value};
            return ret;
        }

        public static implicit operator double(cDouble obj)
        {
            return obj.Value;
        }

        public static cDouble operator ++(cDouble lhs)
        {
            return lhs.Value + 1;
        }

        public static cDouble operator --(cDouble lhs)
        {
            return lhs.Value - 1;
        }

        public static bool operator ==(cDouble lhs, cDouble rhs)
        {
            return Math.Abs(lhs.Value - rhs.Value) < 1e-8;
        }

        public static bool operator !=(cDouble lhs, cDouble rhs)
        {
            return Math.Abs(lhs.Value - rhs.Value) > 1e-8;
        }

        #endregion

        #region Override object

        public bool Equals(cDouble obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cDouble))
            {
                return false;
            }

            return this == (cDouble) obj;
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

        public cDouble(SerializationInfo info, StreamingContext context)
        {
            _value = new DoubleIntBytesUnion();
            Value = info.GetDouble("Value");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(double obj)
        {
            return Value.Equals(obj);
        }

        #endregion

        #region IComparable<T>

        public int CompareTo(double other)
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
            return Value;
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