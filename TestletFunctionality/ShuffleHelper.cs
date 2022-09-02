using System;
using System.Collections.Generic;
using System.Text;

namespace TestletFunctionality
{
    public static class ShuffleHelper
    {
        private static Random rng = new Random();

        //Fisher–Yates shuffle
        public static void Shuffle<T>(this IList<T> list)
        {
            int count = list.Count;
            while (count > 1)
            {
                count--;
                int k = rng.Next(count + 1);
                (list[k], list[count]) = (list[count], list[k]);
            }

        }
    }
}
