using System;

namespace CurrencyConverter.Extensions
{
    public static class AssetExtensions
    {
        public static (int, decimal) Over(this int amount, decimal rate)
        {
            var res = amount / rate;

            var intAmount = (int)Math.Round(res, 0, MidpointRounding.ToZero);
            var change    = res - intAmount;
            return (intAmount, change);
        }

        public static double Over(this int numerator, double denominator, double safeValue = 0)
        {
            try
            {
                if(denominator == 0)
                    return safeValue;

                return numerator / denominator;
            }
            catch(DivideByZeroException)
            {
                return safeValue;
            }
        }
    }
}
