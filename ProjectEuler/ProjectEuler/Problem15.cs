using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    /*
     * Starting in the top left corner of a 2×2 grid, and only being able to move to the right and down, 
     * there are exactly 6 routes to the bottom right corner.
     * How many such routes are there through a 20×20 grid?
     */

    internal class Problem15 : ISolvable
    {
        private readonly int _gridSize;

        public Problem15(int gridSize)
        {
            _gridSize = gridSize;
        }

        public long? SolutionValue { get; set; }

        public long Solve()
        {
            /*
             * If we look at the diagonal nodes and number them from 1 to N+1, where N is the grid size,
             * then for each Kth node there is exactly P(K,N) ways to get from top left corner to that node,
             * where P(K,N) is the Kth element in the Nth line of Pascal triangle, K = 1..N+1
             * Similarly, there's P(K,N) ways to get from the Kth node to the bottom right corner.
             * Hence the total number of routes is P(1,N)^2 + ... + P(N+1,N)^2
             * (e.g. 1*1 + 2*2 + 1*1 = 6 for the 2x2 grid)
             */
            return PascalTriangleLine(_gridSize).Sum(element => element*element);
        }

        public void TestOutput(long lineNumber)
        {
            Console.WriteLine(string.Join(" ", PascalTriangleLine(lineNumber)));
        }

        private IEnumerable<long> PascalTriangleLine(long lineNumber)
        {
            yield return 1;
            long currElement = 1;
            for (int k = 1; k <= lineNumber; k++)
            {
                yield return currElement * (lineNumber + 1 - k) / k;
                currElement = currElement * (lineNumber + 1 - k) / k;
            }
        }


        public StringBuilder SolutionOutput()
        {
            throw new NotImplementedException();
        }
    }
}
