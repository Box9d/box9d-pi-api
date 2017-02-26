using System.Collections.Generic;
using Box9.Leds.Pi.Domain.Componentization;
using Box9.Leds.Pi.Domain.Componentization.Initializers;

namespace Box9.Leds.Pi.Domain.VideoFrames
{
    public interface IVideoFrameComponentService : IInitializerComponentService<VideoFrame, VideoFrameInitializer>
    {
        IEnumerable<VideoFrame> GetAllForVideo(Video video);

        void AddToVideo(IEnumerable<VideoFrame> frames, Video video);

        void ClearVideoFrames(Video video);
    }
}
