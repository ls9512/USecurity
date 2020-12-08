/////////////////////////////////////////////////////////////////////////////
//
//  Script   : cColor.cs
//  Info     : 反作弊 - Color 等效加密类型
//  Author   : ls9512
//  E-mail   : ls9512@vip.qq.com
//
/////////////////////////////////////////////////////////////////////////////
using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace Aya.Security
{
    public struct cColor : IAntiCheatValue<Color>, IEquatable<Color>
    {
        #region Value

        public Color Value
        {
            get => new Color(_r, _g, _b, _a);
            set
            {
                _r = value.r;
                _g = value.g;
                _b = value.b;
                _a = value.a;
            }
        }

        private cFloat _r;
        private cFloat _g;
        private cFloat _b;
        private cFloat _a;

        #endregion

        #region Parse

        public static cColor TryParseAntiCheat(Color value)
        {
            return (cColor)value;
        }

        public static Color TryParseSource(cColor value)
        {
            return (Color)value;
        }

        #endregion

        #region Override operator

        public static implicit operator cColor(Color value)
        {
            var ret = new cColor {Value = value};
            return ret;
        }

        public static implicit operator Color(cColor obj)
        {
            return obj.Value;
        }

        public static bool operator ==(cColor lhs, cColor rhs)
        {
            return lhs._r.Equals(rhs._r) && lhs._g.Equals(rhs._g) && lhs._b.Equals(rhs._b) && lhs._a.Equals(rhs._a);
        }

        public static bool operator !=(cColor lhs, cColor rhs)
        {
            return !lhs._r.Equals(rhs._r) || !lhs._g.Equals(rhs._g) || !lhs._b.Equals(rhs._b) || !lhs._a.Equals(rhs._a);

        }

        #endregion

        #region Override object

        public bool Equals(Color obj)
        {
            return obj.r.Equals(_r) && obj.g.Equals(_g) && obj.b.Equals(_b) && obj.a.Equals(_a);
        }

        public bool Equals(cColor obj)
        {
            return this == obj;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is cColor))
            {
                return false;
            }

            return this == (cColor) obj;
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

        public cColor(SerializationInfo info, StreamingContext context)
        {
            _r = info.GetSingle("R");
            _g = info.GetSingle("G");
            _b = info.GetSingle("B");
            _a = info.GetSingle("A");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("R", (float) _r);
            info.AddValue("G", (float) _g);
            info.AddValue("B", (float) _b);
            info.AddValue("A", (float) _a);
        }

        #endregion
    }
}