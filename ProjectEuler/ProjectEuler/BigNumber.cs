using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectEuler
{
    class BigNumber
    {
        // Integer numbers only for now

        List<int> Value;

        bool IsNegative;

        private readonly string RegexPattern = @"";

        public BigNumber()
        { 
        
        }
        public BigNumber(string txtNumber) 
        {
            Regex TestIfInt = new Regex(RegexPattern);
        }

        public BigNumber(List<int> arrayNumber) 
        {
            Regex TestIfInt = new Regex(RegexPattern);
            Value = new List<int>();
            
        }

        public static BigNumber operator +(BigNumber arg1, BigNumber arg2) 
        {
            BigNumber result = new BigNumber();
            for (int i = 0; i < Math.Min(arg1.Value.Count, arg2.Value.Count); i++) 
            { }


           return result;
        }
        public static BigNumber operator -(BigNumber arg1, BigNumber arg2) 
        {
            BigNumber result = new BigNumber();
            return result;
        }
        public static BigNumber operator *(BigNumber arg1, BigNumber arg2) 
        {
            BigNumber result = new BigNumber();
            return result;
        }
        public static BigNumber operator /(BigNumber arg1, BigNumber arg2) 
        {
            BigNumber result = new BigNumber();
            return result;
        }
        public static BigNumber operator %(BigNumber arg1, BigNumber arg2)
        {
            BigNumber result = new BigNumber();
            return result;
        }
        public static BigNumber operator ^(BigNumber arg1, BigNumber arg2) 
        {
            BigNumber result = new BigNumber();
            return result;
        }

        private bool TryParse(string StringValue)
        {
            bool isBigNumber = false;

            return isBigNumber;
            
        }
    }
}
