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
        //Instansvariablen AirportName må fortsatt deklareres her oppe
        private string AirportName;
        private List<Runway> _allRunways = new List<Runway>();
        private List<Taxi> _allTaxis = new List<Taxi>();
        private List<Flight> _completedFlights = new List<Flight>();
        private List<Flight> _allFlights = new List<Flight>();
        private int _elapsedDays = 0;
        private int _elapsedHours = 0;
        private int _elapsedMinutes = 0;
        private DateTime _scheduledStartDate;
        private DateTime _scheduledEndDate;


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
        /// Constructor for making an airport
        /// </summary>
        /// <param name="AirportName"></param>
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
