using System;
using System.Collections;

namespace LendingSystem.Extensions
{
    /// <summary>
    /// Extensions for decimal type
    /// </summary>
    public static class DecimalExtensions
    {
        /// <summary>
        /// From http://www.daimi.au.dk/~ivan/FastExpproject.pdf
        /// Left to Right Binary Exponentiation
        /// </summary>
        /// <param name="x">base</param>
        /// <param name="y">exponent</param>
        /// <returns></returns>
        public static decimal Pow(this decimal x, uint y)
        {
            decimal a = 1m;
            BitArray e = new BitArray(BitConverter.GetBytes(y));
            int t = e.Count;

            for (int i = t - 1; i >= 0; --i)
            {
                a *= a;
                if (e[i])
                {
                    a *= x;
                }
            }
            return a;
        }

        /// <summary>
        /// Rounds value to 2 decimal places
        /// </summary>
        /// <param name="input">value to be rounded</param>
        /// <returns>Rounded value</returns>
        public static decimal Round(this decimal input)
        {
            return decimal.Round(input, 2, MidpointRounding.AwayFromZero);
        }
    }
}
