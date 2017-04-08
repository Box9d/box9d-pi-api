namespace Box9.Leds.Pi.Core.Mapping
{
    public interface IPopulatableFrom<T>
    {
        void Populate(T source);
    }
}
