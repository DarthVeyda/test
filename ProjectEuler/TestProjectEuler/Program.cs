using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NUnit;
using NUnit.Framework;
using ProjectEuler;

namespace TestProjectEuler
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Testing GetFactors");
            //Console.WriteLine("Naive: {0} {1} {2}" , DateTime.Now, CommonFunctions.PrettyPrint(CommonFunctions.NaiveGetAllFactors(123456789098)),DateTime.Now);
            long LargeNumber = 678987654321;
            List<long> Factors1;
            List<long> Factors2;
            List<double> Meth1Times = new List<double>();
            List<double> Meth2Times = new List<double>();
            for (int i = 0; i < 200; i++)
            {
                var timer = Stopwatch.StartNew();
                Factors1 = new List<long>(CommonFunctions.SmarterGetAllFactors(LargeNumber));
                timer.Stop();
                Meth1Times.Add(timer.Elapsed.TotalMilliseconds);

                timer.Restart();
                Factors2 = new List<long>(CommonFunctions.EvenSmarterGetAllFactors(LargeNumber));
                timer.Stop();
                Meth2Times.Add(timer.Elapsed.TotalMilliseconds);
            }
            Console.WriteLine("SmarterGetAllFactors average execution time: {0}",
                Meth1Times.Average(x => x));
            Console.WriteLine("EvenSmarterGetAllFactors average execution time: {0}",
                Meth2Times.Average(x => x));
            //Console.WriteLine("Smart: {0} {1} {2}", DateTime.Now, CommonFunctions.PrettyPrint(CommonFunctions.SmarterGetAllFactors(LargeNumber)), DateTime.Now);
            //Console.WriteLine("Smarter: {0} {1} {2}", DateTime.Now, CommonFunctions.PrettyPrint(CommonFunctions.EvenSmarterGetAllFactors(LargeNumber)), DateTime.Now);
            //Console.WriteLine("GCD of {0} and {1} is {2}", 2 * 2 * 3 * 3 * 5 * 11, 2 * 3 * 5 * 7 * 11 * 13, CommonFunctions.GCD(2 * 2 * 3 * 3 * 5 * 11, 2 * 3 * 5 * 7 * 11 * 13));
            Console.Read();
        }
    }
}
