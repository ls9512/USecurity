/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cString.cs
//  Info     : 反作弊 - string 等效加密类型，string 类型实时加密解密效率较低，仅限很重要的部分使用
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Aya.Security
{
    public struct cString : IAntiCheatValue<string>, IComparable, ICloneable, IConvertible, IEnumerable, IComparable<string>, IEnumerable<char>, IEquatable<string>
    {
        #region Value

        public string Value
        {
            get => this;
            set => this = value;
        }

        private string _value;

        #endregion

        #region Parse

        public static cString TryParseAntiCheat(string value)
        {
            return (cString) value;
        }

        public static string TryParseSource(cString value)
        {
            return (string) value;
        }

        #endregion

        #region Enc & Dec

        public static cString Encode(string value)
        {
            cString s;
            s._value = AESUtil.Encrypt(value, cGlobal.KeyStr, cGlobal.KeyStr);
            return s;
        }

        public static string Decode(cString value)
        {
            return AESUtil.Decrypt(value._value, cGlobal.KeyStr, cGlobal.KeyStr);
        }

        #endregion

        #region Override operator

        public static implicit operator cString(string value)
        {
            return Encode(value);
        }

        public static implicit operator string(cString obj)
        {
            return Decode(obj);
        }

        public static bool operator ==(cString lhs, cString rhs)
        {
            return lhs._value == rhs._value;
        }

        public static bool operator !=(cString lhs, cString rhs)
        {
            return lhs._value != rhs._value;
        }

        #endregion

        #region Override object

        public bool Equals(cString obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cString))
            {
                return false;
            }

            return this == (cString) obj;
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #endregion

        #region ISerializable

        public cString(SerializationInfo info, StreamingContext context)
        {
            _value = Encode(info.GetString("Value"));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Value", Decode(_value));
        }

        #endregion

        #region IEquatable<T>

        public bool Equals(string obj)
        {
            return Value.Equals(obj);
        }

        #endregion

        #region IComparable<T>

        public int CompareTo(string other)
        {
            return string.Compare(Value, other, StringComparison.Ordinal);
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
            return Value;
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
            return Convert.ToDateTime(Value, provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Value, conversionType);
        }

        #endregion

        #region IEnumerator<T>

        public CharEnumerator GetEnumerator()
        {
            return Value.GetEnumerator();
        }

        IEnumerator<char> IEnumerable<char>.GetEnumerator()
        {
            return (IEnumerator<char>)GetEnumerator();
        }

        #endregion

        #region IEnumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        #endregion

        #region ICloneable

        public object Clone()
        {
            return (object)this;
        } 

        #endregion
    }
}