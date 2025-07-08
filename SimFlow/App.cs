using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SimFlow
{
    public class App(ILogger<App> logger, TimeBlockMonitor monitor, Modbus modbus) : IHostedService
    {
        private readonly ILogger<App> _logger = logger;
        private readonly TimeBlockMonitor _monitor = monitor;
        private readonly Modbus _modbus = modbus;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _monitor.LoadFromCsv("data.csv");

            _monitor.StartTimeReached += (s, e) =>
            {
                _logger.LogInformation("{Time} - Start: {Address}", DateTime.Now, e.Address);
                _modbus.PublishBool(true, e.Address);
            };

            _monitor.StopTimeReached += (s, e) =>
            {
                _logger.LogInformation("{Time} - Stop: {Address}", DateTime.Now, e.Address);
                _modbus.PublishBool(false, e.Address);
            };

            _modbus.Start();
            _monitor.Start();

            _logger.LogInformation("App started. Press Ctrl+C to exit.");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("App is stopping.");
            _modbus.Stop();
            _monitor.Stop();
            return Task.CompletedTask;
        }
    }
}
