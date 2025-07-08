using EasyModbus;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimFlow
{
    public class Modbus
    {
        public ModbusServer Server { get; private set; } = new();
        private bool _runFlag;

        private readonly ILogger<TimeBlockMonitor> _logger;

        public Modbus(ILogger<TimeBlockMonitor> logger)
        {
            _logger = logger;
        }

        public void Start()
        {


            if (!_runFlag)
            {
                _logger.LogInformation("Starting Modbus interface");
                Server?.Listen();
                _runFlag = true;
            }

        }

        public void Stop()
        {
            if (_runFlag)
            {
                _logger.LogInformation("Stoping Modbus interface");
                Server?.StopListening();
                _runFlag = false;
            }
        }

        public void PublishBool(bool value, int address)
        {
            Server.coils[address] = value;
        }

    }
}
