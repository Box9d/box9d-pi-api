using Box9.Leds.Pi.Core.Mapping;
using Box9.Leds.Pi.Domain;

namespace Box9.Leds.Pi.Api.ApiResults
{
    public class VideoMetadataResult : IPopulatableFrom<Video>
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public double FrameRate { get; set; }

        public int TotalFrames { get; set; }

        public void Populate(Video source)
        {
            Id = source.Id;
            FileName = source.FileName;
            FrameRate = source.FrameRate;
            TotalFrames = source.TotalFrames;
        }
    }
}
