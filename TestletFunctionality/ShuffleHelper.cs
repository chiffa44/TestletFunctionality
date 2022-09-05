using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TestletFunctionality
{
    public static class ShuffleHelper
    {
        private static Random rng = new Random();

        public static List<T> FisherYatesShuffle<T>(this IList<T> list)
        {
            List<T> randomizedCopy = list.ToList();
            int count = randomizedCopy.Count;
            while (count > 1)
            {
                count--;
                int k = rng.Next(count + 1);
                (randomizedCopy[k], randomizedCopy[count]) = (randomizedCopy[count], randomizedCopy[k]);
            }
            return randomizedCopy;
        }
    }
}
