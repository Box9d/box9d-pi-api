using Box9.Leds.Pi.Core.Mapping;

namespace Box9.Leds.Pi.Domain.Componentization
{
    public interface IInitializableComponentService<TComponent, TInitializer>
        where TComponent : IInitializable<TInitializer>
    {
        TComponent Initialize(int id, IMappableTo<TInitializer> request);
    }
}
