using System.Collections;
using System.Collections.Generic;

namespace AquaAscension.Utils
{
    public static class GenericCollectionsExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rand.Next(n);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}