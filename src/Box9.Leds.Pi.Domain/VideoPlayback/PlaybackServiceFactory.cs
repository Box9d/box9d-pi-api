using System;
using Box9.Leds.Pi.Core.Config;

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
                return () => GetPlaybackService();
            }
        }

        private IPlaybackService GetPlaybackService()
        {
            if (options.Value.UseFadecandyServer)
            {
                return new FadecandyPlaybackService(options);
            }

            return new SignalRPlaybackService();
        }
    }
}
