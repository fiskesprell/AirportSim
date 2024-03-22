using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class InsufficientResourceException : Exception
    {
        public InsufficientResourceException() : base() { }

        public InsufficientResourceException(string message) : base(message) { }

        public override string Message
        {
            get
            {
                return $"InsufficientResourceException: {base.Message}";
            }
        }
    }
}
