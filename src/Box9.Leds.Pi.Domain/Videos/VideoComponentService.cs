using System.Collections.Generic;
using System.Linq;
using Box9.Leds.Pi.DataAccess.DataInteractions;

namespace Box9.Leds.Pi.Domain.Videos
{
    public class VideoComponentService : IVideoComponentService
    {
        private readonly IVideoMetadataDataInteraction videoMetadataDataInteraction;

        public VideoComponentService(IVideoMetadataDataInteraction videoMetadataDataInteraction)
        {
            this.videoMetadataDataInteraction = videoMetadataDataInteraction;
        }

        public IEnumerable<Video> GetAll()
        {
            return videoMetadataDataInteraction.GetAll()
                .Select(vm => new Video(vm));
        }
    }
}
