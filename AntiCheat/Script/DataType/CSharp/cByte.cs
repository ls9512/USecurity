/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cByte.cs
//  Info     : 反作弊 - byte 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;

namespace Aya.Security
{
    public struct cByte : IAntiCheatValue<byte>, IComparable, IFormattable, IConvertible, IComparable<byte>, IEquatable<byte>
    {
        #region Value

        public byte Value
        {
            get => this;
            set => this = value;
        }

        private byte _value;

        #endregion

        #region Parse

        public static cByte TryParseAntiCheat(byte value)
        {
            return (cByte) value;
        }

        public static byte TryParseSource(cByte value)
        {
            return (byte) value;
        }

        #endregion

        #region Enc & Dec

        public static cByte Encode(byte value)
        {
            cByte i;
            i._value = (byte) (~value ^ cGlobal.Key);
            return i;
        }

        public static byte Decode(cByte value)
        {
            return (byte) (~value._value ^ cGlobal.Key);
        }

        #endregion

        #region Override operator

        public static implicit operator cByte(byte value)
        {
            return Encode(value);
        }

        public static implicit operator byte(cByte obj)
        {
            return Decode(obj);
        }


        public static bool operator ==(cByte lhs, cByte rhs)
        {
            return lhs._value == rhs._value;
        }

        public static bool operator !=(cByte lhs, cByte rhs)
        {
            return lhs._value != rhs._value;
        }

        #endregion

        #region Override object

        public bool Equals(cByte obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cByte))
            {
                return false;
            }

            return this == (cByte) obj;
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

        public cByte(SerializationInfo info, StreamingContext context)
        {
            _value = Encode(info.GetByte("Value"));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(byte obj)
        {
            return Value.Equals(obj);
        }

        #endregion

        #region IComparable<T>

        public int CompareTo(byte other)
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
            return Value;
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