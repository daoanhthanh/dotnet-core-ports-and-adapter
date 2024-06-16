using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Src.Extensions;

namespace Src;

public class Program
{
    public static async Task Main(string[] args)
    {
        var app = CreateHostBuilder(args).Build();

        await app.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => { webBuilder.UseStartup<Startup>(); }
            )
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddConsole();
            })
            .ConfigureHoconReader(args);
}