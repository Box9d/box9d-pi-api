using System.Threading.Tasks;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public interface IVideoPlayer
    {
        VideoPlaybackToken Load(Video video);

        Task PlayAsync(Video video, string playbackToken);

        void Stop(string playbackToken);
    }
}
