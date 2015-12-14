using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    /*
     * The following iterative sequence is defined for the set of positive integers:
     * 
     * n → n/2 (n is even)
     * n → 3n + 1 (n is odd)
     * 
     * Using the rule above and starting with 13, we generate the following sequence:
     * 13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
     * 
     * It can be seen that this sequence (starting at 13 and finishing at 1) contains 10 terms. 
     * Although it has not been proved yet (Collatz Problem), it is thought that all starting numbers finish at 1.
     * 
     * Which starting number, under one million, produces the longest chain?
     * 
     * NOTE: Once the chain starts the terms are allowed to go above one million.
     */

    internal class Problem14 : ISolvable
    {
        private readonly long _range;

        public Problem14(long range)
        {
            _range = range;
        }

        public long? SolutionValue { get; set; }

        public long Solve()
        {
            long maxChainStart = 1;
            long maxLength = 0;
            for (int i = 1; i <= _range; i++)
            {
                long chainStart = i;
                long length = 0;
                do
                {
                    length++;
                    chainStart = CollatzSequence(chainStart);
                } while (chainStart > 1);
                if (maxLength < length)
                {
                    maxLength = length;
                    maxChainStart = i;
                }
            }
            return maxChainStart;

        }

        private long CollatzSequence(long Number)
        {
            if (Number <= 0) throw new ArgumentOutOfRangeException("Number","Collatz sequence is defined for positive integers only");

            switch (Number % 2)
            {
                case 0:
                    return Number/2;
                case 1:
                    return 3 * Number + 1;
            }

            return 1;
        }


        public StringBuilder SolutionOutput()
        {
            throw new NotImplementedException();
        }
    }
}
