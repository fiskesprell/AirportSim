using AirportSimulationCl;
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
        /// <summary>
        /// String representing the name of the airport.
        /// </summary>
        private string _airportName;
        public string AirportName
        {get => _airportName;set => _airportName = value;}

        /// <summary>
        /// List of Terminal objects of all the terminals in this airport.
        /// </summary>
        private List<Terminal> _allTerminals = new List<Terminal>();
        public List<Terminal> AllTerminals
        {get => _allTerminals;}

        /// <summary>
        /// List of Runway objects in this airport.
        /// </summary>
        private List<Runway> _allRunways = new List<Runway>();
        public List<Runway> AllRunways
        { get => _allRunways;}

        /// <summary>
        /// List of Taxi objects of all the taxis in this airport.
        /// </summary>
        private List<Taxi> _allTaxis = new List<Taxi>();
        public List<Taxi> AllTaxis
        {get => _allTaxis;}

        /// <summary>
        /// List of Flight objects of all the flights scheduled in this airport.
        /// </summary>
        private List<Flight> _allFlights = new List<Flight>();
        public List<Flight> AllFlights
        {get => _allFlights;}

        /// <summary>
        /// List of flights that have completed their journey.
        /// </summary>
        private List<Flight> _completedFlights = new List<Flight>();
        public List<Flight> CompletedFlights
        {get => _completedFlights;}

        /// <summary>
        /// Gets and sets the scheduled start date
        /// </summary>
        /// <remarks>
        /// DateTime object that represents what date the simulation is starting.
        /// </remarks>
        private DateTime _scheduledStartDate;
        public DateTime ScheduledStartDate
        {get => _scheduledStartDate; set => _scheduledStartDate = value;}

        /// <summary>
        /// Gets and sets the scheduled end date
        /// </summary>
        /// <remarks>
        /// DateTime object that represents what date the simulation is ending.
        /// </remarks>
        private DateTime _scheduledEndDate;
        public DateTime ScheduledEndDate
        {get => _scheduledEndDate; set => _scheduledEndDate = value;}

        /// <summary>
        /// Gets the list of planes
        /// </summary>
        /// <remarks>
        /// Maintains a list of planes that have been or are currently at the airport. This list is accessible for additions but not direct reassignment from outside the class.
        /// </remarks>
        private List<Plane> _listOfPlanes = new List<Plane> ();
        public List<Plane> ListOfPlanes
        {get => _listOfPlanes;}

        /// <summary>
        /// Constructor for making an empty airport.
        /// </summary>
        public Airport() { }

        /// <summary>
        /// Constructor for making a named, but otherwise empty airport.
        /// </summary>
        public Airport(string airportName)
        {
            this.AirportName = airportName;
        }

        /// <summary>
        /// Constructor for making an Airport object with basic infrastructure. These Terminal, Taxi, Runway, and Gate objects will be connected to each other and assigned to this airport. By default, the Terminal object will not be allowed to serve international flights.
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
        /// Creates a new Terminal object and adds it to the airport’s AllTerminals list.
        /// </summary>
        public Terminal AddNewTerminal(string terminalName)
        {
            Terminal newTerminal = new Terminal(terminalName);
            AllTerminals.Add(newTerminal);
            return newTerminal;
        }

        /// <summary>
        /// Adds a previously created Terminal object to the airport’s AllTerminals list.
        /// </summary>
        /// <param name="terminal"></param>
        public void AddExistingTerminal(Terminal terminal)
        {
            AllTerminals.Add(terminal);
        }

        /// <summary>
        /// Creates a new Runway object and adds it to the airport’s AllRunways list.
        /// </summary>
        public Runway AddNewRunway(string runwayName)
        {
            Runway newRunway = new Runway(runwayName);
            AllRunways.Add(newRunway);
            return newRunway;
        }

        /// <summary>
        /// Adds a previously created Runway object to the airport’s AllRunways list.
        /// </summary>
        /// <param name="runway"></param>
        public void AddExistingRunway(Runway runway)
        {
            AllRunways.Add(runway);
        }

        /// <summary>
        /// Creates a new Taxi object and adds it to the airport’s AllTaxis list.
        /// </summary>
        public Taxi AddNewTaxi(string taxiName)
        {
            Taxi newTaxi = new Taxi(taxiName);
            AllTaxis.Add(newTaxi);
            return newTaxi;
        }

        /// <summary>
        /// Adds a previously created Taxi object to the airport’s AllTaxis list.
        /// </summary>
        /// <param name="taxi"></param>
        public void AddExistingTaxi(Taxi taxi)
        {
            AllTaxis.Add(taxi);
        }
        /// <summary>
        /// Adds a previously created Flight object to the airport’s AllFlights list.
        /// </summary>
        public void AddExistingFlight(Flight flight)
        {
            this.AllFlights.Add(flight);
        }

        /// <summary>
        /// Adds a given flight into the airport’s CompletedFlights list.
        /// </summary>
        public void AddCompletedFlight(Flight flight)
        {
            this.CompletedFlights.Add(flight);
        }
        /// <summary>
        /// Removes a Flight object from the airport’s AllFlights list. <see cref="AllFlights"> list.
        /// </summary>
        public void RemoveCompletedFlightFromAllFlights(Flight flight)
        {
            this.AllFlights.Remove(flight);
        }
        /// <summary>
        /// Prints details of all flights, including the flights’ Number, FlightFrequency and ScheduledDay.
        /// </summary>
        public void PrintAllFlights()
        {
            foreach(Flight flight in this.AllFlights)
            {
                Console.WriteLine($"{flight.Number} - {flight.Frequency} - {flight.ScheduledDay}");
            }
        }

        /// <summary>
        /// Creates a new Taxi and Gate. These need to be connected and are therefore put in the same method.
        /// The new Gate is connected to the Taxi, and the taxi is then added to AllTaxis.
        /// An alternative to using this method to create a new gate would be to use airport.GetAllTerminals() 
        /// to get a list of find all terminals, create a new Gate object, 
        /// then loop through the list of terminals to find one you want to add it to by using
        /// terminal.AddConnectedGate().
        /// </summary>
        /// <param name="gateName">The name of your gate</param>
        /// <param name="taxiName">The name of your taxi</param>
        public void AddNewConnectedGateAndTaxi(string gateName, string taxiName)
        {
            Taxi newTaxi = new Taxi(taxiName);
            Gate newGate = new Gate(gateName);
            newTaxi.AddConnectedGate(newGate);
            AllTaxis.Add(newTaxi);
        }

        public void AddPlaneToListOfAvailablePlanes(Plane plane)
        {
            this.ListOfPlanes.Add(plane);
        }

    }
}
