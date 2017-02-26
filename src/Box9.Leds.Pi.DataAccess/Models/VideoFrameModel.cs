using Dapper.Contrib.Extensions;

namespace Box9.Leds.Pi.DataAccess.Models
{
    [Table("VideoFrame")]
    public class VideoFrameModel
    {
        [ExplicitKey]
        public int Id { get; set; }

        public int VideoId { get; set; }

        public byte[] BinaryData { get; set; }

        public int Position { get; set; }
    }
}
