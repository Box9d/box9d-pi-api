using Box9.Leds.Pi.Core.Identifiers;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class VideoPlaybackToken
    {
        public string Token { get; }

        internal VideoPlaybackToken(string token)
        {
            Token = token;
        }
    }
}
