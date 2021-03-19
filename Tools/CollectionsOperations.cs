using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.Tools
{
    public static class CollectionsOperations
    {
        public static List<int> GetCombinationsSums(this List<int> input)
        {
            List<int> result = new List<int>();
            GetCombinationsSumsLocal(0, input, result);
            return result;
        }

        private static void GetCombinationsSumsLocal(int prefix, IEnumerable<int> input, List<int> result)
        {
            result.Add(prefix);
            for (int i = 0; i < input.Count(); i++)
            {
                GetCombinationsSumsLocal(prefix + input.ElementAt(i), input.Skip(i + 1), result);
            }
        }
    }
}