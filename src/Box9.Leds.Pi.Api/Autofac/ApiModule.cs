using Autofac;
using Box9.Leds.Pi.Core;
using Box9.Leds.Pi.Domain;

namespace Box9.Leds.Pi.Api.Autofac
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<DomainAutofacModule>();
            builder.RegisterModule<CoreAutofacModule>();
        }
    }
}