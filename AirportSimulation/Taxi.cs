using AirportSimulationCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{

    /// <summary>
    /// Represents a taxiway, managing connected gates, runways, and the queue of flights.
    /// </summary>
    public class Taxi
    {
        /// <summary>
        /// The name of the taxiway.
        /// </summary>
        private string _taxiName;
        public string TaxiName
        {get => _taxiName;set => _taxiName = value;}

        /// <summary>
        /// List of gates connected to this taxiway.
        /// </summary>
        private List<Gate> _connectedGates = new List<Gate>();
        public List<Gate> ConnectedGates
        {get => _connectedGates;}

        /// <summary>
        /// List of runways connected to this taxiway.
        /// </summary>
        private List<Runway> _connectedRunways = new List<Runway>();
        public List<Runway> ConnectedRunways
        {get => _connectedRunways;}

        /// <summary>
        /// Queue of flights that wish to use the taxiway.
        /// </summary>
        private Queue<Flight> _taxiQueue = new Queue<Flight>();
        public Queue<Flight> TaxiQueue
        {get => _taxiQueue; }

        /// <summary>
        /// Tells you if the taxiway is available or not. <br/>
        /// True = Taxiway is available. <br/>
        /// False = Taxiway is unavailable.
        /// </summary>
        private bool _isAvailable = true;
        public bool IsAvailable
        {get => _isAvailable;set => _isAvailable = value;}

        public Taxi() { }

        /// <summary>
        /// Initializes a new instance of the Taxi class.
        /// </summary>
        /// <param name="taxiName">The name of the taxiway, typically A-Z.</param>
        public Taxi(string taxiName)
        {
            TaxiName = taxiName;
            Console.WriteLine("Taxi " + taxiName + " har blitt opprettet");
        }

        /// <summary>
        /// This adds a flight to the taxi queue.
        /// </summary>
        /// <param name="flight">Flight</param>
        public void AddToTaxiQueue(Flight flight)
        {
            if (flight.GetDirection() == FlightDirection.Outgoing)
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
            

            else if(flight.GetDirection() == FlightDirection.Incoming)
            {
                if (flight.GetAssignedGate() != null)
                {
                    Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.GetFlightNumber() + " started traveling on " + this.TaxiName + " towards " + flight.GetAssignedGate().GetGateName());
                }
                else
                {
                    Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.GetFlightNumber() + " started traveling on " + this.TaxiName + " towards a Gate");
                }

                TaxiQueue.Enqueue(flight);

            }


        }

        /// <summary>
        /// Removes the flight from the start of the queue. Based on the status of said flight, it either gets access to a runway queue, or arrives at their gate
        /// </summary>
        public void RemoveFromTaxiQueue() 
        { 

            Flight flight = TaxiQueue.Dequeue();
            
            if (flight.GetDirection() == FlightDirection.Incoming || flight.GetStatus() == FlightStatus.ArrivingDelayed)
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
        /// <param name="gate">Gate to connect</param>
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

        /// <summary>
        /// Gets the gates connected to this taxiway.
        /// </summary>
        /// <returns>List of connected gates.</returns>
        public List<Gate> GetConnectedGates()
        {
            return ConnectedGates;
        }
        /// <summary>
        /// Gets the runways connected to this taxiway.
        /// </summary>
        /// <returns>List of connected runways.</returns>
        public List<Runway> GetConnectedRunways()
        {
            return ConnectedRunways;
        }

        /// <summary>
        /// Returns the name of the taxiway.
        /// </summary>
        /// <returns>The taxiway name.</returns>
        public string GetTaxiName()
        {
            return this.TaxiName;
        }

        /// <summary>
        /// Gets the queue of flights for this taxiway.
        /// </summary>
        /// <returns>The queue of flights.</returns>
        public Queue<Flight> GetTaxiQueue()
        {
            return this.TaxiQueue;
        }

    }

}
