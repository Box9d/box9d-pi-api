using System.Threading.Tasks;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public interface IVideoPlayer
    {
        VideoPlaybackToken Load(Video video);

        Task Play(Video video);

        Task Stop();
    }
}
