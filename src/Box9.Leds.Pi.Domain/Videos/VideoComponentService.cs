﻿using System;
using System.Collections.Generic;
using System.Linq;
using Box9.Leds.Pi.Core.Mapping;
using Box9.Leds.Pi.DataAccess;
using Box9.Leds.Pi.DataAccess.Functions;
using Box9.Leds.Pi.DataAccess.Models;
using Box9.Leds.Pi.Domain.Componentization.Initializers;

namespace Box9.Leds.Pi.Domain.Videos
{
    public class VideoComponentService : IVideoComponentService
    {
        private readonly IDatabaseFactory databaseFactory;

        public VideoComponentService(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public Video Initialize(int id, IMappableTo<VideoInitializer> videoInitializer)
        {
            var videoModel = new VideoMetadataModel();
            var video = new Video(videoModel);
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

                return new Video(video);
            }
        }

        public IEnumerable<Video> GetAll()
        {
            using (var conn = databaseFactory.Database())
            {
                return conn
                    .GetAllVideoMetadatas()
                    .Select(vm => new Video(vm));
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
    }
}
