using System.Collections.Generic;

namespace Box9.Leds.Pi.Database
{
    public interface IScriptDiscovery
    {
        IEnumerable<IScript> Discover();
    }
}
