using System.Collections.Generic;
using System.Data;
using Box9.Leds.Pi.DataAccess.Models;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Box9.Leds.Pi.DataAccess.Functions
{
    public static class VideoFrameFunctions
    {
        public static void InsertVideoFrame(this IDbConnection conn, VideoFrameModel frame, IDbTransaction transaction = null)
        {
            if (transaction == null)
            {
                conn.Insert(frame);
            }
            else
            {
                conn.Insert(frame, transaction);
            }
        }

        public static IEnumerable<VideoFrameModel> GetVideoFramesByVideoId(this IDbConnection conn, int videoId)
        {
            return conn.Query<VideoFrameModel>("SELECT * FROM VideoFrame WHERE VideoId = @videoId", new { videoId });
        }

        public static void DeleteVideoFramesByVideoId(this IDbConnection conn, int videoId)
        {
            conn.Execute("DELETE FROM VideoFrame WHERE VideoId = @videoId", new { videoId });
        }
    }
}
