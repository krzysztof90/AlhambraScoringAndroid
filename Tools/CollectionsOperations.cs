using System.Collections.Generic;
using System.Linq;

namespace AlhambraScoringAndroid.Tools
{
    public static class CollectionsOperations
    {
        public static List<(int, int)> GetCombinationsSumsWithCount(this List<int> list)
        {
            List<(int, int)> result = new List<(int, int)>();
            GetCombinationsSumsLocal(0, 0, list, result);
            return result;
        }

        public static List<int> GetCombinationsSums(this List<int> list)
        {
            return GetCombinationsSumsWithCount(list).Select(r => r.Item1).ToList();
        }

        private static void GetCombinationsSumsLocal(int prefix, int count, IEnumerable<int> input, List<(int, int)> result)
        {
            result.Add((prefix, count));
            for (int i = 0; i < input.Count(); i++)
            {
                GetCombinationsSumsLocal(prefix + input.ElementAt(i), count + 1, input.Skip(i + 1), result);
            }
        }

        public static List<(int, int)> GetMatrixSumsWithCount(this List<int> list, int repeatNumber)
        {
            List<int> listLocal;
            if (list.Contains(0))
                listLocal = list;
            else
            {
                listLocal = list.ToList();
                listLocal.Add(0);
            }
            return GetMatrixSumsLocal(listLocal, repeatNumber, 0, listLocal.Count - 1);
        }

        public static List<int> GetMatrixSums(this List<int> list, int repeatNumber)
        {
            return GetMatrixSumsWithCount(list, repeatNumber).Select(r => r.Item1).ToList();
        }

        private static List<(int, int)> GetMatrixSumsLocal(List<int> list, int repeatNumber, int count, int k)
        {
            if (count == repeatNumber - 1)
                return list.Take(k + 1).Select(i => (i, i != 0 ? 1 : 0)).ToList();

            List<(int, int)> result = new List<(int, int)>();

            for (int i = 0; i <= k; i++)
            {
                foreach ((int, int) subElement in GetMatrixSumsLocal(list, repeatNumber, count + 1, i))
                    result.Add((list[i] + subElement.Item1, subElement.Item2 + (list[i] != 0 ? 1 : 0)));
            }

            return result;
        }
    }
}