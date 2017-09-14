using System;
using System.Collections.Generic;

namespace TeleReposter.Common.EqualityComparers
{
    class PropertyComparer<T, TResult> : IEqualityComparer<T>
    {
        private readonly Func<T, TResult> _getProperty;

        public PropertyComparer(Func<T, TResult> predicate)
        {
            this._getProperty = predicate;
        }

        public bool Equals(T x, T y)
        {
            return this._getProperty(x).Equals(_getProperty(y));
        }

        public int GetHashCode(T obj)
        {
            return this._getProperty(obj).GetHashCode();
        }
    }
}