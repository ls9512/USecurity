/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cVector2.cs
//  Info     : 反作弊 - Vector2 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Aya.Security
{
    public struct cVector2 : IAntiCheatValue<Vector2>, IEquatable<Vector2>
    {
        #region Value

        public Vector2 Value
        {
            get => new Vector2(_x, _y);
            set
            {
                _x = value.x;
                _y = value.y;
            }
        }

        private cFloat _x;
        private cFloat _y;

        #endregion

        #region Parse

        public static cVector2 TryParseAntiCheat(Vector2 value)
        {
            return (cVector2) value;
        }

        public static Vector2 TryParseSource(cVector2 value)
        {
            return (Vector2) value;
        }

        #endregion

        #region Override operator

        public static implicit operator cVector2(Vector2 value)
        {
            var ret = new cVector2 {Value = value};
            return ret;
        }

        public static implicit operator Vector2(cVector2 obj)
        {
            return obj.Value;
        }

        public static bool operator ==(cVector2 lhs, cVector2 rhs)
        {
            return Math.Abs(lhs._x - rhs._x) < 1e-6 && Math.Abs(lhs._y - rhs._y) < 1e-6;
        }

        public static bool operator !=(cVector2 lhs, cVector2 rhs)
        {
            return Math.Abs(lhs._x - rhs._x) >= 1e-6 && Math.Abs(lhs._y - rhs._y) >= 1e-6;

        }

        #endregion

        #region Override object

        public bool Equals(Vector2 obj)
        {
            return Math.Abs(Value.x - obj.x) < 1e-6 && Math.Abs(Value.y - obj.y) < 1e-6;
        }

        public bool Equals(cVector2 obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cVector2))
            {
                return false;
            }

            return this == (cVector2) obj;
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

        public cVector2(SerializationInfo info, StreamingContext context)
        {
            _x = info.GetSingle("X");
            _y = info.GetSingle("Y");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("X", (float) _x);
            info.AddValue("Y", (float) _y);
        }

        #endregion
    }
}