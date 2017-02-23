namespace Box9.Leds.Pi.Database.Scripts
{
    public class _0001_CreateVideoTable : IScript
    {
        public string Name
        {
            get
            {
                return "Create video table";
            }
        }

        public int Id
        {
            get
            {
                return 1;
            }
        }

        public string Sql
        {
            get
            {
                return "CREATE TABLE Video(id INTERGER PRIMARY KEY NOT NULL, name NVARCHAR(255) NOT NULL)";
            }
        }
    }
}
