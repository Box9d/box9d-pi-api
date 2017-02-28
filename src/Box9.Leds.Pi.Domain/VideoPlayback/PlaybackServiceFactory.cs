using System;
using Box9.Leds.Pi.Core.Config;
using Microsoft.Extensions.Options;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public class PlaybackServiceFactory : IPlaybackServiceFactory
    {
        private readonly IOptions<VideoPlayerOptions> options;

        public PlaybackServiceFactory(IOptions<VideoPlayerOptions> options)
        {
            this.options = options;
        }

        public Func<IPlaybackService> Playback
        {
            get
            {
                return () => options.Value.UseFadecandyServer
                    ? (IPlaybackService)new FadecandyPlaybackService(options)
                    : (IPlaybackService)new DummyPlaybackService();
            }
        }
    }
}
