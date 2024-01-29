using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Airport : IAirport
    {
        // Instance Variables
        /// <summary>
        /// The name of your Airport.
        /// </summary>
        private string AirportName { get; set; }
        /// <summary>
        /// List containing all terminals in this airport
        /// </summary>
        private List<Terminal> allTerminals = new List<Terminal>();
        /// <summary>
        /// List containing all runways in this airport
        /// </summary>
        private List<Runway> allRunways = new List<Runway>();
        /// <summary>
        /// List containing all taxiways in this airport
        /// </summary>
        private List<Taxi> allTaxis = new List<Taxi>();
        private List<Flight> allFlights = new List<Flight>();

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
            //Connecter de til hverandre så de kan brukes
            runwayName.addConnectedTaxi(taxiName);
            taxiName.addConnectedRunway(runwayName);
            taxiName.addConnectedGate(gateName);
            gateName.AddConnectedTaxi(taxiName);
            Console.WriteLine("Nå er airport " + this.AirportName + " opprettet");
        }

        /// <summary>
        /// Calls the terminal constructor and adds the resulting object to the list of terminals
        /// </summary>
        public void addTerminal(string name)
        {
            Terminal newTerminal = new Terminal(name);
            allTerminals.Add(newTerminal);
        }

        /// <summary>
        /// Calls the runway constructor and adds the resulting object to the list of runways
        /// </summary>
        public void addRunway(string name)
        {
            Runway newRunway = new Runway(name);
            allRunways.Add(newRunway);
        }

        /// <summary>
        /// Calls the taxi constructor and adds the resulting object to the list of taxiways
        /// </summary>
        public void addTaxi(string name)
        {
            Taxi newTaxi = new Taxi(name);
            allTaxis.Add(newTaxi);
        }

        public void addFlight(string name, string destination, int hour, int minutes, Direction direction, Airport this)
        {
            Flight newFlight = new Flight(number, destination, hour, minutes, direction, this);
            allFlights.Add(newFlight);
        }

    }
}
