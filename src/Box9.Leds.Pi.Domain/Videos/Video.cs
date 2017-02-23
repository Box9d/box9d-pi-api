using Box9.Leds.Pi.Core.Validation;
using Box9.Leds.Pi.DataAccess.Models;
using Box9.Leds.Pi.Domain.Componentization;
using Box9.Leds.Pi.Domain.Componentization.Initializers;

namespace Box9.Leds.Pi.Domain
{
    public class Video : IInitializable<VideoInitializer>
    {
        internal VideoMetadataModel Model;

        public int Id { get { return Model.Id; } }

        public string FileName { get { return Model.Name; } }

        public double FrameRate { get { return Model.FrameRate; } }

        public int TotalFrames { get { return Model.TotalFrames; } }

        internal Video(VideoMetadataModel videoMetadataModel)
        {
            Model = videoMetadataModel;
        }

        public void Initialize(int id, VideoInitializer initializer)
        {
            Guard.This(id)
                .AgainstNegative("Video Id cannot be negative")
                .AgainstZero("Video Id cannot be '0'");

            Model.Id = id;

            SetFileName(initializer.FileName);
            SetFrameRate(initializer.FrameRate);
            SetTotalFrames(initializer.TotalFrames);
        }

        public void SetFileName(string fileName)
        {
            Guard.This(fileName)
                .AgainstNullOrEmpty("Video file name should not be empty");

            Model.Name = fileName;
        }

        public void SetFrameRate(double frameRate)
        {
            Guard.This(frameRate)
                .AgainstNegative("Frame rate cannot be negative")
                .AgainstZero("Frame rate cannot be '0'");

            Model.FrameRate = frameRate;
        }

        public void SetTotalFrames(int totalFrames)
        {
            Guard.This(totalFrames)
                .AgainstNegative("Total number of frames cannot be negative")
                .AgainstZero("Total number of frames cannot be '0'");

            Model.TotalFrames = totalFrames;
        }
    }
}
