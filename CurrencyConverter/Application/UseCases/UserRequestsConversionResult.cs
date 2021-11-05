using CurrencyConverter.Application.Models;
using CurrencyConverter.Domain;

namespace CurrencyConverter.Application.UseCases
{
    public class UserRequestsConversionResult
    {
        public UserRequestsConversionResult(ConversionSummary summary, ConversionStatus result)
        {
            Summary = summary;
            Result  = result;
        }

        public ConversionSummary Summary { get; }
        public ConversionStatus  Result  { get; }
    }
}
