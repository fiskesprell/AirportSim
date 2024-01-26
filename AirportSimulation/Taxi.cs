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

        public Taxi(string name)
        {
            Name = name;
        }

        public void addToQueue(Flight flight)
        {
            TaxiQueue.Enqueue(flight);
        }

        public void removeFromQueue() 
        { 
            TaxiQueue.Dequeue(flight); 
        }

        public int lengthQueue()
        {
            return TaxiQueue.Count;
        }

        public void addConnectedGate(Gate gate)
        {
            ConnectedGate.Add(gate);
        }

        public void removeConnectedGate(Gate gate)
        {
            ConnectedGate.Remove(gate);
        }

        public void addConnectedRunway(Runway runway)
        {
            ConnectedRunway.Add(runway);
        }

        public void removeConnectedRunway(Runway runway)
        {
            ConnectedRunway.Remove(runway);
        }

    }

}
