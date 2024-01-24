using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Taxi
    {
        public string Name { get; set; }
        public List<Gate> ConnectedGate { get; set; } 
        public List<Runway> ConnectedRunway { get; set; } 
        public Queue<Flight> TaxiQueue { get; set; }
        public double TravelTime { get; set; } = 5; //TravelTime is a the amount of time it takes to get from gate to runway
        public bool IsAvailable { get; set; = true;


    }

}
