using Box9.Leds.Pi.Core.Mapping;
using Box9.Leds.Pi.Domain.Componentization.Initializers;

namespace Box9.Leds.Pi.Api.ApiRequests
{
    public class VideoMetadataCreateRequest : IMappableTo<VideoInitializer>
    {
        public string FileName { get; set; }

        public double FrameRate { get; set; }

        public int TotalFrames { get; set; }

        public VideoInitializer Map()
        {
            return new VideoInitializer
            {
                FileName = FileName,
                FrameRate = FrameRate,
                TotalFrames = TotalFrames
            };
        }
    }
}
