using System;
using CurrencyConverter.Extensions;

namespace CurrencyConverter.Application
{
    public interface ICurrencyConverter
    {
        ConversionResult Convert(int amount);
    }

    public struct ConversionResult
    {
        public ConversionResult(int integerPart, decimal decimalPart)
        {
            IntegerPart = integerPart;
            DecimalPart = decimalPart;
        }

        public readonly int     IntegerPart;
        public readonly decimal DecimalPart;
    }

    public class CurrencyConverter : ICurrencyConverter
    {
        private readonly decimal _rate;

        public CurrencyConverter(decimal rate)
        {
            _rate = rate;
        }

        public ConversionResult Convert(int amount)
        {
            try
            {
                var (res, change) = amount.Over(_rate);
                return new ConversionResult(res, change);
            }
            catch(Exception)
            {
                Console.WriteLine($"Invalid conversion {amount} to rate {_rate}!");
                return new ConversionResult(0, 0);
            }
        }
    }
}
