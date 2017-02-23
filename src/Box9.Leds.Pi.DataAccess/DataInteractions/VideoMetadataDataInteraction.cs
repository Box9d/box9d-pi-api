using System.Collections.Generic;
using Box9.Leds.Pi.DataAccess.Models;
using Dapper;

namespace Box9.Leds.Pi.DataAccess.DataInteractions
{
    public class VideoMetadataDataInteraction : IVideoMetadataDataInteraction
    {
        private readonly IDatabaseFactory databaseFactory;

        public VideoMetadataDataInteraction(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        public IEnumerable<VideoMetadataModel> GetAll()
        {
            using (var conn = databaseFactory.Database())
            {
                return conn.Query<VideoMetadataModel>("SELECT * FROM Video");
            }
        }
    }
}
