using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    /* Find the difference between the sum of the squares of the 
     * first one hundred natural numbers and the square of the sum.
     */

    internal class Problem6 : ISolvable
    {
        private readonly int _count;
        public Problem6(int count) 
        {
            _count = count;
        }

        public long? SolutionValue { get; set; }

        public long Solve()
        {
            long Diff = 0;

            for (int i = 1; i <= _count; i++)
                for (int j = 1; j <= _count; j++)
                {
                    if (i != j)
                        Diff += i * j;
                }

            return Diff;
        }


        public StringBuilder SolutionOutput()
        {
            throw new NotImplementedException();
        }
    }
}
