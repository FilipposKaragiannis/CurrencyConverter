using System;
using CurrencyConverter.Application.Models;
using CurrencyConverter.Application.UseCases;
using CurrencyConverter.Domain;
using CurrencyConverter.Exceptions;

namespace CurrencyConverter.Application.Services
{
    public interface ICurrencyConversionCallbacks
    {
        void InvalidConversion(string source, string target);
        void UnknownError(string      source, string target);
    }

    public interface ICurrencyConversionService
    {
        public ConversionSummary Convert(string source, string target, int amount);
    }

    public class CurrencyConversionService : ICurrencyConversionService
    {
        private readonly IUserRequestsCurrencyConversion _userRequestsCurrencyConversion;

        public CurrencyConversionService(IUserRequestsCurrencyConversion userRequestsCurrencyConversion)
        {
            _userRequestsCurrencyConversion = userRequestsCurrencyConversion;
        }

        public ConversionSummary Convert(string source, string target, int amount)
        {
            var sourceAsset = new Asset(amount, source);

            var request = new UserRequestsConversionRequest(sourceAsset, target);
            try
            {
                var result = _userRequestsCurrencyConversion.Execute(request);

                return result.Summary;
            }
            catch(InvalidConversionException)
            {
                Console.Error.Write($"Conversion from {source} to {target} does not exist");
            }
            catch(Exception)
            {
                Console.Error.Write($"Something went wrong trying to convert {source} => {target}");
            }

            return new ConversionSummary(sourceAsset, new Asset(), 0);
        }
    }
}
