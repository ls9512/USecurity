/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cVector3.cs
//  Info     : 反作弊 - Vector3 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Aya.Security
{
    public struct cVector3 : IAntiCheatValue<Vector3>, IEquatable<Vector3>
    {
        #region Value

        public Vector3 Value
        {
            get => new Vector3(_x, _y, _z);
            set
            {
                _x = value.x;
                _y = value.y;
                _z = value.z;
            }
        }

        private cFloat _x;
        private cFloat _y;
        private cFloat _z;

        #endregion

        #region Parse

        public static cVector3 TryParseAntiCheat(Vector3 value)
        {
            return (cVector3) value;
        }

        public static Vector3 TryParseSource(cVector3 value)
        {
            return (Vector3) value;
        }

        #endregion

        #region Override operator

        public static implicit operator cVector3(Vector3 value)
        {
            var ret = new cVector3 {Value = value};
            return ret;
        }

        public static implicit operator Vector3(cVector3 obj)
        {
            return obj.Value;
        }

        public static bool operator ==(cVector3 lhs, cVector3 rhs)
        {
            return Math.Abs(lhs._x - rhs._x) < 1e-6 && Math.Abs(lhs._y - rhs._y) < 1e-6 && Math.Abs(lhs._z - rhs._z) < 1e-6;
        }

        public static bool operator !=(cVector3 lhs, cVector3 rhs)
        {
            return Math.Abs(lhs._x - rhs._x) >= 1e-6 && Math.Abs(lhs._y - rhs._y) >= 1e-6 && Math.Abs(lhs._z - rhs._z) >= 1e-6;

        }

        #endregion

        #region Override object

        public bool Equals(Vector3 obj)
        {
            return Math.Abs(Value.x - obj.x) < 1e-6 && Math.Abs(Value.y - obj.y) < 1e-6 && Math.Abs(Value.z - obj.z) < 1e-6;
        }

        public bool Equals(cVector3 obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cVector3))
            {
                return false;
            }

            return this == (cVector3) obj;
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

        public cVector3(SerializationInfo info, StreamingContext context)
        {
            _x = info.GetSingle("X");
            _y = info.GetSingle("Y");
            _z = info.GetSingle("Z");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("X", (float) _x);
            info.AddValue("Y", (float) _y);
            info.AddValue("Z", (float) _z);
        }

        #endregion
    }
}