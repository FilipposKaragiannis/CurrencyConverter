using CurrencyConverter.Domain;

namespace CurrencyConverter.Application.UseCases
{
    public class UserRequestsConversionRequest
    {
        public UserRequestsConversionRequest(Asset asset, string target)
        {
            Asset  = asset;
            Target = target;
        }

        public Asset  Asset  { get; }
        public string Target { get; }
    }
}
