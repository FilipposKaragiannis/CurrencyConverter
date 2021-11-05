using CurrencyConverter.Domain;

namespace CurrencyConverter.Application.Models
{
    public class ConversionSummary
    {
        public ConversionSummary(Asset sourceAsset, Asset destAsset, decimal change)
        {
            SourceAsset = sourceAsset;
            DestAsset   = destAsset;
            Change      = change;
        }

        public Asset   SourceAsset { get; }
        public Asset   DestAsset   { get; }
        public decimal Change      { get; }
    }
}
