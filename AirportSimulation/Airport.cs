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
        /// The name of your Airport.
        /// </summary>
        private string _airportName;
        public string AirportName
        {get => _airportName;set => _airportName = value;}

        /// <summary>
        /// List containing all terminals in this airport
        /// </summary>
        private List<Terminal> _allTerminals = new List<Terminal>();
        public List<Terminal> AllTerminals
        {get => _allTerminals;}

        /// <summary>
        /// List containing all runways in this airport
        /// </summary>
        private List<Runway> _allRunways = new List<Runway>();
        public List<Runway> AllRunways
        { get => _allRunways;}

        /// <summary>
        /// List containing all taxiways in this airport
        /// </summary>
        private List<Taxi> _allTaxis = new List<Taxi>();
        public List<Taxi> AllTaxis
        {get => _allTaxis;}

        /// <summary>
        /// List containing all flights in this airport
        /// </summary>
        private List<Flight> _allFlights = new List<Flight>();
        public List<Flight> AllFlights
        {get => _allFlights;}

        /// <summary>
        /// List containing all completed flights in this airport
        /// </summary>
        private List<Flight> _completedFlights = new List<Flight>();
        public List<Flight> CompletedFlights
        {get => _completedFlights;}

        /// <summary>
        /// Gets and sets the scheduled start date
        /// </summary>
        /// <remarks>
        /// This property represents the scheduled start date for the simulation
        /// </remarks>
        private DateTime _scheduledStartDate;
        public DateTime ScheduledStartDate
        {get => _scheduledStartDate; set => _scheduledStartDate = value;}

        /// <summary>
        /// Gets and sets the scheduled end date
        /// </summary>
        /// <remarks>
        /// This property represent the end date fort the simulation
        /// </remarks>
        private DateTime _scheduledEndDate;
        public DateTime ScheduledEndDate
        {get => _scheduledEndDate; set => _scheduledEndDate = value;}

        /// <summary>
        /// Gets the list of planes
        /// </summary>
        /// <remarks>
        /// A list of planes that are have been or are at this airport
        /// </remarks>
        private List<Plane> _listOfPlanes = new List<Plane> ();
        public List<Plane> ListOfPlanes
        {get => _listOfPlanes;}

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
        public void AddExistingFlight(Flight flight)
        {
            this.AllFlights.Add(flight);
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
