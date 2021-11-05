using Autofac;
using CurrencyConverter.Application.Services;
using CurrencyConverter.Application.UseCases;
using CurrencyConverter.Infrastructure;

namespace CurrencyConverter.Application
{
    public class ContainerRegistrations : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CurrencyConversionService>()
                   .As<ICurrencyConversionService>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<UserRequestsCurrencyConversion>()
                   .As<IUserRequestsCurrencyConversion>()
                   .InstancePerDependency();

            builder.RegisterType<CurrencyConverterProvider>()
                   .WithParameter("parser", new FileParser<CurrencyConversionDto[]>())
                   .As<ICurrencyConverterProvider>()
                   .InstancePerLifetimeScope();

            builder.RegisterType<ExchangeRateLocator>()
                   .WithParameter("parser", new FileParser<CurrencyConversionDto[]>())
                   .As<IExchangeRateLocator>()
                   .InstancePerLifetimeScope();
        }
    }
}
