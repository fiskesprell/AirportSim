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
        /// Declares instance of ElapsedDays
        /// </summary>
        public int ElapsedDays { get; private set; } = 0;
        /// <summary>
        /// Declares instance of ElapsedHours
        /// </summary>
        public int ElapsedHours { get; private set; } = 0;
        /// <summary>
        /// Declares instance of ElapsedMinutes
        /// </summary>
        public int ElapsedMinutes { get; private set; } = 0;


        /// <summary>
        /// Declares instance of scheduled start date.
        /// </summary>
        private DateTime ScheduledStartDate { get; set; }

        /// <summary>
        /// Declares instance of scheduled end date.
        /// </summary>
        private DateTime ScheduledEndDate { get; set; }
            

        /// <summary>
        /// Constructor for making an airport
        /// </summary>
        /// <param name="AirportName"></param>
        /// <param name="TerminalName"></param>
        /// <param name="TaxiName"></param>
        /// <param name="RunwayName"></param>
        /// <param name="GateName"></param>
        /// <param name="GateName"></param>
        /// <returns>New Airport object</returns>
        public Airport(string airportName, string terminalName, string taxiName, string runwayName, string gateName)
        {
            this.AirportName = airportName;
            Terminal terminal = addTerminal(terminalName);
            Taxi taxi = addTaxi(taxiName);
            Runway runway = addRunway(runwayName);
            Gate gate = terminal.addGate(gateName);
            gate.addTaxi(taxi);
            taxi.addConnectedGate(gate);
            taxi.addConnectedRunway(runway);
            runway.addConnectedTaxi(taxi);
        }

        /// <summary>
        /// Calls the terminal constructor and adds the resulting object to the list of terminals
        /// </summary>
        /// <param name="TerminalName"></param>
        /// <returns>New Terminal object</returns>
        public Terminal addTerminal(string name)
        {
            Terminal newTerminal = new Terminal(name);
            AllTerminals.Add(newTerminal);
            return newTerminal;
        }

        /// <summary>
        /// Calls the runway constructor and adds the resulting object to the list of runways
        /// </summary>
        /// <param name="RunwayName"></param>
        /// <returns>New Runway object</returns>
        public Runway addRunway(string name)
        {
            Runway newRunway = new Runway(name);
            AllRunways.Add(newRunway);
            return newRunway;
        }

        /// <summary>
        /// Calls the taxi constructor and adds the resulting object to the list of taxiways
        /// </summary>
        /// <param name="TaxiName"></param>
        /// <returns>New Taxi object</returns>
        public Taxi addTaxi(string name)
        {
            Taxi newTaxi = new Taxi(name);
            AllTaxis.Add(newTaxi);
            return newTaxi;
        }

        /// <summary>
        /// Adds a flight to the AllFlights list
        /// </summary>
        /// <param name="Flight">The flight to add to AllFlights</param>
        public void addFlight(Flight flight)
        {
            this.AllFlights.Add(flight);
        }

        /// <summary>
        /// Gets AllFlights
        /// </summary>
        /// <returns>List of all flights</returns>
        public List<Flight> getAllFlights()
        {
            return this.AllFlights;
        }

        /// <summary>
        /// Gets AllRunways
        /// </summary>
        /// <returns>List of all runways</returns>
        public List<Runway> getAllRunways()
        {
            return AllRunways;
        }

        /// <summary>
        /// Gets AllTaxis
        /// </summary>
        /// <returns>List of all taxi-ways</returns>
        public List<Taxi> getAllTaxis()
        {
            return AllTaxis;
        }

        /// <summary>
        /// Gets AllTerminals
        /// </summary>
        /// <returns>List of all terminals</returns>
        public List<Terminal> getAllTerminals()
        {
            return AllTerminals;
        }

        /// <summary>
        /// Gets ScheduledStartDate
        /// </summary>
        /// <returns>Scheduled start date of simulation</returns>
        public DateTime getScheduledStartDate()
        {
            return ScheduledStartDate;
        }

        /// <summary>
        /// Sets ScheduledStartDate using DateTime format
        /// </summary>
        /// <param name = "ScheduledStartDate"></param name>
        public void setScheduledStartDate(DateTime scheduledStartDate)
        {
            ScheduledStartDate = scheduledStartDate;
        }

        /// <summary>
        /// Gets ScheduledEndDate
        /// </summary>
        /// <returns>Scheduled end date of simulation</returns>

        public DateTime getScheduledEndDate()
        {
            return ScheduledEndDate;
        }

        /// <summary>
        /// Sets ScheduledEndDate using DateTime format
        /// </summary>
        /// <param name = "ScheduledEndDate"></param name>

        public void setScheduledEndDate(DateTime scheduledEndDate)
        {
            ScheduledEndDate = scheduledEndDate;
        }

        /// <summary>
        /// Gets this AirportName
        /// </summary>
        /// <returns>Name of the Airport object</returns>
        public string getAirportName()
        {
            return AirportName;
        }

        /// <summary>
        /// Adds Flight object to list of completed flights
        /// </summary>
        /// <param name = "flight">Flight object to add to CompletedFlights</param name>
        public void addCompletedFlight(Flight flight)
        {
            this.CompletedFlights.Add(flight);
        }


        /// <summary>
        /// Removes Flight object to list of all flights
        /// </summary>
        /// <param name = "flight">Flight object to remove from AllFlights</param name>
        public void removeCompletedFlightFromAllFlights(Flight flight)
        {
            this.AllFlights.Remove(flight);
        }


    }
}
