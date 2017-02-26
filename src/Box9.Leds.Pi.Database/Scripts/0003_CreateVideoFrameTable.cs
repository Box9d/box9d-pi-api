namespace Box9.Leds.Pi.Database.Scripts
{
    public class _0003_CreateVideoFrameTable : IScript
    {
        public int Id
        {
            get
            {
                return 3;
            }
        }

        public string Name
        {
            get
            {
                return "Create video frame table ";
            }
        }

        public string Sql
        {
            get
            {
                return "CREATE TABLE VideoFrame (id INTEGER PRIMARY KEY NOT NULL, videoid INTEGER NOT NULL, position INTEGER, binarydata NONE)";
            }
        }
    }
}
