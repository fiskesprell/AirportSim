using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Runway
    {
        // Instance Variables
        public String RunwayName;
        public double RunwayLength = 3000; // in meters
        public List<Taxi> ConnectedTaxi = new List<Taxi>(); // litt usikker på om denne er gjort riktig
        public Queue<Flight> RunwayQueue = new Queue<Flight>();
        public DateTime LastMaintainance;
        // fra fly kommer inn på rullebanen, til det har lettet og rullebanen er ledig igjen
        public double AverageTakeoffTime = 600; // in seconds - here 10 minutes
        // fra et fly blir klarert til å lande til det er landet, bremset, og klart for å sette seg i taxikø
        public double AverageLandingTime = 300; // in seconds (5 minutes)
        public bool IsAvailable = true;
        public Flight FlightOnRunway = null;


        // Constructor
        public Runway(string runwayName)
        {
            this.RunwayName = runwayName;
            Console.WriteLine("Runway " + runwayName + " har blitt opprettet");
        }

        // Se hvilken flight som er neste i køen
        /// <summary>
        /// Returns the flight that is in front of the queue
        /// </summary>
        /// <returns></returns>
        public Flight peekQueue()
        {
            return RunwayQueue.Peek();
        }

        // Legge til en flight i køen rullebanekøen. Automatikk fra når en flight er fremst i taxi køen
        /// <summary>
        /// Adds a flight to the runwayqueue
        /// </summary>
        /// <param name="flight"></param>
        public void enqueueFlight(Flight flight)
        {
            RunwayQueue.Enqueue(flight);
        }

        // Fjerne fremste fra køen, bruke denne til å gi tilgang til runway når den er ledig
        /// <summary>
        /// Removes a flight from the queue. Based on the flightstatus it either goes to the runway and prepares for takeoff
        /// or it gets sent to the taxiqueue that is connected to their assigned gate
        /// </summary>
        public void dequeueFlight()
        {
            Flight flight = RunwayQueue.Dequeue();

            //Status er private, bruk get
            //Kanskje bedre å bruke direction uansett?
            if (flight.Status == FlightStatus.Departing || flight.Status == FlightStatus.DepartingDelayed)
            {
                addFlightToRunway(flight);
            }

            else
            {
                //AssignedGate er private, bruk get
                Gate desiredGate = flight.AssignedGate;
                Taxi optimalTaxi = null;
                int queueSize = int.MaxValue;
                //Går gjennom alle taxi som er connected til runwayen
                foreach (Taxi taxi in ConnectedTaxi)
                {
                    //Sjekker om gaten er connected til taxi
                    if (desiredGate in taxi.ConnectedGate)
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
                optimalTaxi.addToQueue(flight);
                
            }

        }

        // Legge til taxi objekter i listen 
        /// <summary>
        /// Adds a taxi object to the list of connected taxiways
        /// </summary>
        /// <param name="taxi"></param>
        public void addConnectedTaxi(Taxi taxi)
        {
            ConnectedTaxi.Add(taxi);
        }

        /// <summary>
        /// Will send the flight to the runway and prepare it for takeoff
        /// </summary>
        /// <param name="flight"></param>
        public void addFlightToRunway(Flight flight)
        {
            //Legge inn implementasjon slik at flight som har status delayed for snike?
            if (ConnectedTaxi.Count != 0)
            {

                foreach (Taxi taxi in ConnectedTaxi)
                {

                }
            }
            
            
        }
    }
}
