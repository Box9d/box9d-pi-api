﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Box9.Leds.Pi.DataAccess.Functions;
using Box9.Leds.Pi.Domain.VideoFrames;
using Box9.Leds.Pi.DataAccess.Models;

namespace Box9.Leds.Pi.Domain.Videos
{
    internal static class VideoActions
    {
        internal static Action<IDbConnection> DispatchAddFrames(this Video video, IEnumerable<VideoFrame> framesToAdd)
        {
            return (IDbConnection conn) =>
            {
                var transaction = conn.BeginTransaction();

                var nextId = conn.GetNextId<VideoFrameModel>();

                try
                {
                    foreach (var frame in framesToAdd)
                    {
                        frame.SetVideo(video);
                        frame.Model.Id = nextId;
                        conn.InsertVideoFrame(frame.Model, transaction);

                        nextId++;
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            };
        }

        internal static Action<IDbConnection> DispatchClearFrames(this Video video)
        {
            return (IDbConnection conn) =>
            {
                conn.DeleteVideoFramesByVideoId(video.Id);
            };
        }
    }
}
