using System.Reflection;
using Hocon.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Src.Extensions;

public static class HoconReaderExtension
{
    public static IHostBuilder ConfigureHoconReader(this IHostBuilder host, string[] args)
    {
        host.ConfigureAppConfiguration((hostingContext, config) =>
        {
            {
                var env = hostingContext.HostingEnvironment;

                config.AddHoconFile(
                        "appSettings.conf",
                        optional: false,
                        reloadOnChange: false
                    )
                    .AddHoconFile(
                        "appSettings.Production.conf",
                        optional: true,
                        reloadOnChange: false)
                    .AddHoconFile(
                        "appSettings.Staging.conf",
                        optional: true,
                        reloadOnChange: false)
                    .AddHoconFile(
                        $"appSettings.{env.EnvironmentName}.conf",
                        optional: true,
                        reloadOnChange: true);

                if (env.IsDevelopment() || env.IsEnvironment("Local"))
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    
                    config.AddUserSecrets(appAssembly, optional: true);
                }
                
                config.AddEnvironmentVariables();
                
                config.AddCommandLine(args);
            }
        });
        return host;
    }
}