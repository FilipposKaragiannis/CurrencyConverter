using System;
using CurrencyConverter.Application.Models;
using CurrencyConverter.Domain;
using CurrencyConverter.Exceptions;

namespace CurrencyConverter.Application.UseCases
{
    public interface IUserRequestsCurrencyConversion
    {
        UserRequestsConversionResult Execute(UserRequestsConversionRequest request);
    }

    public class UserRequestsCurrencyConversion : IUserRequestsCurrencyConversion
    {
        private readonly ICurrencyConverterProvider _converterProvider;

        public UserRequestsCurrencyConversion(ICurrencyConverterProvider converterProvider)
        {
            _converterProvider = converterProvider;
        }

        public UserRequestsConversionResult Execute(UserRequestsConversionRequest request)
        {
            var converter = _converterProvider.Get(request.Asset.Ccy, request.Target);

            if(converter == null)
                throw new InvalidConversionException();

            try
            {
                var conversion = converter.Convert(request.Asset.Amount);
                return new UserRequestsConversionResult(new ConversionSummary(request.Asset,
                                                                              new Asset(conversion.IntegerPart, request.Target),
                                                                              conversion.DecimalPart),
                                                        ConversionStatus.Success);
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return new UserRequestsConversionResult(new ConversionSummary(request.Asset, new Asset(), 0),
                                                        ConversionStatus.InvalidConversion);
            }
        }
    }
}
