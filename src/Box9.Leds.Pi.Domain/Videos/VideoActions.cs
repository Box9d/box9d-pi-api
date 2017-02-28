using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Box9.Leds.Pi.DataAccess.Functions;
using Box9.Leds.Pi.Domain.VideoFrames;

namespace Box9.Leds.Pi.Domain.Videos
{
    internal static class VideoActions
    {
        internal static Action<IDbConnection> DispatchAddFrames(this Video video, IEnumerable<VideoFrame> framesToAdd)
        {
            return (IDbConnection conn) =>
            {
                var transaction = conn.BeginTransaction();

                try
                {
                    foreach (var frame in framesToAdd)
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
            };
        }

        internal static Func<IDbConnection, IEnumerable<VideoFrame>> DispatchGetFramesForVideo(this Video video)
        {
            return (IDbConnection conn) =>
            {
                return conn.GetVideoFramesByVideoId(video.Id)
                    .Select(f => new VideoFrame(f));
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
