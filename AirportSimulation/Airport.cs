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
            Terminal terminal = AddTerminal(terminalName);
            AddTaxi(taxiName);
            AddRunway(runwayName);
            terminal.addGate(gateName);
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

        public void AddFlight(string name, string destination, int hour, int minutes, Direction direction, Airport this)
        {
            Flight newFlight = Flight(number, destination, hour, minutes, direction, this);
            allFlights.Add(newFlight);
        }

        private void SimulateTime(int days, int hours, int minutes)
        {
            //Legger inn en sjekk at det finnes minst et objekt av hver del av infrastrukturen, ellers vil ikke simuleringen begynne
            if (allRunways.Count == 0 || allTaxis.Count == 0 || allTerminals.Count == 0)
            {
                throw new Exception 
            }
            int totalMinutes = 1440 * days + 60 * hours + minutes;

            for (int i = 0; i < totalMinutes; i++)
            {
                for each(var flight in allFlights) {
                    flight.updateElapsedTime(this);
                }
                if (ElapsedMinutes == 60)
                {
                    ElapsedHours += 1;
                    ElapsedMinutes = 0;
                }

                if (ElapsedHours == 24)
                {
                    ElapsedDays += 1;
                    ElapsedHours = 0;
                }
                ElapsedMinutes += 1;
            }
        }

    }
}
