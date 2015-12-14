using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    /*
     * 2520 is the smallest number that can be divided by each of the numbers from 1 to 10 without any remainder.
     * What is the smallest positive number that is evenly divisible by all of the numbers from 1 to 20?
     */

    internal class Problem5 : ISolvable
    {
        private int MaxNumber;
        public Problem5(int maxNumber) { MaxNumber = maxNumber; }

        public long? SolutionValue { get; set; }

        public long Solve()
        {
            long MinDivisible = 1;

            List<int> Primes = new List<int>();

            //We are going to build a complete list of all prime factors for all integers from 2 to MaxNumber
            for (int factor = 2; factor <= MaxNumber; factor++)
            {
                int tmpFactor = factor;

                foreach (var existingPrime in Primes)
                {
                    var tmpRemainder = tmpFactor % existingPrime;
                    if (tmpRemainder == 0)
                    {
                        tmpFactor = tmpFactor / existingPrime;
                    }
                    if (tmpFactor == 1) break;
                }
                if (CommonFunctions.IsPrime(tmpFactor))
                {
                    Primes.Add(tmpFactor);
                    MinDivisible *= tmpFactor;
                }

            }

            return MinDivisible;
        }


        public StringBuilder SolutionOutput()
        {
            throw new NotImplementedException();
        }
    }
}
