using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Flight
    {
        public string Number { get; set; }
        public string Company { get; set; }
        public GateLicence FlightType { get; set; }
        public Gate AssignedGate { get; set; }
        public bool IsInternational { get; set; } = false;
    }
}
