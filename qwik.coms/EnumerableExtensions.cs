using System.Collections.Generic;
using System.Linq;

namespace qwik.coms
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable.ToArray();
            return array[helpers.Random.Rand(array.Length)];
        }  
    }
}