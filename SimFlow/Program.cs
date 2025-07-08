using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using SimFlow;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddLogging(config =>
        {
            config.SetMinimumLevel(LogLevel.Information);
            
            config.AddSimpleConsole(options =>
            {
                options.TimestampFormat = "[dd-MM-yyyy HH:mm:ss] ";
                options.IncludeScopes = true;
                options.SingleLine = true;
            });
        });

        services.AddSingleton<Modbus>();
        services.AddSingleton<TimeBlockMonitor>();
        services.AddHostedService<App>(); // Your custom service
    })
    .Build();

host.Run();