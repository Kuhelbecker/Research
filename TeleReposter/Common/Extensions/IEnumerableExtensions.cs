﻿using System;
using System.Collections.Generic;
using System.Linq;
using TeleReposter.Common.EqualityComparers;

namespace TeleReposter.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TResult>
            (this IEnumerable<TSource> source, Func<TSource, TResult> predicate)
        {
            return source.Distinct(new PropertyComparer<TSource, TResult>(predicate));
        }
    }
}