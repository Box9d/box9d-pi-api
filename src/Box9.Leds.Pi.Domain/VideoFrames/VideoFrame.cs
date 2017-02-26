using Box9.Leds.Pi.Core.Validation;
using Box9.Leds.Pi.DataAccess.Models;
using Box9.Leds.Pi.Domain.Componentization;
using Box9.Leds.Pi.Domain.Componentization.Initializers;

namespace Box9.Leds.Pi.Domain.VideoFrames
{
    public class VideoFrame : IInitializable<VideoFrameInitializer>
    {
        internal VideoFrameModel Model { get; set; }

        public byte[] BinaryData { get { return Model.BinaryData; } }

        public int Position { get { return Model.Position; } }

        public int VideoId { get { return Model.VideoId; } }

        internal VideoFrame(VideoFrameModel model)
        {
            Model = model;
        }

        public void Initialize(int id, VideoFrameInitializer initializer)
        {
            Guard.This(id)
                .AgainstNegative("Video Id cannot be negative")
                .AgainstZero("Video Id cannot be '0'");

            Model.Id = id;

            SetPosition(initializer.Position);
            SetBinaryData(initializer.BinaryData);
        }

        public void SetPosition(int position)
        {
            Guard.This(position)
                .AgainstNegative("Frame position cannot be negative")
                .AgainstZero("Frame position cannot be '0'");

            Model.Position = position;
        }

        public void SetBinaryData(byte[] binaryData)
        {
            Model.BinaryData = binaryData;
        }

        internal void SetVideo(Video video)
        {
            Model.VideoId = video.Id;
        }
    }
}
