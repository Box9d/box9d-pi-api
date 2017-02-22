using Box9.Leds.Pi.DataSync.DropBoxSerivce;

namespace Box9.Leds.Pi.DataSync
{
    public interface IDropBoxServiceFactory
    {
        IDropBoxService Initialize();
    }
}
