namespace Box9.Leds.Pi.Core.Mapping
{
    public static class MappingExtensions
    {
        public static T Map<T>(this IMappableTo<T> source)
        {
            return source.Map();
        }

        public static void PopulateFrom<T>(this IPopulatableFrom<T> target, T source)
        {
            target.PopulateFrom(source);
        }
    }
}
