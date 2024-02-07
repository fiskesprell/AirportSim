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
            TaxiName = name;
            Console.WriteLine("Taxi " + name + " har blitt opprettet");
        }

        /// <summary>
        /// This adds a flight to the taxi queue.
        /// </summary>
        /// <param name="flight"></param>
        public void addToTaxiQueue(Flight flight)
        {
            if (flight.getDirection() == Direction.Outgoing)
            {
                if (flight.getStatus() != FlightStatus.Departing)
                {
                    Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.getFlightNumber() + " started traveling on " + this.TaxiName + " towards " + flight.getAssignedGate().getGateName());
                    TaxiQueue.Enqueue(flight);
                }

                else
                {
                    Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.getFlightNumber() + " started traveling on " + this.TaxiName + " towards " + flight.getDesiredRunway().getRunwayName());
                    TaxiQueue.Enqueue(flight);
                }
            }
            

            else if(flight.getDirection() == Direction.Incoming)
            {
                Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.getFlightNumber() + " started traveling on " + this.TaxiName + " towards " + flight.getAssignedGate().getGateName());
                TaxiQueue.Enqueue(flight);
            }

            
        }

        /// <summary>
        /// Removes the flight from the start of the queue. Based on the status of said flight, it either gets access to a runway queue, or arrives at their gate
        /// </summary>
        public void removeFromTaxiQueue() 
        { 

            Flight flight = TaxiQueue.Dequeue();
            
            if (flight.getDirection() == Direction.Incoming || flight.getStatus() == FlightStatus.ArrivingDelayed)
            {
                flight.parkGate(flight.getAssignedGate());
            }
            else
            {
                if (flight.getStatus() != FlightStatus.Departing)
                {
                    //Hvis statusen er departing så er den ferdig med å boarde så da skal den finne en taxi for å finne en runway for å ta av
                    if (!flight.getIsTraveling())
                    {
                        flight.parkGate(flight.getAssignedGate());
                    }
                }

                else
                {
                    if (flight.getDesiredRunway() == null)
                    {
                        Runway correctRunway = flight.findRunway();
                        correctRunway.addToRunwayQueue(flight);
                    }//hvis statusen ikke er "departing" så vil det si at den ikke har boardet enda og skal til gate. Dvs, den kommer fra hangar
                    else
                    {
                        flight.getDesiredRunway().addToRunwayQueue(flight);
                    }
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

        public string getTaxiName()
        {
            return this.TaxiName;
        }

        public Queue<Flight> getTaxiQueue()
        {
            return this.TaxiQueue;
        }

    }

}
