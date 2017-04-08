using Box9.Leds.Pi.Core.Mapping;
using Box9.Leds.Pi.Domain.VideoPlayback;

namespace Box9.Leds.Pi.Api.ApiResults
{
    public class LoadVideoPlaybackResult : IPopulatableFrom<VideoPlaybackToken>
    {
        public string PlaybackToken { get; set; }

        public void Populate(VideoPlaybackToken source)
        {
            PlaybackToken = source.Token;
        }
    }
}
