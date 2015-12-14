using System;
using System.Text;

namespace ProjectEuler
{
    /*
     * If we list all the natural numbers below 10 that are multiples of 3 or 5, 
     * we get 3, 5, 6 and 9. The sum of these multiples is 23.
     * Find the sum of all the multiples of 3 or 5 below 1000.
     */

    internal class Problem1 : ISolvable
    {
        public int Size;
        public Problem1(int size) { Size = size; }

        public long? SolutionValue { get; private set; }

        public long Solve()
        {
            long sum = 0;
            for (int i = 1; i < Size; i++)
            {
                if ((i % 3 == 0) || (i % 5 == 0))
                    sum += i;
            }
            SolutionValue = sum;
            return sum;
        }


        public StringBuilder SolutionOutput()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Problem 1:");
            if (!SolutionValue.HasValue) result.Append("The solution is not yet calculated");
            else
                result.AppendFormat("Sum of all the multiples of 3 or 5 below {0} equals {1}", Size, SolutionValue);
            return result;
        }
    }
}
