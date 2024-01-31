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

        /// <summary>
        /// Constructor for making a taxiway
        /// </summary>
        /// <param name="name"></param>
        public Taxi(string name)
        {
            Name = name;
        }

        /// <summary>
        /// This adds a flight to the taxi queue.
        /// </summary>
        /// <param name="flight"></param>
        public void addToQueue(Flight flight)
        {
            TaxiQueue.Enqueue(flight);
        }

        /// <summary>
        /// Removes the flight from the start of the queue. Based on the status of said flight, it either gets access to a runway queue, or arrives at their gate
        /// </summary>
        public void removeFromQueue() 
        { 
            Flight flight = TaxiQueue.Dequeue();
            
            //Status er private, bruk get
            if (flight.Status == FlightStatus.Arrived || flight.Status == FlightStatus.ArrivingDelayed)
            {
                flight.parkGate(flight.AssignedGate);
            }
            else
            {
                foreach (Runway runway in ConnectedRunway)
                {
                    //Finn ut hvor køen er minst
                    //Kalle på findRunway fra flight?
                    Runway correctRunway = flight.findRunway();
                    correctRunway.enqueueFlight(flight);
                }
                
            }
        }

        /// <summary>
        /// Returns the size of the taxi queue
        /// </summary>
        /// <returns></returns>
        public int lengthQueue()
        {
            return TaxiQueue.Count;
        }

        /// <summary>
        /// Adds a gate to the list of connected gates
        /// </summary>
        /// <param name="gate"></param>
        public void addConnectedGate(Gate gate)
        {
            ConnectedGate.Add(gate);
        }

        /// <summary>
        /// Removes a certain gate from the list of connected gates
        /// </summary>
        /// <param name="gate"></param>
        public void removeConnectedGate(Gate gate)
        {
            ConnectedGate.Remove(gate);
        }

        /// <summary>
        /// Adds a runway to the list of connected runways
        /// </summary>
        /// <param name="runway"></param>
        public void addConnectedRunway(Runway runway)
        {
            ConnectedRunway.Add(runway);
        }

        /// <summary>
        /// Removes a runway from the list of connected runways
        /// </summary>
        /// <param name="runway"></param>
        public void removeConnectedRunway(Runway runway)
        {
            ConnectedRunway.Remove(runway);
        }

    }

}
