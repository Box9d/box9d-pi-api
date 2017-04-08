using System;
using System.Collections.Generic;
using System.Linq;
using Box9.Leds.Pi.DataAccess;
using Box9.Leds.Pi.DataAccess.Functions;
using Box9.Leds.Pi.DataAccess.Models;
using Box9.Leds.Pi.Domain.Componentization.Initializers;
using Box9.Leds.Pi.Domain.Dispatch;
using Box9.Leds.Pi.Core.Mapping;

namespace Box9.Leds.Pi.Domain.Videos
{
    public class VideoComponentService : IVideoComponentService
    {
        private readonly IDispatcher dispatcher;
        private readonly IDatabaseFactory databaseFactory;

        public VideoComponentService(IDatabaseFactory databaseFactory, IDispatcher dispatcher)
        {
            this.databaseFactory = databaseFactory;
            this.dispatcher = dispatcher;
        }

        public Video Initialize(int id, IMappableTo<VideoInitializer> videoInitializer)
        {
            var video = Video(new VideoMetadataModel());
            video.Initialize(id, videoInitializer.Map());

            using (var conn = databaseFactory.Database())
            {
                var existingVideosById = conn.GetVideoMetadataById(id);
                if (existingVideosById != null)
                {
                    throw new ArgumentException(string.Format("Video with Id '{0}' already exists", id));
                }
            }

            return video;
        }

        public Video GetById(int id)
        {
            using (var conn = databaseFactory.Database())
            {
                var video = conn.GetVideoMetadataById(id);
                if (video == null)
                {
                    throw new ArgumentException(string.Format("Video with Id '{0}' does not exist", id));
                }

                return Video(video);
            }
        }

        public IEnumerable<Video> GetAll()
        {
            using (var conn = databaseFactory.Database())
            {
                return conn
                    .GetAllVideoMetadatas()
                    .Select(vm => Video(vm));
            }
        }

        public void Save(Video video)
        {
            using (var conn = databaseFactory.Database())
            {
                var videoWithId = conn.GetVideoMetadataById(video.Id);
                if (videoWithId == null)
                {
                    conn.InsertVideoMetadata(video.Model);
                }
                else
                {
                    conn.UpdateVideoMetadata(video.Model);
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = databaseFactory.Database())
            {
                var video = conn.GetVideoMetadataById(id);
                if (video == null)
                {
                    throw new ArgumentException(string.Format("Video with Id '{0}' does not exist", id));
                }

                conn.DeleteVideoMetadata(video);
            }
        }

        private Video Video(VideoMetadataModel model)
        {
            return new Video(model, dispatcher);
        }
    }
}
