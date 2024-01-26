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
        public List<Gate> ConnectedGate = new List<Gate>();
        public List<Runway> ConnectedRunway = new List<Runway>();
        public Queue<Flight> TaxiQueue = new Queue<Flight>();
        public double TravelTime { get; set; } = 5; //TravelTime is a the amount of time it takes to get from gate to runway
        public bool IsAvailable { get; set; = true;


    }

}
