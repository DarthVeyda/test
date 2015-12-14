using System;
using System.Text;

namespace ProjectEuler
{
    /*
     * The prime factors of 13195 are 5, 7, 13 and 29.
     * What is the largest prime factor of the number 600851475143 ?
     */

    internal class Problem3 : ISolvable
    {
        private readonly long _number;
        public Problem3(long number) { _number = number; }

        public long? SolutionValue { get; set; }

        public long Solve() 
        {
            long MaxFactor = 1;
            for (long p = Convert.ToInt64(Math.Truncate(Math.Sqrt(_number))); p >= 2; p--) 
            {
                if ((_number % p == 0) && ((p % 6 == 1) || (p % 6 == 5) || (p == 3) || (p == 2))) 
                {
                    if (CommonFunctions.IsPrime(p))
                    {
                        MaxFactor = p;
                        break;
                    }
                }
            }

            return MaxFactor;
        }

        public StringBuilder SolutionOutput()
        {
            throw new NotImplementedException();
        }
    }
}
