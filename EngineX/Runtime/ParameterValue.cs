using System;

namespace EngineX.Runtime
{
    public struct ParameterValue : IEquatable<ParameterValue>
    {
        private Object _inner;

        public ParameterValue(object inner)
        {
            _inner = inner;
        }

        public object ValueForCalculation => _inner;

        public bool Equals(ParameterValue other)
        {
            return ParameterValueHelper.AreEqual(_inner, other._inner);
        }

        public override bool Equals(object obj)
        {
            return obj is ParameterValue other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (_inner != null ? ParameterValueHelper.GetHashCode(_inner) : 0);
        }
        public static implicit operator int(ParameterValue d) => ParameterValueHelper.ToInt(d._inner);
        public static implicit operator ParameterValue(int d) => new(d);
        public static implicit operator ParameterValue(decimal d) => new(d);
        public static implicit operator ParameterValue(float d) => new(d);
        public static implicit operator ParameterValue(string d) => new(d);
    }
}