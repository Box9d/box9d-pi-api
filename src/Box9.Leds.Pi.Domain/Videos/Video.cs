using System;
using System.Collections.Generic;
using System.Linq;
using Box9.Leds.Pi.Core.Validation;
using Box9.Leds.Pi.DataAccess.Models;
using Box9.Leds.Pi.Domain.Componentization;
using Box9.Leds.Pi.Domain.Componentization.Initializers;
using Box9.Leds.Pi.Domain.Dispatch;
using Box9.Leds.Pi.Domain.VideoFrames;
using Box9.Leds.Pi.Domain.Videos;

namespace Box9.Leds.Pi.Domain
{
    public class Video : IInitializable<VideoInitializer>
    {
        private IEnumerable<VideoFrame> frames;

        private readonly IDispatcher dispatcher;
        internal VideoMetadataModel Model;

        public int Id { get { return Model.Id; } }

        public string FileName { get { return Model.Name; } }

        public double FrameRate { get { return Model.FrameRate; } }

        public int TotalFrames { get { return frames.Count(); } }

        internal Video(VideoMetadataModel videoMetadataModel, IDispatcher dispatcher)
        {
            Model = videoMetadataModel;

            this.dispatcher = dispatcher;

            frames = dispatcher.Dispatch(this.DispatchGetFramesForVideo());
        }

        public void Initialize(int id, VideoInitializer initializer)
        {
            Guard.This(id)
                .AgainstNegative("Video Id cannot be negative")
                .AgainstZero("Video Id cannot be '0'");

            Model.Id = id;

            SetFileName(initializer.FileName);
            SetFrameRate(initializer.FrameRate);
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

        public void AddFrames(IEnumerable<VideoFrame> framesToAdd)
        {
            foreach (var videoFrame in framesToAdd)
            {
                foreach (var occupiedPosition in frames.Select(fr => fr.Position))
                {
                    if (videoFrame.Position == occupiedPosition)
                    {
                        throw new ArgumentException(string.Format("Frame position {0} is already occupied for video", videoFrame.Position));
                    }
                }
            }

            dispatcher.Dispatch(this.DispatchAddFrames(framesToAdd));
            frames = dispatcher.Dispatch(this.DispatchGetFramesForVideo()); // Refresh frames after adding
        }

        public void ClearFrames()
        {
            dispatcher.Dispatch(this.DispatchClearFrames());
            frames = dispatcher.Dispatch(this.DispatchGetFramesForVideo()); // Refresh frames after clearing
        }
    }
}
