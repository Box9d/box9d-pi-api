using System.Collections.Generic;
using Box9.Leds.Pi.DataAccess.Models;

namespace Box9.Leds.Pi.DataAccess.DataInteractions
{
    public interface IVideoMetadataDataInteraction
    {
        IEnumerable<VideoMetadataModel> GetAll();
    }
}
