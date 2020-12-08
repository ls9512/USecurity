/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cQuaternion.cs
//  Info     : 反作弊 - cQuaternion 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Aya.Security
{
    public struct cQuaternion : IAntiCheatValue<Quaternion>, IEquatable<Quaternion>
    {
        #region Value

        public Quaternion Value
        {
            get => new Quaternion(_x, _y, _z, _w);
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

        public static cQuaternion TryParseAntiCheat(Quaternion value)
        {
            return (cQuaternion) value;
        }

        public static Quaternion TryParseSource(cQuaternion value)
        {
            return (Quaternion) value;
        }

        #endregion

        #region Override operator

        public static implicit operator cQuaternion(Quaternion value)
        {
            var ret = new cQuaternion {Value = value};
            return ret;
        }

        public static implicit operator Quaternion(cQuaternion obj)
        {
            return obj.Value;
        }

        public static bool operator ==(cQuaternion lhs, cQuaternion rhs)
        {
            return Math.Abs(lhs._x - rhs._x) < 1e-6 && Math.Abs(lhs._y - rhs._y) < 1e-6 && Math.Abs(lhs._z - rhs._z) < 1e-6 && Math.Abs(lhs._w - rhs._w) < 1e-6;
        }

        public static bool operator !=(cQuaternion lhs, cQuaternion rhs)
        {
            return Math.Abs(lhs._x - rhs._x) >= 1e-6 && Math.Abs(lhs._y - rhs._y) >= 1e-6 && Math.Abs(lhs._z - rhs._z) >= 1e-6 && Math.Abs(lhs._w - rhs._w) >= 1e-6;

        }

        #endregion

        #region Override object

        public bool Equals(Quaternion obj)
        {
            return Math.Abs(Value.x - obj.x) < 1e-6 && Math.Abs(Value.y - obj.y) < 1e-6 && Math.Abs(Value.z - obj.z) < 1e-6 && Math.Abs(Value.w - obj.w) < 1e-6;
        }

        public bool Equals(cQuaternion obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cQuaternion))
            {
                return false;
            }

            return this == (cQuaternion) obj;
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

        public cQuaternion(SerializationInfo info, StreamingContext context)
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