namespace Box9.Leds.Pi.Domain.Componentization
{
    public interface IInitializable<TInitializer>
    {
        void Initialize(int id, TInitializer initializer);
    }
}
