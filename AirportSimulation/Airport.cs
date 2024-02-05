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
        /// 
        //Dette blir setter og getter for AirportName, pass på at getter og setter defineres med liten forbokstav
        public string airportName {

            get { return AirportName; }
            private set { AirportName = value; }

        }
        /*Eksempelbruk blir da:
        string newName = "Gardermoen";
        airport.airportName = newName;
        */


        /// <summary>
        /// List containing all terminals in this airport
        /// </summary>
        public List<Terminal> AllTerminals = new List<Terminal>();
        /// <summary>
        /// List containing all runways in this airport
        /// </summary>
        public List<Runway> AllRunways { 
                
            get { return _allRunways; }
            set { _allRunways = value; }
        
        } 
        /// <summary>
        /// List containing all taxiways in this airport
        /// </summary>
        public List<Taxi> AllTaxis {

            get { return _allTaxis; }
            set { _allTaxis = value; }

        }
        public List<Flight> AllFlights {

            get { return _allFlights; }
            set { _allFlights = value; }

        }

        public List<Flight> CompletedFlights {

            get { return _completedFlights; }
            set { _completedFlights = value; }
        
        }

        public int ElapsedDays {

            get { return _elapsedDays; }
            private set { _elapsedDays = value; } // Setter is private

        }
        public int ElapsedHours {

            get { return _elapsedHours; }
            private set { _elapsedHours = value; }

        }
        public int ElapsedMinutes {

            get { return _elapsedMinutes; }
            private set { _elapsedMinutes = value; }

        } 

        private DateTime ScheduledStartDate {

            get { return _scheduledStartDate; }
            set { _scheduledStartDate = value; }

        }
        private DateTime ScheduledEndDate {

            get { return _scheduledEndDate; }
            set { _scheduledEndDate = value; }

        }
            

        /// <summary>
        /// Constructor for initializing a new instance of the <see cref="Airport"/> class.
        /// </summary>
        /// <param name="airportName"></param>
        /// <param name="terminalName"></param>
        /// <param name="taxiName"></param>
        /// <param name="runwayName"></param>
        /// <param name="gateName"></param>
        public Airport(string airportName, string terminalName, string taxiName, string runwayName, string gateName)
        {
            this.AirportName = airportName;
            Terminal terminal = addTerminal(terminalName);
            //terminal.setIsInternational(true);
            Taxi taxi = addTaxi(taxiName);
            Runway runway = addRunway(runwayName);
            Gate gate = terminal.addGate(gateName);
            gate.addTaxi(taxi);
            taxi.addConnectedGate(gate);
            taxi.addConnectedRunway(runway);
            runway.addConnectedTaxi(taxi);
        }

        /// <summary>
        /// Adds a new <see cref="Terminal"/> with the specified name to the airport and returns it.
        /// </summary>
        /// <param name="terminalName">The name of the terminal to be added.</param>
        /// <returns>The newly created <see cref="Terminal"/> object.</returns>
        public Terminal addTerminal(string name)
        {
            Terminal newTerminal = new Terminal(name);
            AllTerminals.Add(newTerminal);
            return newTerminal;
        }

        /// <summary>
        /// Adds a new <see cref="Runway"/> with the specified name to the airport and returns it.
        /// </summary>
        /// <param name="runwayName">The name of the runway to be added.</param>
        /// <returns>The newly created <see cref="Runway"> object.</returns>
        public Runway addRunway(string name)
        {
            Runway newRunway = new Runway(name);
            AllRunways.Add(newRunway);
            return newRunway;
        }

        /// <summary>
        /// Adds a new <see cref="Taxi"/> with the specified name to the airport and returns it.
        /// </summary>
        /// <param name="taxiName">The name of the taxi to be added.</param>
        /// <returns>The newly created <see cref="Taxi"> object.</returns>
        public Taxi addTaxi(string name)
        {
            Taxi newTaxi = new Taxi(name);
            AllTaxis.Add(newTaxi);
            return newTaxi;
        }

        /// <summary>
        /// Adds a new <see cref="Flight"/> with the specified name to the airport.
        /// </summary>
        /// <param name="flightNumber">The name of the flight to be added.</param>
        /// <returns>The newly created <see cref="Flight"> object.</returns>
        public void addFlight(Flight flight)
        {
            this.AllFlights.Add(flight);
        }

        /// <summary>
        /// Gets all flights in the <see cref="Airport"> and returns as list.
        /// </summary>
        /// <returns>List of all flights.</returns>
        public List<Flight> getAllFlights()
        {
            return this.AllFlights;
        }

        /// <summary>
        /// Gets all runways in the <see cref="Airport"> and returns as list.
        /// </summary>
        /// <returns>List of all runways.</returns>
        public List<Runway> getAllRunways()
        {
            return AllRunways;
        }

        /// <summary>
        /// Gets all taxiways in the <see cref="Airport"> and returns as list.
        /// </summary>
        /// <returns>List of all taxiways.</returns>
        public List<Taxi> getAllTaxis()
        {
            return AllTaxis;
        }

        /// <summary>
        /// Gets all flights in the <see cref="Airport"> and returns as list.
        /// </summary>
        /// <returns>List of all flights.</returns>
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
