using SimpleMapping;

namespace Box9.Leds.Pi.Domain.Componentization
{
    public interface IInitializerComponentService<TComponent, TInitializer>
        where TComponent : IInitializable<TInitializer>
    {
        TComponent Initialize(int id, IMappableTo<TInitializer> request);
    }
}
