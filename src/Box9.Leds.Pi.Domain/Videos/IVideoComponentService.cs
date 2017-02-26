using System.Collections.Generic;
using Box9.Leds.Pi.Domain.Componentization;
using Box9.Leds.Pi.Domain.Componentization.Initializers;

namespace Box9.Leds.Pi.Domain.Videos
{
    public interface IVideoComponentService : IInitializerComponentService<Video, VideoInitializer>
    {
        Video GetById(int id);

        IEnumerable<Video> GetAll();

        void Save(Video video);

        void Delete(int id);
    }
}
