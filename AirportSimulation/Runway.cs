﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public class Runway
    {
        // Instance Variables
        public String RunwayName;
        public List<Taxi> ConnectedTaxi = new List<Taxi>(); // litt usikker på om denne er gjort riktig
        public Queue<Flight> RunwayQueue = new Queue<Flight>();
        public bool IsAvailable = true;
        public Flight FlightOnRunway = null;
        


        // Constructor
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
        public void addToRunwayQueue(Flight flight)
        {
            RunwayQueue.Enqueue(flight);
        }

        // Fjerne fremste fra køen, bruke denne til å gi tilgang til runway når den er ledig
        /// <summary>
        /// Removes a flight from the queue. Based on the flightstatus it either goes to the runway and prepares for takeoff
        /// or it gets sent to the taxiqueue that is connected to their assigned gate
        /// </summary>
        public void removeFromRunwayQueue()
        {
            // NB: Denne funksjonen krever at du har en AssignedGate
            Flight flight = RunwayQueue.Dequeue();

            if (flight.getDirection() == Direction.Outgoing)
            {
                addFlightToRunway(flight);
            }

            else
            {
                //AssignedGate er private, bruk get
                Gate desiredGate = flight.getAssignedGate();
                Taxi optimalTaxi = null;
                int queueSize = int.MaxValue;
                //Går gjennom alle taxi som er connected til runwayen
                foreach (Taxi taxi in ConnectedTaxi)
                {
                    //Sjekker om gaten er connected til taxi
                    if (taxi.getConnectedGates().Contains(desiredGate))
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
                optimalTaxi.addToTaxiQueue(flight);
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

        public void addFlightToRunway(Flight flight)
        {
            this.IsAvailable = false;
            FlightOnRunway = flight;

        }

        public void setIsAvailable(bool availability)
        {
            this.IsAvailable=availability;
        }

        public bool getIsAvailable()
        {
            return IsAvailable;
        }

        public void setFlightOnRunway(Flight flight)
        {
            FlightOnRunway = flight;
        }

        public Flight getFlightOnRunway()
        {
            return FlightOnRunway;
        }

        public string getRunwayName()
        {
            return this.RunwayName;
        }

        public Queue<Flight> getRunwayQueue()
        {
            return this.RunwayQueue;
        }
    }


}
