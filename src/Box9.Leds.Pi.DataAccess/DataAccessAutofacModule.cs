using Autofac;
using Box9.Leds.Pi.DataAccess.DataInteractions;
using Box9.Leds.Pi.Database;

namespace Box9.Leds.Pi.DataAccess
{
    public class DataAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            /* Single instance of database factory so that update scripts are only run once per application lifetime */
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>()
                .SingleInstance();

            builder.RegisterType<VideoMetadataDataInteraction>().As<IVideoMetadataDataInteraction>();

            builder.RegisterModule(new DatabaseAutofacModule());
        }
    }
}
