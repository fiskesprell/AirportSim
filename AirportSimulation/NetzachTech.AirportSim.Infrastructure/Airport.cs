using AirportSimulationCl;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// List of Terminal objects in this airport.
        /// </summary>
        private ObservableCollection<Terminal> _allTerminals = new ObservableCollection<Terminal>();
        public ObservableCollection<Terminal> AllTerminals
        { get => _allTerminals; }

        /// <summary>
        /// List of Runway objects in this airport.
        /// </summary>
        private ObservableCollection<Runway> _allRunways = new ObservableCollection<Runway>();
        public ObservableCollection<Runway> AllRunways
        { get => _allRunways; }

        /// <summary>
        /// List of Taxi objects of in this airport.
        /// </summary>
        private ObservableCollection<Taxi> _allTaxis = new ObservableCollection<Taxi>();
        public ObservableCollection<Taxi> AllTaxis
        { get => _allTaxis; }

        /// <summary>
        /// List contaning all gates in this airport
        /// </summary>
        private ObservableCollection<Gate> _allGates = new ObservableCollection<Gate>();
        public ObservableCollection<Gate> AllGates
        { get => _allGates; }

        /// <summary>
        /// List containing all flights in this airport
        /// </summary>
        private ObservableCollection<Flight> _allFlights = new ObservableCollection<Flight>();
        public ObservableCollection<Flight> AllFlights
        { get => _allFlights; }

        /// <summary>
        /// List of flights that have completed their journey.
        /// </summary>
        private ObservableCollection<Flight> _completedFlights = new ObservableCollection<Flight>();
        public ObservableCollection<Flight> CompletedFlights
        { get => _completedFlights; }

        /// <summary>
        /// Gets and sets the scheduled start date. DateTime object that represents what date the simulation is ending.
        /// </summary>
        private DateTime _scheduledStartDate;
        public DateTime ScheduledStartDate
        {get => _scheduledStartDate; set => _scheduledStartDate = value;}

        /// <summary>
        /// Gets and sets the scheduled end date. DateTime object that represents what date the simulation is ending.
        /// </summary>
        private DateTime _scheduledEndDate;
        public DateTime ScheduledEndDate
        {get => _scheduledEndDate; set => _scheduledEndDate = value;}

        /// <summary>
        /// Gets the list of planes. List of planes that have been or are currently at the airport. This list is accessible for additions but not direct reassignment from outside the class.
        /// </summary>
        /// <remarks>
        /// A list of planes that are have been or are at this airport
        /// </remarks>
        private ObservableCollection<Plane> _listOfPlanes = new ObservableCollection<Plane>();
        public ObservableCollection<Plane> ListOfPlanes
        { get => _listOfPlanes; }

        /// <summary>
        /// Constructor for making an empty airport.
        /// </summary>
        public Airport() { }

        /// <summary>
        /// Constructor for making a named, but otherwise empty airport.
        /// </summary>
        /// <param name="airportName">The name of the airport.</param>
        public Airport(string airportName)
        {
            this.AirportName = airportName;
        }

        /// <summary>
        /// Constructor for making an Airport object with basic infrastructure. These Terminal, Taxi, Runway, and Gate objects will be connected to each other and assigned to this airport. By default, the Terminal object will not be allowed to serve international flights.
        /// </summary>
        /// <param name="airportName">The name of the airport.</param>
        /// <param name="terminalName">The name for the initial terminal.</param>
        /// <param name="taxiName">The name for the initial taxi pathway.</param>
        /// <param name="runwayName">The name for the initial runway.</param>
        /// <param name="gateName">The name for the initial gate connected to the terminal.</param>
        public Airport(string airportName, string terminalName, string taxiName, string runwayName, string gateName)
        {
            this.AirportName = airportName;
            Terminal terminal = AddNewTerminal(terminalName);
            //terminal.setIsInternational(true);
            Taxi taxi = AddNewTaxi(taxiName, TaxiwayType.Main);
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
            newTerminal.Airport = this;
            return newTerminal;
        }

        /// <summary>
        /// Adds a previously created Terminal object to the airport’s AllTerminals list.
        /// </summary>
        /// <param name="terminal"></param>
        public void AddExistingTerminal(Terminal terminal)
        {
            AllTerminals.Add(terminal);
            terminal.Airport = this;
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
        public Taxi AddNewTaxi(string taxiName, TaxiwayType taxiwayType)
        {
            Taxi newTaxi = new Taxi(taxiName, TaxiwayType.Main);
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
            Taxi newTaxi = new Taxi(taxiName, TaxiwayType.Main);
            Gate newGate = new Gate(gateName);
            newTaxi.AddConnectedGate(newGate);
            AllTaxis.Add(newTaxi);
        }

        /// <summary>
        /// Removes a plane from the list of available planes at this airport.
        /// </summary>
        /// <param name="plane"></param>
        public void RemovePlaneFromListOfPlanes(Plane plane)
        {
            ListOfPlanes.Remove(plane);
        }

        public void AddPlaneToListOfAvailablePlanes(Plane plane)
        {
            this.ListOfPlanes.Add(plane);
        }

        /// <summary>
        /// Finds and returns a terminalobject if the name matches with any of the created objects. Returns null if no matches were found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Terminal FindTerminal(string name)
        {
            foreach(var terminal in AllTerminals)
            {
                if (terminal.TerminalName.Equals(name))
                    return terminal;
            }
            return null;
        }

        /// <summary>
        /// Finds and returns a runwayobject if the name matches with any of the created objects. Returns null if no matches were found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Runway FindRunway(string name)
        {
            foreach (var runway in AllRunways)
            {
                if (runway.RunwayName.Equals(name))
                    return runway;
            }
            return null;
        }

        /// <summary>
        /// Finds and returns a taxiobjects if the name matches with any of the created objects. Returns null if no matches were found.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Taxi FindTaxi(string name)
        {
            foreach (var taxi in AllTaxis)
            {
                if (taxi.TaxiName.Equals(name))
                    return taxi;
            }
            return null;
        }

        /// <summary>
        /// Loops through all the gates at this airport and finds one with a matching name to the name given as argument.
        /// Loops through all the taxis in this airport and finds one with a matching name to the name given as argument.
        /// If both are found, adds a to-way connection between the two.
        /// </summary>
        /// <param name="gateName"></param>
        /// <param name="taxiName"></param>
        public void ConnectGateAndTaxi(string gateName, string taxiName)
        {
            Gate selectedGate = null;
            Taxi selectedTaxi = null;

            foreach(var taxi in AllTaxis)
            {
                if (taxi.TaxiName.Equals(taxiName))
                    selectedTaxi = taxi;
            }

            foreach(var terminal in AllTerminals)
                foreach(var gate in terminal.ConnectedGates)
                {
                    if (gate.GateName.Equals(gateName))
                        selectedGate = gate;
                }

            if (selectedGate != null && selectedTaxi != null)
            {
                selectedTaxi.AddConnectedGate(selectedGate);
                selectedGate.AddTaxi(selectedTaxi);
            }

        }

        /// <summary>
        /// Adds a two-way connection beteen the given gate and taxi.
        /// </summary>
        /// <param name="gate"></param>
        /// <param name="taxi"></param>
        public void ConnectGateAndTaxi(Gate gate, Taxi taxi) 
        {
            gate.AddTaxi(taxi);
            taxi.AddConnectedGate(gate);
        }

        /// <summary>
        /// Loops through all the taxis in the airport and finds one that matches the given name. Loops though all the runways 
        /// and finds one that matches the given name. If both a taxi and a runway is found, it will add a two-way connection between those two.
        /// </summary>
        /// <param name="taxiName"></param>
        /// <param name="runwayName"></param>
        public void ConnectTaxiAndRunway(string taxiName, string runwayName)
        {
            Taxi selectedTaxi = null;
            Runway selectedRunway = null;

            foreach(var taxi in AllTaxis)
                if(taxi.TaxiName.Equals(taxiName))
                    selectedTaxi = taxi;

            foreach(var runway in AllRunways)
                if (runway.RunwayName.Equals(runwayName))
                    selectedRunway = runway;

            if (selectedRunway != null && selectedTaxi != null)
            {
                selectedTaxi.AddConnectedRunway(selectedRunway);
                selectedRunway.AddConnectedTaxi(selectedTaxi);
            }
        }

        /// <summary>
        /// Adds a two-way connection beteen the given taxi and runway.
        /// </summary>
        /// <param name="taxi"></param>
        /// <param name="runway"></param>
        public void ConnectTaxiAndRunway(Taxi taxi, Runway runway)
        {
            taxi.AddConnectedRunway(runway);
            runway.AddConnectedTaxi(taxi);
        }

        /// <summary>
        /// Loops through all the temrinals in the airport and finds a terminal that matches the name given as argument, and adds a new gate to that temrinal.
        /// </summary>
        /// <param name="gateName"></param>
        /// <param name="terminalName"></param>
        public void AddNewGate(string gateName, string terminalName)
        {
            foreach (var terminal in AllTerminals)
                if (terminal.TerminalName.Equals(terminalName))
                    terminal.AddNewGate(gateName);

        }

        /// <summary>
        /// Adds a new gate to the terminal gives as argument.
        /// </summary>
        /// <param name="gateName"></param>
        /// <param name="terminal"></param>
        public void AddNewGate(string gateName, Terminal terminal)
        {
            terminal.AddNewGate(gateName);

        }

        public void AddExistingGateToTerminal(Gate gate, string terminalName)
        {
            foreach(var terminal in AllTerminals)
                if (terminal.TerminalName.Equals(terminalName))
                    terminal.AddExistingGate(gate);
        }
    }
}
