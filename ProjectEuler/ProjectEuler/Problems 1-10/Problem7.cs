using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    /*
     * By listing the first six prime numbers: 2, 3, 5, 7, 11, and 13, we can see that the 6th prime is 13.
     * What is the 10 001st prime number?
*/

    internal class Problem7 : ISolvable
    {
        public int Number;

        public Problem7(int number) { Number = number; }

        public long? SolutionValue { get; set; }

        public long Solve() 
        {
            long NthPrime = 1;
            int Count = 0;
            do 
            {
                NthPrime++;
                if (CommonFunctions.IsPrime(NthPrime))
                    Count++;
            } 
            while (Count < Number);

            return NthPrime;
        }


        public StringBuilder SolutionOutput()
        {
            throw new NotImplementedException();
        }
    }
}
