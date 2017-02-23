namespace Box9.Leds.Pi.Database.Scripts
{
    public class _0002_AddVideoTableColumns : IScript
    {
        public int Id
        {
            get
            {
                return 2;
            }
        }

        public string Name
        {
            get
            {
                return "Add video table columns";
            }
        }

        public string Sql
        {
            get
            {
                return "ALTER TABLE Video ADD COLUMN framerate REAL;"
                    + "ALTER TABLE Video ADD COLUMN totalframes INT;";
            }
        }
    }
}
