using System;
using System.Collections.Generic;
using CurrencyConverter.Infrastructure;

namespace CurrencyConverter.Application
{
    public interface IExchangeRateLocator
    {
        IEnumerable<string> Assets { get; }
        IEnumerable<string> Targets(string source);
    }

    public class ExchangeRateLocator : IExchangeRateLocator
    {
        private readonly HashSet<string>                  assets;
        private readonly Dictionary<string, List<string>> sourceMap;

        public ExchangeRateLocator(FileParser<CurrencyConversionDto[]> parser)
        {
            assets    = new HashSet<string>();
            sourceMap = new Dictionary<string, List<string>>();

            var rates = parser.Parse("ExchangeRates.json", typeof(Rates.ExchangeRateLocator));
            PopulateData(rates ?? Array.Empty<CurrencyConversionDto>());
        }

        public IEnumerable<string> Assets => assets;

        public IEnumerable<string> Targets(string source)
        {
            if(sourceMap.TryGetValue(source, out var res))
                return res;

            throw new ApplicationException($"There are no targets available for {source}");
        }

        private void PopulateData(IEnumerable<CurrencyConversionDto> rates)
        {
            foreach(var r in rates)
            {
                assets.Add(r.Source);

                if(sourceMap.ContainsKey(r.Source))
                {
                    var l = sourceMap[r.Source];
                    l.Add(r.Target);
                    sourceMap[r.Source] = l;
                }
                else
                {
                    sourceMap[r.Source] = new List<string> { r.Target };
                }
            }
        }
    }
}
