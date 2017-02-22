using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Box9.Leds.Pi.Core.Config
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfigurationRoot configRoot)
        {
            services.Configure<DropBoxApiOptions>(configRoot.GetSection("Dropbox"));
        }
    }
}
