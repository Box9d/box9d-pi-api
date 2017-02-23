namespace Box9.Leds.Pi.Database
{
    public interface IScript
    {
        string Name { get; }

        string Sql { get; }

        int Id { get; }
    }
}
