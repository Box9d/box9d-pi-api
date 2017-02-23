using System.Collections.Generic;
using System.Data;
using Box9.Leds.Pi.DataAccess.Models;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Box9.Leds.Pi.DataAccess.Functions
{
    public static class VideoMetadataFunctions
    {
        public static IEnumerable<VideoMetadataModel> GetAllVideoMetadatas(this IDbConnection dbConnection)
        {
            return dbConnection.GetAll<VideoMetadataModel>();
        }

        public static VideoMetadataModel GetVideoMetadataById(this IDbConnection dbConnection, int id)
        {
            return dbConnection.Get<VideoMetadataModel>(id);
        }

        public static void UpdateVideoMetadata(this IDbConnection dbConnection, VideoMetadataModel model)
        {
            dbConnection.Update(model);
        }

        public static void InsertVideoMetadata(this IDbConnection dbConnection, VideoMetadataModel model)
        {
            dbConnection.Insert(model);
        }

        public static void DeleteVideoMetadata(this IDbConnection dbConnection, VideoMetadataModel model)
        {
            dbConnection.Delete(model);
        }
    }
}
