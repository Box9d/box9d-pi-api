namespace Box9.Leds.Pi.Database.Scripts
{
    public class _0001_CreateVersioningTable : IScript
    {
        public string Name
        {
            get
            {
                return "Create versioning table";
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
                return "CREATE TABLE Video(Id INTERGER PRIMARY KEY, name TEXT NOT NULL)";
            }
        }
    }
}
