using Autofac;

namespace Box9.Leds.Pi.Database
{
    public class DatabaseAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ScriptDiscovery>().As<IScriptDiscovery>();
        }
    }
}
