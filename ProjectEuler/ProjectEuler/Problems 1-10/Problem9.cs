using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEuler
{
    internal class Problem9 : ISolvable
    {
        /*
         * A Pythagorean triplet is a set of three natural numbers, a < b < c, for which,
         * a^2 + b^2 = c^2
         * There exists exactly one Pythagorean triplet for which a + b + c = 1000.
         * Find the product abc.
         */

        private readonly int _sum;

        public Problem9(int sum) 
        {
            _sum = sum;
        }

        public long? SolutionValue { get; set; }

        public long Solve()
        {
            /*
             * Newton’s method which relies on the identity:
             * (x^2–y^2)^2+ (2xy)^2≡(x^2+y^2)^2
             *
             * a^2+ b^2= c^2
             *
             * a= d(x^2–y^2), 
             * b= 2dxy, 
             * c= d(x^2+ y^2), 
             * 
             * with x>y>0.
             * Where (x, y)=1, x and y are of opposite parity and (a,b,c)=d. 
             * If d=1, then we say that the Triple is Primitive 
             * 
             */
            
            /*
             * For this problem we assume d=1. It's not exactly the rightful assumption,
             * but we got lucky and solution exists for this particular Sum and d=1
             * 
             * a= x^2–y^2, 
             * b= 2xy, 
             * c= x^2+ y^2
             * 
             * We know that a+b+c=Sum
             * 
             * Thus,
             * 2x^2 + 2xy = Sum,
             * or
             * x+y = Sum/(2x)
             * 
             * That means Sum is divisible by both x+y and 2x
             * 
             * Now we need to go through all factors of Sum 
             * and determine if there are any that satisfy both the above condition
             * and conditions for Newton's method
             */

            if (_sum % 2 == 1) throw new Exception("Sum of the Pythagorean triplets cannot be odd");

            List<long> Factors = CommonFunctions.NaiveGetAllFactors(_sum/2);

            foreach (var factor in Factors) 
            {
                var x = factor;
                var y = _sum / (2*x) - x;
                if ((x > y) && (y > 0) && ((x + y) % 2 == 1)) // && (CommonFunctions.AreCoPrimes(x, y)) is actually
                {                                             // not necessary - we are accounting for d this way
                    var a = x * x - y * y;
                    var b = 2 * x * y;
                    var c = x * x + y * y;
                    return a * b * c;
                }
            }

            return 0;
        }


        public StringBuilder SolutionOutput()
        {
            throw new NotImplementedException();
        }
    }
}
