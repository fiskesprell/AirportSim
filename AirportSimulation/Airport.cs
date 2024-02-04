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
        public List<Flight> AllFlights { get; set; } = new List<Flight>();

        public List<Flight> CompletedFlights { get; set; } = new List<Flight> ();

        public int ElapsedDays { get; private set; } = 0;
        public int ElapsedHours { get; private set; } = 0;
        public int ElapsedMinutes { get; private set; } = 0;

        private DateTime ScheduledStartDate { get; set; }
        private DateTime ScheduledEndDate { get; set; }
            

        /// <summary>
        /// Constructor for making an airport
        /// </summary>
        /// <param name="AirportName"></param>
        public Airport(string airportName, string terminalName, string taxiName, string runwayName, string gateName)
        {
            this.AirportName = airportName;
            Terminal terminal = addTerminal(terminalName);
            terminal.setIsInternational(true);
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
        public Terminal addTerminal(string name)
        {
            Terminal newTerminal = new Terminal(name);
            AllTerminals.Add(newTerminal);
            return newTerminal;
        }

        /// <summary>
        /// Calls the runway constructor and adds the resulting object to the list of runways
        /// </summary>
        public Runway addRunway(string name)
        {
            Runway newRunway = new Runway(name);
            AllRunways.Add(newRunway);
            return newRunway;
        }

        /// <summary>
        /// Calls the taxi constructor and adds the resulting object to the list of taxiways
        /// </summary>
        public Taxi addTaxi(string name)
        {
            Taxi newTaxi = new Taxi(name);
            AllTaxis.Add(newTaxi);
            return newTaxi;
        }

        public void addFlight(Flight flight)
        {
            this.AllFlights.Add(flight);
        }

        public List<Flight> getAllFlights()
        {
            return this.AllFlights;
        }

        public List<Runway> getAllRunways()
        {
            return AllRunways;
        }

        public List<Taxi> getAllTaxis()
        {
            return AllTaxis;
        }

        public List<Terminal> getAllTerminals()
        {
            return AllTerminals;
        }

        public DateTime getScheduledStartDate()
        {
            return ScheduledStartDate;
        }

        public void setScheduledStartDate(DateTime scheduledStartDate)
        {
            ScheduledStartDate = scheduledStartDate;
        }

        public DateTime getScheduledEndDate()
        {
            return ScheduledEndDate;
        }

        public void setScheduledEndDate(DateTime scheduledEndDate)
        {
            ScheduledEndDate = scheduledEndDate;
        }

        public string getAirportName()
        {
            return AirportName;
        }

        public void addCompletedFlight(Flight flight)
        {
            this.CompletedFlights.Add(flight);
        }

        public void removeCompletedFlightFromAllFlights(Flight flight)
        {
            this.AllFlights.Remove(flight);
        }


    }
}
