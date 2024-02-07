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
        public string TaxiName { get; set; }
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
            TaxiName = name;
            Console.WriteLine("Taxi " + name + " har blitt opprettet");
        }

        /// <summary>
        /// This adds a flight to the taxi queue.
        /// </summary>
        /// <param name="flight"></param>
        public void AddToTaxiQueue(Flight flight)
        {
            if (flight.GetDirection() == Direction.Outgoing)
            {
                if (flight.GetStatus() != FlightStatus.Departing)
                {
                    Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.GetFlightNumber() + " started traveling on " + this.TaxiName + " towards " + flight.GetAssignedGate().GetGateName());
                    TaxiQueue.Enqueue(flight);
                }

                else
                {
                    Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.GetFlightNumber() + " started traveling on " + this.TaxiName + " towards " + flight.GetDesiredRunway().GetRunwayName());
                    TaxiQueue.Enqueue(flight);
                }
            }
            

            else if(flight.GetDirection() == Direction.Incoming)
            {
                Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.GetFlightNumber() + " started traveling on " + this.TaxiName + " towards " + flight.GetAssignedGate().GetGateName());
                TaxiQueue.Enqueue(flight);
            }

            
        }

        /// <summary>
        /// Removes the flight from the start of the queue. Based on the status of said flight, it either gets access to a runway queue, or arrives at their gate
        /// </summary>
        public void RemoveFromTaxiQueue() 
        { 

            Flight flight = TaxiQueue.Dequeue();
            
            if (flight.GetDirection() == Direction.Incoming || flight.GetStatus() == FlightStatus.ArrivingDelayed)
            {
                flight.ParkFlightAtGate(flight.GetAssignedGate());
            }
            else
            {
                if (flight.GetStatus() != FlightStatus.Departing)
                {
                    //Hvis statusen er departing så er den ferdig med å boarde så da skal den finne en taxi for å finne en runway for å ta av
                    if (!flight.GetIsTraveling())
                    {
                        flight.ParkFlightAtGate(flight.GetAssignedGate());
                    }
                }

                else
                {
                    if (flight.GetDesiredRunway() == null)
                    {
                        Runway correctRunway = flight.FindRunway();
                        correctRunway.AddToRunwayQueue(flight);
                    }//hvis statusen ikke er "departing" så vil det si at den ikke har boardet enda og skal til gate. Dvs, den kommer fra hangar
                    else
                    {
                        flight.GetDesiredRunway().AddToRunwayQueue(flight);
                    }
                }
                
            }
        }

        /// <summary>
        /// Adds a gate to the list of connected gates
        /// </summary>
        /// <param name="gate"></param>
        public void AddConnectedGate(Gate gate)
        {
            ConnectedGates.Add(gate);
        }

        /// <summary>
        /// Removes a certain gate from the list of connected gates
        /// </summary>
        /// <param name="gate"></param>
        public void RemoveConnectedGate(Gate gate)
        {
            ConnectedGates.Remove(gate);
        }

        /// <summary>
        /// Adds a runway to the list of connected runways
        /// </summary>
        /// <param name="runway"></param>
        public void AddConnectedRunway(Runway runway)
        {
            ConnectedRunways.Add(runway);
        }

        /// <summary>
        /// Removes a runway from the list of connected runways
        /// </summary>
        /// <param name="runway"></param>
        public void RemoveConnectedRunway(Runway runway)
        {
            ConnectedRunways.Remove(runway);
        }

        public List<Gate> GetConnectedGates()
        {
            return ConnectedGates;
        }

        public List<Runway> GetConnectedRunways()
        {
            return ConnectedRunways;
        }

        public string GetTaxiName()
        {
            return this.TaxiName;
        }

        public Queue<Flight> GetTaxiQueue()
        {
            return this.TaxiQueue;
        }

    }

}
