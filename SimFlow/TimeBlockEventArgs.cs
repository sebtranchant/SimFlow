using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimFlow
{
    public class TimeBlockEventArgs : EventArgs
    {
        public short Address { get; }

        public TimeBlockEventArgs(short address)
        {
            Address = address;
        }
    }
}
