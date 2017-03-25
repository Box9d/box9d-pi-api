using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Box9.Leds.Pi.Api
{
    public static class FileServer
    {
        public static void UseCustomFileServer(this IAppBuilder builder)
        {
            var serverDirectory = Path.Combine(CurrentDirectory(), "Visualizer");

            var fileSystem = new PhysicalFileSystem(serverDirectory);
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = fileSystem,
            };
            options.StaticFileOptions.ServeUnknownFileTypes = true;

            builder.UseFileServer(options);
        }

        private static string CurrentDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            return Path.GetDirectoryName(path);
        }
    }
}