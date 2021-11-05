using System;
using System.Collections.Generic;
using CurrencyConverter.Extensions;
using CurrencyConverter.Infrastructure;

namespace CurrencyConverter.Application
{
    public interface ICurrencyConverterProvider
    {
        public ICurrencyConverter Get(string source, string target);
    }

    public class CurrencyConverterProvider : ICurrencyConverterProvider
    {
        private const    string                                 KeySeparator = "-";
        private readonly Dictionary<string, ICurrencyConverter> _converters;

        public CurrencyConverterProvider(FileParser<CurrencyConversionDto[]> parser)
        {
            _converters = new Dictionary<string, ICurrencyConverter>();

            var rates = parser.Parse("ExchangeRates.json", typeof(Rates.ExchangeRateLocator));

            foreach(var r in rates ?? Array.Empty<CurrencyConversionDto>())
            {
                var key  = GetKey(r.Source, r.Target);
                var rate = r.SourceAmount.Over((double)r.TargetAmount);
                _converters[key] = new CurrencyConverter((decimal)rate);
            }
        }

        public ICurrencyConverter Get(string source, string target)
        {
            return _converters.TryGetValue(GetKey(source, target), out var converter) ? converter : null;
        }

        private static string GetKey(string source, string target)
        {
            return $"{source}{KeySeparator}{target}";
        }
    }
}
