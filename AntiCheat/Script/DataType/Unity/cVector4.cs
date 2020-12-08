/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cVector4.cs
//  Info     : 反作弊 - Vector4 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Aya.Security
{
    public struct cVector4 : IAntiCheatValue<Vector4>, IEquatable<Vector4>
    {
        #region Value

        public Vector4 Value
        {
            get => new Vector4(_x, _y, _z, _w);
            set
            {
                _x = value.x;
                _y = value.y;
                _z = value.z;
                _w = value.w;
            }
        }

        private cFloat _x;
        private cFloat _y;
        private cFloat _z;
        private cFloat _w;

        #endregion

        #region Parse

        public static cVector4 TryParseAntiCheat(Vector4 value)
        {
            return (cVector4) value;
        }

        public static Vector4 TryParseSource(cVector4 value)
        {
            return (Vector4) value;
        }

        #endregion

        #region Override operator

        public static implicit operator cVector4(Vector4 value)
        {
            var ret = new cVector4 {Value = value};
            return ret;
        }

        public static implicit operator Vector4(cVector4 obj)
        {
            return obj.Value;
        }

        public static bool operator ==(cVector4 lhs, cVector4 rhs)
        {
            return Math.Abs(lhs._x - rhs._x) < 1e-6 && Math.Abs(lhs._y - rhs._y) < 1e-6 && Math.Abs(lhs._z - rhs._z) < 1e-6 && Math.Abs(lhs._w - rhs._w) < 1e-6;
        }

        public static bool operator !=(cVector4 lhs, cVector4 rhs)
        {
            return Math.Abs(lhs._x - rhs._x) >= 1e-6 && Math.Abs(lhs._y - rhs._y) >= 1e-6 && Math.Abs(lhs._z - rhs._z) >= 1e-6 && Math.Abs(lhs._w - rhs._w) >= 1e-6;
        }

        #endregion

        #region Override object

        public bool Equals(Vector4 obj)
        {
            return Math.Abs(Value.x - obj.x) < 1e-6 && Math.Abs(Value.y - obj.y) < 1e-6 && Math.Abs(Value.z - obj.z) < 1e-6 && Math.Abs(Value.w - obj.w) < 1e-6;
        }

        public bool Equals(cVector4 obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cVector4))
            {
                return false;
            }

            return this == (cVector4) obj;
        }

        public override string ToString()
        {
            return Value.ToString();
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

        public cVector4(SerializationInfo info, StreamingContext context)
        {
            _x = info.GetSingle("X");
            _y = info.GetSingle("Y");
            _z = info.GetSingle("Z");
            _w = info.GetSingle("W");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("X", (float) _x);
            info.AddValue("Y", (float) _y);
            info.AddValue("Z", (float) _z);
            info.AddValue("W", (float) _w);
        }

        #endregion
    }
}