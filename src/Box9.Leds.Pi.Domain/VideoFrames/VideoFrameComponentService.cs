using Box9.Leds.Pi.Core.Mapping;
using Box9.Leds.Pi.DataAccess;
using Box9.Leds.Pi.DataAccess.Models;
using Box9.Leds.Pi.Domain.Componentization.Initializers;

namespace Box9.Leds.Pi.Domain.VideoFrames
{
    public class VideoFrameComponentService : IVideoFrameComponentService
    {
        private readonly IDatabaseFactory databaseFactory;

        public VideoFrameComponentService(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public VideoFrame Initialize(int id, IMappableTo<VideoFrameInitializer> request)
        {
            var videoFrame = new VideoFrame(new VideoFrameModel());
            videoFrame.Initialize(id, request.Map());
            return videoFrame;
        }
    }
}
