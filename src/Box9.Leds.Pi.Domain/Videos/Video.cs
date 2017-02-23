using Box9.Leds.Pi.DataAccess.Models;

namespace Box9.Leds.Pi.Domain
{
    public class Video
    {
        private VideoMetadataModel videoMetadataModel;

        public string FilePath
        {
            get
            {
                return videoMetadataModel.FileName;
            }
        }

        internal Video(VideoMetadataModel videoMetadataModel)
        {
            this.videoMetadataModel = videoMetadataModel;
        }
    }
}
