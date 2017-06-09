using System;
using System.Threading.Tasks;

namespace Box9.Leds.Pi.Domain.VideoPlayback
{
    public interface IVideoPlayer
    {
        void Load(Video video);

        Task Play(string timeReferenceUrl, DateTime? playAt);

        Task Stop();
    }
}
