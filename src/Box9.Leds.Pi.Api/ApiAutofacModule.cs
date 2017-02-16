using Autofac;
using Box9.Leds.Pi.Domain;

namespace Box9.Leds.Pi.Api.Autofac
{
    public class ApiAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DomainAutofacModule());
        }
    }
}