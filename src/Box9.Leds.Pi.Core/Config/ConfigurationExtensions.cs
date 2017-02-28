using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Box9.Leds.Pi.Core.Config
{
    public static class ConfigurationExtensions
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfigurationRoot configRoot)
        {
            var test = configRoot.GetSection("VideoPlayback");

            services.Configure<DropBoxApiOptions>(options => configRoot.GetSection("Dropbox").Bind(options));
            services.Configure<VideoPlayerOptions>(options => configRoot.GetSection("VideoPlayback").Bind(options));
        }
    }
}
