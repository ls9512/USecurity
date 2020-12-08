/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cChar.cs
//  Info     : 反作弊 - char 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;

namespace Aya.Security
{
    public struct cChar : IAntiCheatValue<char>, IComparable, IConvertible, IComparable<char>, IEquatable<char>
    {
        #region Value

        public char Value
        {
            get => this;
            set => this = value;
        }

        private char _value;

        #endregion

        #region Parse

        public static cChar TryParseAntiCheat(char value)
        {
            return (cChar) value;
        }

        public static char TryParseSource(cChar value)
        {
            return (char) value;
        }

        #endregion

        #region Enc & Dec

        public static cChar Encode(char value)
        {
            cChar i;
            i._value = (char) (~value ^ cGlobal.Key);
            return i;
        }

        public static char Decode(cChar value)
        {
            return (char) (~value._value ^ cGlobal.Key);
        }

        #endregion

        #region Override operator

        public static implicit operator cChar(char value)
        {
            return Encode(value);
        }

        public static implicit operator char(cChar obj)
        {
            return Decode(obj);
        }

        public static bool operator ==(cChar lhs, cChar rhs)
        {
            return lhs._value == rhs._value;
        }

        public static bool operator !=(cChar lhs, cChar rhs)
        {
            return lhs._value != rhs._value;
        }

        #endregion

        #region Override object

        public bool Equals(cChar obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cChar))
            {
                return false;
            }

            return this == (cChar) obj;
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

        public cChar(SerializationInfo info, StreamingContext context)
        {
            _value = Encode(info.GetChar("Value"));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Value);
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(char obj)
        {
            return Value.Equals(obj);
        }

        #endregion

        #region IComparable<T>

        public int CompareTo(char other)
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
            return Convert.ToBoolean(Value);
        }

        public byte ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Value);
        }

        public char ToChar(IFormatProvider provider)
        {
            return Value;
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