using Autofac;
using Box9.Leds.Pi.DataAccess;
using Box9.Leds.Pi.Domain.Dispatch;
using Box9.Leds.Pi.Domain.Logging;
using Box9.Leds.Pi.Domain.VideoFrames;
using Box9.Leds.Pi.Domain.VideoPlayback;
using Box9.Leds.Pi.Domain.Videos;

namespace Box9.Leds.Pi.Domain
{
    public class DomainAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VideoComponentService>().As<IVideoComponentService>();
            builder.RegisterType<VideoFrameComponentService>().As<IVideoFrameComponentService>();
            builder.RegisterType<PlaybackServiceFactory>().As<IPlaybackServiceFactory>();
            builder.RegisterType<VideoPlayer>().As<IVideoPlayer>()
                .SingleInstance(); // Persist Video play, stop etc functionality across API requests
            builder.RegisterType<Log>().As<ILog>().SingleInstance();
            builder.RegisterType<VideoPlayerMonitor>().As<IVideoPlayerMonitor>();
            builder.RegisterType<Dispatcher>().As<IDispatcher>();

            builder.RegisterModule(new DataAccessAutofacModule());
        }
    }
}
