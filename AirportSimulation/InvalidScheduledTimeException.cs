using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class InvalidScheduledTimeException : Exception
    {
        public InvalidScheduledTimeException() : base() { }

        public InvalidScheduledTimeException(string message) : base(message) { }

        public override string Message
        {
            get
            {
                return $"InvalidScheduledTimeException: {base.Message}";
            }
        }
    }
}
