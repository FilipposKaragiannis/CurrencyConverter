using Autofac;
using CurrencyConverter.Application.Models;
using CurrencyConverter.Application.Services;
using CurrencyConverter.Presentation;
using PresentationRegistrations = CurrencyConverter.Presentation.ContainerRegistrations;
using ApplicationRegistrations = CurrencyConverter.Application.ContainerRegistrations;

namespace CurrencyConverter
{
    public class ProgramExecutor : IScreenCallbacks
    {
        private ICurrencyConversionService _conversionService;

        public ProgramExecutor()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ApplicationRegistrations());
            builder.RegisterModule(new PresentationRegistrations());

            Container = builder.Build();
        }

        private static IContainer Container { get; set; }

        public ConversionSummary Convert(string source, string target, int amount)
        {
            return _conversionService.Convert(source, target, amount);
        }

        public void Run()
        {
            using var scope = Container.BeginLifetimeScope();

            var screen = scope.Resolve<Screen>();

            _conversionService = scope.Resolve<ICurrencyConversionService>();
            screen.Register(this);

            screen.Start();
        }
    }
}
