using System.Collections.Generic;

namespace qwik.helpers
{
    public static class ListExtensions
    {
        public static void Fill<T>(this IList<T> list, T toAdd)
        {
            if (!list.Contains(toAdd))
            {
                list.Add(toAdd);
            }
        }
    }
}