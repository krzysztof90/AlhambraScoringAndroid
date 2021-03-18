using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlhambraScoringAndroid.Tools
{
    public static class CollectionsOperations
    {
        public static List<int> getCombinationsSums(this List<int> input)
        {
            List<int> result = new List<int>();
            getCombinationsSumsLocal(0, input, result);
            return result;
        }

        private static void getCombinationsSumsLocal(int prefix, IEnumerable<int> input, List<int> result)
        {
            result.Add(prefix);
            for (int i = 0; i < input.Count(); i++)
            {
                getCombinationsSumsLocal(prefix + input.ElementAt(i), input.Skip(i + 1), result);
            }
        }
    }
}