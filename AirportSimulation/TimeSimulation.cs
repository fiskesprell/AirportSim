//Code inspired by "Discrete Event Simulation: A Population Growth Example" By Arnaldo Perez
//https://learn.microsoft.com/en-us/archive/msdn-magazine/2016/march/csharp-discrete-event-simulation-a-population-growth-example

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class TimeSimulation
    {
        public List<Flight> Flights { get; set; }
        public int ElapsedTime { get; set }
        private int _currenTime;

        public TimeSimulation(IEnumerable<Flight> flights, int elapsedTime)
        {
            Flights = new List<Flight>(flights);
            ElapsedTime = elapsedTime;
            _currentTime = 0;
        }
    }
}
