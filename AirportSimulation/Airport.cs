using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public class Airport : IAirport
    {
        // Instance Variables
        /// <summary>
        /// The name of your Airport.
        /// </summary>
        public string AirportName { get; set; }
        /// <summary>
        /// List containing all terminals in this airport
        /// </summary>
        public List<Terminal> AllTerminals = new List<Terminal>();
        /// <summary>
        /// List containing all runways in this airport
        /// </summary>
        public List<Runway> AllRunways { get; set; } = new List<Runway>();
        /// <summary>
        /// List containing all taxiways in this airport
        /// </summary>
        public List<Taxi> AllTaxis { get; set; } = new List<Taxi>();
       /// <summary>
        /// List containing all flights in this airport
        /// </summary>
        public List<Flight> AllFlights { get; set; } = new List<Flight>();
       /// <summary>
        /// List containing all completed flights in this airport
        /// </summary>
        public List<Flight> CompletedFlights { get; set; } = new List<Flight> ();
        /// <summary>
        /// Gets and sets the scheduled start date
        /// </summary>
        private DateTime ScheduledStartDate { get; set; }
       /// <summary>
        /// Gets and sets the scheduled end date
        /// </summary>
        private DateTime ScheduledEndDate { get; set; }

        public Airport() { }

        public Airport(string airportName)
        {
            this.AirportName = airportName;
        }

        /// <summary>
        /// Constructor for making an airport
        /// </summary>
        /// <param name="AirportName"></param>
        public Airport(string airportName, string terminalName, string taxiName, string runwayName, string gateName)
        {
            this.AirportName = airportName;
            Terminal terminal = AddNewTerminal(terminalName);
            //terminal.setIsInternational(true);
            Taxi taxi = AddNewTaxi(taxiName);
            Runway runway = AddNewRunway(runwayName);
            Gate gate = terminal.AddNewGate(gateName);
            gate.AddTaxi(taxi);
            taxi.AddConnectedGate(gate);
            taxi.AddConnectedRunway(runway);
            runway.AddConnectedTaxi(taxi);
        }

        /// <summary>
        /// Calls the terminal constructor and adds the resulting object to the list of terminals
        /// </summary>
        public Terminal AddNewTerminal(string terminalName)
        {
            Terminal newTerminal = new Terminal(terminalName);
            AllTerminals.Add(newTerminal);
            return newTerminal;
        }

        /// <summary>
        /// Adds a terminalobject to the list of terminals
        /// </summary>
        /// <param name="terminal"></param>
        public void AddExistingTerminal(Terminal terminal)
        {
            AllTerminals.Add(terminal);
        }

        /// <summary>
        /// Calls the runway constructor and adds the resulting object to the list of runways
        /// </summary>
        public Runway AddNewRunway(string runwayName)
        {
            Runway newRunway = new Runway(runwayName);
            AllRunways.Add(newRunway);
            return newRunway;
        }

        /// <summary>
        /// Adds a runwayobject to the list of runways
        /// </summary>
        /// <param name="runway"></param>
        public void AddExistingRunway(Runway runway)
        {
            AllRunways.Add(runway);
        }

        /// <summary>
        /// Call the taxi constructor and adds the resulting object taxi object to the list of taxis
        /// </summary>
        public Taxi AddNewTaxi(string taxiName)
        {
            Taxi newTaxi = new Taxi(taxiName);
            AllTaxis.Add(newTaxi);
            return newTaxi;
        }

        /// <summary>
        /// Adds a taxiobject to the list of taxis
        /// </summary>
        /// <param name="taxi"></param>
        public void AddExistingTaxi(Taxi taxi)
        {
            AllTaxis.Add(taxi);
        }
        /// <summary>
        /// Adds a flight object to the airport
        /// </summary>
        public void AddNewFlight(Flight flight)
        {
            this.AllFlights.Add(flight);
        }
        /// <summary>
        /// Gets all flights in the airport
        /// </summary>
        public List<Flight> GetAllFlights()
        {
            return this.AllFlights;
        }
        /// <summary>
        /// Gets all runways the airport
        /// </summary>
        public List<Runway> GetAllRunways()
        {
            return AllRunways;
        }
        /// <summary>
        /// Gets all taxis in the airport
        /// </summary>
        public List<Taxi> GetAllTaxis()
        {
            return AllTaxis;
        }
        /// <summary>
        /// Gets all terminals in the airport
        /// </summary>
        public List<Terminal> GetAllTerminals()
        {
            return AllTerminals;
        }
        /// <summary>
        /// Gets scheduled start date for the airport's simulation
        /// </summary>
        public DateTime GetScheduledStartDate()
        {
            return ScheduledStartDate;
        }
        /// <summary>
        /// Sets scheduled start date for the airport's simulation
        /// </summary>
        public void SetScheduledStartDate(DateTime scheduledStartDate)
        {
            ScheduledStartDate = scheduledStartDate;
        }
        /// <summary>
        /// Gets scheduled end date for the airport's simulation
        /// </summary>
        public DateTime GetScheduledEndDate()
        {
            return ScheduledEndDate;
        }
        /// <summary>
        /// Sets scheduled end date for the airport's simulation
        /// </summary>
        public void SetScheduledEndDate(DateTime scheduledEndDate)
        {
            ScheduledEndDate = scheduledEndDate;
        }
        /// <summary>
        /// Gets the name of the airport
        /// </summary>
        public string GetAirportName()
        {
            return AirportName;
        }
        /// <summary>
        /// Adds a flight to the airport's completed flights
        /// </summary>
        public void AddCompletedFlight(Flight flight)
        {
            this.CompletedFlights.Add(flight);
        }
        /// <summary>
        /// Removes a completed flight from the airport's <see cref="AllFlights"> list.
        /// </summary>
        public void RemoveCompletedFlightFromAllFlights(Flight flight)
        {
            this.AllFlights.Remove(flight);
        }
        /// <summary>
        /// Prints flight number, frequency and scheduled day for all flights.
        /// </summary>
        public void PrintAllFlights()
        {
            foreach(Flight flight in this.AllFlights)
            {
                Console.WriteLine($"{flight.GetFlightNumber} - {flight.GetFlightFrequency} - {flight.GetScheduledDay}");
            }
        }

        /// <summary>
        /// Creates a new Taxi and Gate. These need to be connected and are therefore put in the same method.
        /// The new Gate is connected to the Taxi, and the taxi is then added to AllTaxis.
        /// </summary>
        /// <param name="gateName">The name of your gate</param>
        /// <param name="taxiName">The name of your taxi</param>
        public void AddNewConnectedGateAndTaxi(String gateName, String taxiName)
        {
            Taxi newTaxi = new Taxi(taxiName);
            Gate newGate = new Gate(gateName);
            newTaxi.AddConnectedGate(newGate);
            AllTaxis.Add(newTaxi);
        }
    }
}
