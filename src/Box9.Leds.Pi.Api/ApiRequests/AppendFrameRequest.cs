using Box9.Leds.Pi.Domain.Componentization.Initializers;
using SimpleMapping;

namespace Box9.Leds.Pi.Api.ApiRequests
{
    public class AppendFrameRequest : IMappableTo<VideoFrameInitializer>
    {
        public int Position { get; set; }

        public byte[] BinaryData { get; set; }

        public VideoFrameInitializer Map()
        {
            return new VideoFrameInitializer
            {
                BinaryData = BinaryData,
                Position = Position
            };
        }
    }
}
