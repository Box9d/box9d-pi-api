using System;
using System.Collections.Generic;
using System.Linq;
using Box9.Leds.Pi.Core.Mapping;
using Box9.Leds.Pi.DataAccess;
using Box9.Leds.Pi.DataAccess.Functions;
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

        public void AddToVideo(IEnumerable<VideoFrame> frames, Video video)
        {
            using (var conn = databaseFactory.Database())
            {
                var transaction = conn.BeginTransaction();

                try
                {
                    foreach (var frame in frames)
                    {
                        frame.SetVideo(video);
                        conn.InsertVideoFrame(frame.Model, transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void ClearVideoFrames(Video video)
        {
            using (var conn = databaseFactory.Database())
            {
                conn.DeleteVideoFramesByVideoId(video.Id);
            }
        }

        public IEnumerable<VideoFrame> GetAllForVideo(Video video)
        {
            using (var conn = databaseFactory.Database())
            {
                return conn
                    .GetVideoFramesByVideoId(video.Id)
                    .Select(f => new VideoFrame(f));
            }
        }
    }
}
