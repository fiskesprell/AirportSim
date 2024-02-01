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
        private string AirportName { get; set; }
        /// <summary>
        /// List containing all terminals in this airport
        /// </summary>
        private List<Terminal> AllTerminals = new List<Terminal>();
        /// <summary>
        /// List containing all runways in this airport
        /// </summary>
        private List<Runway> AllRunways { get; set; } = new List<Runway>();
        /// <summary>
        /// List containing all taxiways in this airport
        /// </summary>
        private List<Taxi> AllTaxis = new List<Taxi>();
        private List<Flight> AllFlights = new List<Flight>();

        public int ElapsedDays { get; private set; } = 0;
        public int ElapsedHours { get; private set; } = 0;
        public int ElapsedMinutes { get; private set; } = 0;
            

        /// <summary>
        /// Constructor for making an airport
        /// </summary>
        /// <param name="AirportName"></param>
        public Airport(string airportName, string terminalName, string taxiName, string runwayName, string gateName)
        {
            this.AirportName = airportName;
            Terminal terminal = addTerminal(terminalName);
            addTaxi(taxiName);
            addRunway(runwayName);
            terminal.addGate(gateName);
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
        public void addRunway(string name)
        {
            Runway newRunway = new Runway(name);
            AllRunways.Add(newRunway);
        }

        /// <summary>
        /// Calls the taxi constructor and adds the resulting object to the list of taxiways
        /// </summary>
        public void addTaxi(string name)
        {
            Taxi newTaxi = new Taxi(name);
            AllTaxis.Add(newTaxi);
        }

        public void addFlight(string name, string destination, int hour, int minutes, Direction direction, Airport airport)
        {
            Flight newFlight = new Flight(name, destination, hour, minutes, direction, this);
            AllFlights.Add(newFlight);
        }

        public List<Flight> getAllFlights()
        {
            return AllFlights;
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

    }
}
