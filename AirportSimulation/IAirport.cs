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
        private int CountDays = 0;
        private int CountHours = 0;
        private int CountMinutes = 0;

        void AddRunway(Runway runway);
        void AddTerminal(Terminal terminal);
        void AddFlight(Flight flight);
        void AddGate(Gate gate);
        void SimulateTime(int days, int hours, int minutes);

    }
}
