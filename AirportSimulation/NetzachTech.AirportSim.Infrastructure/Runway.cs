using AirportSimulation;
using NetzachTech.AirportSim.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.Infrastructure
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
        private string _runwayName;
        public string RunwayName
        { get => _runwayName; set => _runwayName = value; }

        /// <summary>
        /// A list of taxiways that are connected to this runway.
        /// </summary>
        private List<Taxi> _connectedTaxi = new List<Taxi>();
        public List<Taxi> ConnectedTaxi
        { get => _connectedTaxi; }

        /// <summary>
        /// Queue of flights waiting to take off or land on this runway.
        /// </summary>
        private Queue<Flight> _runwayQueue = new Queue<Flight>();
        public Queue<Flight> RunwayQueue
        { get => _runwayQueue; }

        /// <summary>
        /// Indicates if the runway is currently available. True if available.
        /// </summary>
        private bool _isAvailable = true;
        public bool IsAvailable
        { get => _isAvailable; set => _isAvailable = value; }

        /// <summary>
        /// The flight currently occupying the runway, if any.
        /// </summary>
        private Flight _flightOnRunway = null;
        public Flight FlightOnRunway
        { get => _flightOnRunway; set => _flightOnRunway = value; }

        // Constructor
        /// <summary>
        /// Initializes a new instance of the Runway class.
        /// </summary>
        /// <param name="runwayName">The name of the runway.</param>
        public Runway(string runwayName)
        {
            this.RunwayName = runwayName;
        }

        public Runway() { }

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

            if (flight.FlightDirection == FlightDirection.Outgoing)
            {
                AddFlightToRunway(flight);
            }

            else
            {

                Gate desiredGate = flight.AssignedGate;
                Taxi optimalTaxi = null;
                int queueSize = int.MaxValue;
                //Går gjennom alle taxi som er connected til runwayen
                foreach (Taxi taxi in ConnectedTaxi)
                {
                    //Sjekker om gaten er connected til taxi
                    if (taxi.ConnectedGates.Contains(desiredGate))
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
            IsAvailable = false;
            FlightOnRunway = flight;

        }
    }


}
