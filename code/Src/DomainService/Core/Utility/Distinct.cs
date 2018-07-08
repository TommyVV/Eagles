using System;
using System.Collections.Generic;

namespace Eagles.DomainService.Core.Utility
{
    public static class Distinct
    {
        public static IEnumerable<T> DistinctBy<T, TResult>(this IEnumerable<T> source, Func<T, TResult> where)
        {
            HashSet<TResult> hashSetData = new HashSet<TResult>();
            foreach (T item in source)
            {
                if (hashSetData.Add(where(item)))
                {
                    yield return item;
                }
            }
        }
    }
}
