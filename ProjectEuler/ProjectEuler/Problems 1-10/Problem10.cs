using System.Linq;
using System.Text;

namespace ProjectEuler
{
    /*
     * The sum of the primes below 10 is 2 + 3 + 5 + 7 = 17.
     * Find the sum of all the primes below two million. 
     */

    internal class Problem10 : ISolvable
    {
        private readonly int _max;
        public Problem10(int max) { _max = max; }

        public long? SolutionValue { get; set; }

        public long Solve()
        {
            return (long)Enumerable.Range(1, (int)_max - 1)
                                       .Where(i => CommonFunctions.IsPrime((long)i)).Select(i => (long)i)
                                       .Sum(i => (decimal)i);
        }


        public StringBuilder SolutionOutput()
        {
            throw new System.NotImplementedException();
        }
    }
}
