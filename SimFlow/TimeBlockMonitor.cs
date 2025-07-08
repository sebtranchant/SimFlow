using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SimFlow
{
    public class TimeBlockMonitor
    {
        private readonly List<TimeBlock> Timeblocks = new();
        private readonly System.Timers.Timer _timer = new(1000);
        private readonly object _lock = new object();

        public event EventHandler<TimeBlockEventArgs>? StartTimeReached;
        public event EventHandler<TimeBlockEventArgs>? StopTimeReached;

        private readonly ILogger<TimeBlockMonitor> _logger;

        public TimeBlockMonitor(ILogger<TimeBlockMonitor> logger)
        {
            _logger = logger;
        }

        public void LoadFromCsv(string filePath)
        {
            _logger.LogInformation("Loading data from CSV: {Path}", filePath);
            using StreamReader reader = new(filePath);
            while (!reader.EndOfStream)
            {
                string? line = reader.ReadLine();
                if (line == null) continue;

                string[] values = line.Split(';');
                if (values.Length >= 3 &&
                    DateTime.TryParse(values[0], out DateTime start) &&
                    DateTime.TryParse(values[1], out DateTime stop))
                {
                    short address = short.Parse(values[2]);
                    Timeblocks.Add(new TimeBlock(start, stop, address));
                    
                }
            }
            foreach (var block in Timeblocks)
            {
                _logger.LogInformation("List of TimeBlock configured");
               _logger.LogInformation(block.ToString());
            }
        }
        public void Start()
        {
            _logger.LogInformation("Starting timer...");
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
        }

        public void Stop()
        {
            _logger.LogInformation("Stoping timer...");
            _timer.Elapsed -= _timer_Elapsed;
            _timer.Stop();
        }

        private void _timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            lock (_lock)
            {
                DateTime now = DateTime.Now;

                foreach (var block in Timeblocks)
                {
                    if (CanTriggerStart(block) && Math.Abs((block.StartTime - now).TotalSeconds) < 1)
                    {
                        block.StartTriggered = now;
                        _logger.LogInformation($"StartTriggered : {block.StartTriggered}");
                        StartTimeReached?.Invoke(this, new TimeBlockEventArgs(block.Address));
                        
                    }
                    if (CanTriggerStop(block) && Math.Abs((block.StopTime - now).TotalSeconds) < 1)
                    {
                        block.StopTriggered = now;
                        _logger.LogInformation($"StopTriggered : {block.StartTriggered}");
                        StopTimeReached?.Invoke(this, new TimeBlockEventArgs(block.Address));
                        
                    }
                }
            }
        }

        private static bool CanTriggerStop(TimeBlock block)
        {
            return block.StopTriggered == null || (DateTime.Now - block.StopTriggered.Value).TotalHours >= 24;
        }

        private static bool CanTriggerStart(TimeBlock block)
        {
            return block.StartTriggered == null || (DateTime.Now - block.StartTriggered.Value).TotalHours >= 24;
        }
    }
}
