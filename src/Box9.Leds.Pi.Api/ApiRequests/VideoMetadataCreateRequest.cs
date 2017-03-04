using Box9.Leds.Pi.Domain.Componentization.Initializers;
using SimpleMapping;

namespace Box9.Leds.Pi.Api.ApiRequests
{
    public class VideoMetadataCreateRequest : IMappableTo<VideoInitializer>
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public double FrameRate { get; set; }

        public VideoInitializer Map()
        {
            return new VideoInitializer
            {
                FileName = FileName,
                FrameRate = FrameRate,
            };
        }
    }
}
