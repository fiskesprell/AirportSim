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
            

        /// <summary>
        /// Constructor for making an airport
        /// </summary>
        /// <param name="AirportName"></param>
        public Airport(string AirportName)
        {
            this.AirportName = AirportName;
        }

        /// <summary>
        /// Calls the terminal constructor and adds the resulting object to the list of terminals
        /// </summary>
        public void AddTerminal(string name)
        {
            Terminal newTerminal = Terminal(name);
            allTerminals.Add(newTerminal);
        }

        /// <summary>
        /// Calls the runway constructor and adds the resulting object to the list of runways
        /// </summary>
        public void AddRunway(string name)
        {
            Runway newRunway = Runway(name);
            allRunways.Add(newRunway);
        }

        /// <summary>
        /// Calls the taxi constructor and adds the resulting object to the list of taxiways
        /// </summary>
        public void AddTaxi(string name)
        {
            Taxi newTaxi = Taxi(name);
            allTaxis.Add(newTaxi);
        }

    }
}
