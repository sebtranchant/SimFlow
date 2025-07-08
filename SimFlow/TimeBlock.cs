using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimFlow
{
    internal class TimeBlock
    {
        public DateTime StartTime { get; set; }
        public DateTime StopTime { get; set; }
        public short Address { get; set; }
        public DateTime? StartTriggered { get; set; }
        public DateTime? StopTriggered { get; set; }

        // Optional: Constructor for convenience
        public TimeBlock(DateTime startTime, DateTime stopTime, short address)
        {
            StartTime = startTime;
            StopTime = stopTime;
            Address = address;
           
        }

        public override string ToString()
        {
            return $"* Start time {StartTime.ToString("T")} / Stop time {StopTime.ToString("T")} / Adresse : {Address.ToString()} *";
        }
    
    
    }


}
