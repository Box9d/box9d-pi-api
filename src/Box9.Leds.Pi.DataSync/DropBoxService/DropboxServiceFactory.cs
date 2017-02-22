using Box9.Leds.Pi.Core.Config;
using Microsoft.Extensions.Options;

namespace Box9.Leds.Pi.DataSync.DropBoxSerivce
{
    public class DropboxServiceFactory : IDropBoxServiceFactory
    {
        private readonly DropBoxApiOptions options;

        public DropboxServiceFactory(IOptions<DropBoxApiOptions> options)
        {
            this.options = options.Value;
        }

        public IDropBoxService Initialize()
        {
            try
            {
                return new AuthenticatedDropboxService(options.AccessToken);
            }
            catch
            {
                return new UnauthenticatedDropboxService();
            }
        }
    }
}
