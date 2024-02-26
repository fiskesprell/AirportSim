using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class InvalidInfrastructureException : Exception
    {
        public InvalidInfrastructureException() : base() { }

        public InvalidInfrastructureException(string message) : base(message) { }

        public override string Message
        {
            get
            {
                return $"InvalidInfrastructureException: {base.Message}";
            }
        }
    }
}
