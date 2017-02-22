using Autofac;
using Box9.Leds.Pi.DataSync.DropBoxSerivce;

namespace Box9.Leds.Pi.DataSync
{
    public class DataSyncAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataSyncManager>().As<IDataSyncManager>();
            builder.RegisterType<DropboxServiceFactory>().As<IDropBoxServiceFactory>();
        }
    }
}
