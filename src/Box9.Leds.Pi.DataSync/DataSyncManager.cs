using System;
using Box9.Leds.Pi.Core.Config;
using Microsoft.Extensions.Options;

namespace Box9.Leds.Pi.DataSync
{
    public class DataSyncManager : IDataSyncManager
    {
        private readonly DropBoxApiOptions options;

        public DataSyncManager(IOptions<DropBoxApiOptions> options)
        {
            this.options = options.Value;
        }

        public void Synchronize()
        {
            // todo
            throw new NotImplementedException();
        }
    }
}
