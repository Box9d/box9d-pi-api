using Autofac;
using Box9.Leds.Pi.Core.Config;

namespace Box9.Leds.Pi.Core
{
    public class CoreAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Options<>))
                .As(typeof(IOptions<>))
                .InstancePerDependency();
        }
    }
}
