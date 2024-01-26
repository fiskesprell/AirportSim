using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal interface IAirport
    {
        /// <summary>
        /// The name of your Airport.
        /// </summary>
        public string name;
        public List<Taxi> Taxiways { get; set; }
        public List<Runway> Runways { get; set; }
        public List<Gate> Gates { get; set; }
        public List<Flight> Flights { get; set; }

        void AddRunway(Runway runway);
        void AddTerminal(Terminal terminal);
        void AddFlight(Flight flight);
        void AddGate(Gate gate);

    }
}
