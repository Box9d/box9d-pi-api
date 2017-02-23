using System.Collections.Generic;

namespace Box9.Leds.Pi.Domain.Videos
{
    public interface IVideoComponentService
    {
        IEnumerable<Video> GetAll();
    }
}
