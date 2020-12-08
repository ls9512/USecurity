/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cFloat.cs
//  Info     : 反作弊 - float 等效加密类型
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
    public struct cFloat : IAntiCheatValue<float>, IComparable, IFormattable, IConvertible, IComparable<float>, IEquatable<float>
    {
        #region Value

        public float Value
        {
            get
            {
                var v = new FloatIntBytesUnion {i = ~_value.i ^ cGlobal.Key};
                return v.f;
            }
            set
            {
                var v = new FloatIntBytesUnion {f = value};
                _value.i = ~v.i ^ cGlobal.Key;
            }
        }

        #endregion

        #region Parse

        public static cFloat TryParseAntiCheat(float value)
        {
            return (cFloat) value;
        }

        public static float TryParseSource(cFloat value)
        {
            return (float) value;
        }

        #endregion

        #region Enc & Dec

        [StructLayout(LayoutKind.Explicit)]
        private struct FloatIntBytesUnion
        {
            [FieldOffset(0)] public float f;
            [FieldOffset(0)] public int i;
            [FieldOffset(0)] public byte b1;
            [FieldOffset(1)] public byte b2;
            [FieldOffset(2)] public byte b3;
            [FieldOffset(3)] public byte b4;
        }

        private FloatIntBytesUnion _value;

        #endregion

        #region Override operator

        public static implicit operator cFloat(float value)
        {
            var ret = new cFloat {Value = value};
            return ret;
        }

        public static implicit operator float(cFloat obj)
        {
            return obj.Value;
        }

        public static cFloat operator ++(cFloat lhs)
        {
            return lhs.Value + 1;
        }

        public static cFloat operator --(cFloat lhs)
        {
            return lhs.Value - 1;
        }

        public static bool operator ==(cFloat lhs, cFloat rhs)
        {
            return Math.Abs(lhs.Value - rhs.Value) < 1e-6;
        }

        public static bool operator !=(cFloat lhs, cFloat rhs)
        {
            return Math.Abs(lhs.Value - rhs.Value) > 1e-6;
        }

        #endregion

        #region Override object

        public bool Equals(cFloat obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cFloat))
            {
                return false;
            }

            return this == (cFloat) obj;
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

        public cFloat(SerializationInfo info, StreamingContext context)
        {
            _value = new FloatIntBytesUnion();
            Value = info.GetSingle("Value");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(float obj)
        {
            return Value.Equals(obj);
        }

        #endregion

        #region IComparable<T>

        public int CompareTo(float other)
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
            return Convert.ToInt64(Value);
        }

        public sbyte ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Value);
        }

        public float ToSingle(IFormatProvider provider)
        {
            return Value;
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