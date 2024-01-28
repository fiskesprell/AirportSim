using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Taxi
    {
        /// <summary>
        /// The name of your Taxiway.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of gates connected to this taxiway.
        /// </summary>
        public List<Gate> ConnectedGate = new List<Gate>();
        /// <summary>
        /// List of runways connected to this taxiway.
        /// </summary>
        public List<Runway> ConnectedRunway = new List<Runway>();
        /// <summary>
        /// Queue of flights that wish to use the taxiway.
        /// </summary>
        public Queue<Flight> TaxiQueue = new Queue<Flight>();
        /// <summary>
        /// Amount of time it takes for plane to get from gate to the runway through the taxiway. <br/>
        /// In seconds. Average value is 60.
        /// </summary>
        public double TravelTime { get; set; } = 60; //TravelTime is a the amount of time it takes to get from gate to runway
        // TODO: Skal dette være sekunder? Er 60 en reasonable standardverdi?
        /// <summary>
        /// Tells you if the taxiway is available or not. <br/>
        /// True = Taxiway is available. <br/>
        /// False = Taxiway is unavailable.
        /// </summary>
        public bool IsAvailable { get; set; } = true;

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
            flight = TaxiQueue.Dequeue();
            
            if (flight.Status == Arrived || flight.Status == ArrivingDelayed)
            {
                flight.ParkGate();
            }
            else
            {
                foreach (Runway runway in ConnectedRunway)
                {
                    //Finn ut hvor køen er minst
                }
                runway.enqueueFlight(flight);
            }
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
