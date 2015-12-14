using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="listToTest"></param>
        /// <param name="secondList"></param>
        /// <returns></returns>
        public static bool ContentsEqual(this List<long> listToTest, List<long> secondList)
        {
            if (listToTest.Count != secondList.Count) return false;

            List<long> secondListCopy = new List<long>(secondList);

            foreach (var element in listToTest)
            {
                if (!secondListCopy.Contains(element)) return false;
                secondListCopy.Remove(element);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listToTest"></param>
        /// <param name="secondList"></param>
        /// <returns></returns>
        public static bool ContentsEqual(this List<int> listToTest, List<int> secondList)
        {
            if (listToTest.Count != secondList.Count) return false;

            List<int> secondListCopy = new List<int>(secondList);

            foreach (var element in listToTest)
            {
                if (!secondListCopy.Contains(element)) return false;
                secondListCopy.Remove(element);
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictToTest"></param>
        /// <param name="secondDict"></param>
        /// <returns></returns>
        public static bool ContentsEqual(this Dictionary<int, int> dictToTest, Dictionary<int, int> secondDict)
        {
            if (dictToTest.Count != secondDict.Count) return false;

            foreach (var element in dictToTest)
            {
                int tmpValue;
                if (!(secondDict.TryGetValue(element.Key, out tmpValue) && (element.Value == tmpValue))) return false;
            }
            return true;
        }
    }

    internal class ListComparer : IEqualityComparer<List<long>>
    {
        public bool Equals(List<long> x, List<long> y)
        {
            return x.ContentsEqual(y);
        }

        public int GetHashCode(List<long> obj)
        {
            if (null == obj) return 0;
            unchecked
            {
                return obj.Aggregate(17, (current, element) => current * 23 + element.GetHashCode());
            }
        }
    }

    class CollectionExtensions
    {
    }
}
