using Autofac;

namespace CurrencyConverter.Presentation
{
    public class ContainerRegistrations : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new TerminalInputOutput())
                   .As<IInputOutput>();

            builder.RegisterType<Screen>()
                   .InstancePerDependency();
        }
    }
}
