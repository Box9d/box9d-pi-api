using Autofac;
using Box9.Leds.Pi.DataAccess;
using Box9.Leds.Pi.Domain.Videos;

namespace Box9.Leds.Pi.Domain
{
    public class DomainAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VideoComponentService>().As<IVideoComponentService>();

            builder.RegisterModule(new DataAccessAutofacModule());
        }
    }
}
