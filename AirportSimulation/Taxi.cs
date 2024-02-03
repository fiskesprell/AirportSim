using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public class Taxi
    {
        /// <summary>
        /// The name of your Taxiway.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// List of gates connected to this taxiway.
        /// </summary>
        public List<Gate> ConnectedGates = new List<Gate>();
        /// <summary>
        /// List of runways connected to this taxiway.
        /// </summary>
        public List<Runway> ConnectedRunways = new List<Runway>();
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
            Console.WriteLine("Taxi " + name + " har blitt opprettet");
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
            
            if (flight.getStatus() == FlightStatus.Arrived || flight.getStatus() == FlightStatus.ArrivingDelayed)
            {
                flight.parkGate(flight.getAssignedGate());
            }
            else
            {
                if (flight.getStatus() == FlightStatus.Departing)
                {
                    Runway correctRunway = flight.findRunway();
                    correctRunway.enqueueFlight(flight);
                }

                else
                {
                    flight.parkGate(flight.getAssignedGate());
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
            ConnectedGates.Add(gate);
        }

        /// <summary>
        /// Removes a certain gate from the list of connected gates
        /// </summary>
        /// <param name="gate"></param>
        public void removeConnectedGate(Gate gate)
        {
            ConnectedGates.Remove(gate);
        }

        /// <summary>
        /// Adds a runway to the list of connected runways
        /// </summary>
        /// <param name="runway"></param>
        public void addConnectedRunway(Runway runway)
        {
            ConnectedRunways.Add(runway);
        }

        /// <summary>
        /// Removes a runway from the list of connected runways
        /// </summary>
        /// <param name="runway"></param>
        public void removeConnectedRunway(Runway runway)
        {
            ConnectedRunways.Remove(runway);
        }

        public List<Gate> getConnectedGates()
        {
            return ConnectedGates;
        }

        public List<Runway> getConnectedRunways()
        {
            return ConnectedRunways;
        }

        public string getName()
        {
            return this.Name;
        }

        public Queue<Flight> getTaxiQueue()
        {
            return this.TaxiQueue;
        }

    }

}
