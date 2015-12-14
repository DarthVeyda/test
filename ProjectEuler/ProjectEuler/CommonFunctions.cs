using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace ProjectEuler
{

    public static class CommonFunctions
    {
        /// <summary>
        /// Gamma function - used for quick calculation 
        /// of factorials (as Г(n+1)=n!)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double GammaLn(double x)
        {
            return (x + 0.5)*Math.Log(x + 5.5) - (x + 5.5) +
                   Math.Log(Constants.CoeffsApprox[0]*
                            (Constants.CoeffsApprox[1] + Constants.CoeffsApprox[2]/(x + 1) + Constants.CoeffsApprox[3]/(x + 2) + Constants.CoeffsApprox[4]/(x + 3) + Constants.CoeffsApprox[5]/(x + 4) + Constants.CoeffsApprox[6]/(x + 5) + Constants.CoeffsApprox[7]/(x + 6))/x);
        }

        //Lucas sequences - U and V (for the Lucas pseudoprime check)

        private static long U(int P, int Q, long n) 
        {
            if (n < 0) { throw new ArgumentOutOfRangeException("n", "Lucas sequence is defined for non-negative integers only"); }
            switch (n)
            {
                case 0: { return 0; }
                case 1: { return 1; } 
                default: { return P * U(P, Q, n - 1) - Q * U(P, Q, n - 2); }
            }
        }

        private static long V(int P, int Q, long n)
        {
            if (n < 0) { throw new ArgumentOutOfRangeException("n", "Lucas sequence is defined for non-negative integers only"); }
            switch (n)
            {
                case 0: { return 2; }
                case 1: { return P; }
                default: { return P * V(P, Q, n - 1) - Q * V(P, Q, n - 2); }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static bool IsPrime(long Number)
        {
            bool isPrime = true;
            if ((Number == 0) || (Number == 1)) return false;
            if ((Number == 2) || (Number == 3)) return true;

            #region Baillie - PSW primality test
            // Check whether Number is not Fermat pseudoprime
            if (!BigInteger.ModPow(2, Number, Number).Equals(2)) return false;

            // Check that Number is not a perfect square
            // TODO

            // Check whether Number is not Lucas pseudoprime
            // TODO

            #endregion

            // Eratosthehes sieve
            if ((Number % 6 == 1) || (Number % 6 == 5))
            {
                for (long f = Convert.ToInt64(Math.Truncate(Math.Sqrt(Number))); f >= 2; f--)
                {
                    if ((f % 6 == 1) || (f % 6 == 5) || (f == 3) || (f == 2))
                    {
                        if (Number % f == 0)
                        {
                            isPrime = false; break;
                        }
                    }
                }
                return isPrime;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static List<long> GetPrimeFactors(long Number)
        {
            List<long> Primes = new List<long>();

            if ((Number == 0) || (Number == 1)) return Primes;
            if (IsPrime(Number)) { Primes.Add(Number); return Primes; }

            long MaxFactor = (long)Math.Truncate(Math.Sqrt(Number));

            for (long factor = 2; factor <= MaxFactor; factor++)
            {
                if (IsPrime(Number)) { Primes.Add(Number); break; }

                //if (Number == 1) break;
                if (IsPrime(factor))
                {
                    while ((Number % factor == 0) && (Number > 1)) { Primes.Add(factor); Number = Number / factor; }
                }
            }
            return Primes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static bool AreCoPrimes(long Num1, long Num2)
        {
            return 1 == GCD(Num1, Num2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Num1"></param>
        /// <param name="Num2"></param>
        /// <returns></returns>
        public static long GCD(long Num1, long Num2)
        {
            //Euclid's alrothitm (modified)
            do
            {
                if (Num1 > Num2) Num1 = Num1 % Num2;
                else Num2 = Num2 % Num1;
            }
            while ((Num1 > 0) && (Num2 > 0));

            return Num1 > Num2 ? Num1 : Num2;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static List<long> NaiveGetAllFactors(long Number)
        {
            List<long> Factors = new List<long>();

            for (long factor = 1; factor <= Number; factor++)
            {
                if (Number % factor == 0) { Factors.Add(factor); }
            }

            return Factors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static List<long> SmarterGetAllFactors(long Number)
        {
            List<long> Factors = new List<long>();

            List<long> PrimeFactors = GetPrimeFactors(Number);

            foreach (var combination in ProduceWithRecursion(PrimeFactors))
            {
                long Factor = combination.Aggregate<long, long>(1, (current, primefactor) => current*primefactor);
                if (!Factors.Contains(Factor))
                    Factors.Add(Factor);
            }

            return Factors;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static List<long> EvenSmarterGetAllFactors(long Number)
        {
            List<long> PrimeFactors = GetPrimeFactors(Number);

            //Dictionary<long, int> PrimeFactorsSorted = PrimeFactors.GroupBy(factor => factor).ToDictionary(factor => factor.Key, factor => factor.Count());

            return
                ProduceWithRecursion(PrimeFactors)
                    .Distinct(new ListComparer())
                    .Select(
                        combination =>
                            combination.Aggregate<long, long>(1, (current, primefactor) => current*primefactor))
                    .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static long NumberOfFactors(long Number)
        {
            /*
             *See https://www.topcoder.com/community/data-science/data-science-tutorials/prime-numbers-factorization-and-euler-function/
             *If Number = p1^a1*...*pn^an, where p1...pn are primes, 
             *then the number of its positive divisors equals to
             *(a1 + 1) * (a2 + 1) * … * (an + 1) 
             */
            //TODO - test; the values for 6 and 15 were incorrect(?)
            return
                GetPrimeFactors(Number)
                    .GroupBy(factor => factor)
                    .Select(factor => factor.Count())
                    .Aggregate<int, long>(1, (current, count) => current*(count + 1));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static IEnumerable<long> ConstructSetFromBits(long n)
        {
            for (long i = 0; n != 0; n /= 2, i++)
            {
                if ((n & 1) != 0) { yield return i; }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="allValues"></param>
        /// <returns></returns>
        private static IEnumerable<List<long>> ProduceWithRecursion(List<long> allValues)
        {
            for (var i = 0; i < (1 << allValues.Count); i++)
            {
                yield return ConstructSetFromBits(i).Select(n => allValues[(int)n]).ToList();
            }
        }

    }
}
