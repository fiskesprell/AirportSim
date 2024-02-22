using AirportSimulationCl;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    /// <summary>
    /// Manages a runway's operations including flight queue, connected taxiways, and availability.
    /// </summary>
    public class Runway
    {
        // Instance Variables

        /// <summary>
        /// The name identifier for the runway. Typically in 1-9 format.
        /// </summary>
        public String RunwayName;

        /// <summary>
        /// A list of taxiways that are connected to this runway.
        /// </summary>
        public List<Taxi> ConnectedTaxi = new List<Taxi>(); // litt usikker på om denne er gjort riktig

        /// <summary>
        /// Queue of flights waiting to take off or land on this runway.
        /// </summary>
        public Queue<Flight> RunwayQueue = new Queue<Flight>();

        /// <summary>
        /// Indicates if the runway is currently available. True if available.
        /// </summary>
        public bool IsAvailable = true;

        /// <summary>
        /// The flight currently occupying the runway, if any.
        /// </summary>
        public Flight FlightOnRunway = null;

        public Runway() { }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the Runway class.
        /// </summary>
        /// <param name="runwayName">The name of the runway.</param>
        public Runway(string runwayName)
        {
            this.RunwayName = runwayName;
            Console.WriteLine("Runway " + runwayName + " har blitt opprettet");
        }



        // Legge til en flight i køen rullebanekøen. Automatikk fra når en flight er fremst i taxi køen
        /// <summary>
        /// Adds a flight to the runwayqueue
        /// </summary>
        /// <param name="flight"></param>
        public void AddToRunwayQueue(Flight flight)
        {
            RunwayQueue.Enqueue(flight);
        }

        // Fjerne fremste fra køen, bruke denne til å gi tilgang til runway når den er ledig
        /// <summary>
        /// Removes a flight from the queue. Based on the flightstatus it either goes to runway and prepares for takeoff
        /// or gets sent to the taxiqueue that is connected to their assigned gate.
        /// </summary>
        /// <remarks>Requires an assigned gate</remarks>
        public void RemoveFromRunwayQueue()
        {
            // NB: Denne funksjonen krever at du har en AssignedGate
            Flight flight = RunwayQueue.Dequeue();

            if (flight.GetDirection() == FlightDirection.Outgoing)
            {
                AddFlightToRunway(flight);
            }

            else
            {
                //AssignedGate er private, bruk get
                Gate desiredGate = flight.GetAssignedGate();
                Taxi optimalTaxi = null;
                int queueSize = int.MaxValue;
                //Går gjennom alle taxi som er connected til runwayen
                foreach (Taxi taxi in ConnectedTaxi)
                {
                    //Sjekker om gaten er connected til taxi
                    if (taxi.GetConnectedGates().Contains(desiredGate))
                    {
                        //Hvis den er connected, sjekk køstørrelsen
                        //Hvis 
                        if (taxi.TaxiQueue.Count() > queueSize)
                        {
                            optimalTaxi = taxi;
                            queueSize = taxi.TaxiQueue.Count();
                        }
                    }
                }
                optimalTaxi.AddToTaxiQueue(flight);
            }

        }

        // Legge til taxi objekter i listen 
        /// <summary>
        /// Adds a taxi object to the list of connected taxiways
        /// </summary>
        /// <param name="taxi">The taxiway to add.</param>
        public void AddConnectedTaxi(Taxi taxi)
        {
            ConnectedTaxi.Add(taxi);
        }

        /// <summary>
        /// Assigns a flight to the runway, marking it as unavailable.
        /// </summary>
        /// <param name="flight">The flight to add to the runway.</param>
        public void AddFlightToRunway(Flight flight)
        {
            this.IsAvailable = false;
            FlightOnRunway = flight;

        }

        /// <summary>
        /// Sets the availability of the runway.
        /// </summary>
        /// <param name="availability">The availability status to set.</param>
        public void SetIsAvailable(bool availability)
        {
            this.IsAvailable=availability;
        }

        /// <summary>
        /// Returns the availability status of the runway.
        /// </summary>
        /// <returns>True if the runway is available. Otherwise, false.</returns>
        public bool GetIsAvailable()
        {
            return IsAvailable;
        }

        /// <summary>
        /// Sets the flight currently on the runway.
        /// </summary>
        /// <param name="flight">The flight to set.</param>
        public void SetFlightOnRunway(Flight flight)
        {
            FlightOnRunway = flight;
        }

        /// <summary>
        /// Gets the flight currently on the runway.
        /// </summary>
        /// <returns>The flight on the runway.</returns>
        public Flight GetFlightOnRunway()
        {
            return FlightOnRunway;
        }

        /// <summary>
        /// Returns the name of the runway.
        /// </summary>
        /// <returns>The runway name.</returns>
        public string GetRunwayName()
        {
            return this.RunwayName;
        }

        /// <summary>
        /// Returns the queue of flights for the runway.
        /// </summary>
        /// <returns>The runway's flight queue.</returns>
        public Queue<Flight> GetRunwayQueue()
        {
            return this.RunwayQueue;
        }
    }


}
