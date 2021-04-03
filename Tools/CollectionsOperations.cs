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

        public static List<int> GetMatrixSums(this List<int> list, int repeatNumber)
        {
            return GetMatrixSumsLocal(list, repeatNumber, 0, list.Count - 1);
        }

        private static List<int> GetMatrixSumsLocal(List<int> list, int repeatNumber, int c, int k)
        {
            if (c == repeatNumber - 1)
                return list.Take(k + 1).ToList();

            List<int> result = new List<int>();

            for (int i = 0; i <= k; i++)
            {
                foreach (int subElement in GetMatrixSumsLocal(list, repeatNumber, c + 1, i))
                    result.Add(list[i] + subElement);
            }

            return result;
        }
    }
}