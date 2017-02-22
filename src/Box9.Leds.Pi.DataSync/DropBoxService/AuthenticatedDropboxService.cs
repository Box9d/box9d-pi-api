using Dropbox.Api;

namespace Box9.Leds.Pi.DataSync.DropBoxSerivce
{
    public class AuthenticatedDropboxService : IDropBoxService
    {
        private readonly DropboxClient service;

        public AuthenticatedDropboxService(string accessToken)
        {
            service = new DropboxClient(accessToken);
        }

        public void Dispose()
        {
            service.Dispose();
        }
    }
}
