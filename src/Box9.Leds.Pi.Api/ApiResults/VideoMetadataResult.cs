using Box9.Leds.Pi.Core.Mapping;
using Box9.Leds.Pi.Domain;

namespace Box9.Leds.Pi.Api.ApiResults
{
    public class VideoMetadataResult : IPopulatableFrom<Video>
    {
        public string FilePath { get; set; }

        public void PopulateFrom(Video source)
        {
            FilePath = source.FilePath;
        }
    }
}
