﻿using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.Infrastructure
{
    public class Airport : IAirport
    {
        /// <summary>
        /// The name of your Airport.
        /// </summary>
        private string _airportName;
        public string AirportName
        { get => _airportName; set => _airportName = value; }

        /// <summary>
        /// List containing all terminals in this airport
        /// </summary>
        private ObservableCollection<Terminal> _allTerminals = new ObservableCollection<Terminal>();
        public ObservableCollection<Terminal> AllTerminals
        { get => _allTerminals; }

        /// <summary>
        /// List containing all runways in this airport
        /// </summary>
        private ObservableCollection<Runway> _allRunways = new ObservableCollection<Runway>();
        public ObservableCollection<Runway> AllRunways
        { get => _allRunways; }

        /// <summary>
        /// List containing all taxiways in this airport
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
        /// List containing all completed flights in this airport
        /// </summary>
        private ObservableCollection<Flight> _completedFlights = new ObservableCollection<Flight>();
        public ObservableCollection<Flight> CompletedFlights
        { get => _completedFlights; }

        /// <summary>
        /// Gets and sets the scheduled start date
        /// </summary>
        /// <remarks>
        /// This property represents the scheduled start date for the simulation
        /// </remarks>
        private DateTime _scheduledStartDate;
        public DateTime ScheduledStartDate
        { get => _scheduledStartDate; set => _scheduledStartDate = value; }

        /// <summary>
        /// Gets and sets the scheduled end date
        /// </summary>
        /// <remarks>
        /// This property represent the end date fort the simulation
        /// </remarks>
        private DateTime _scheduledEndDate;
        public DateTime ScheduledEndDate
        { get => _scheduledEndDate; set => _scheduledEndDate = value; }

        /// <summary>
        /// Gets the list of planes
        /// </summary>
        /// <remarks>
        /// A list of planes that are have been or are at this airport
        /// </remarks>
        private ObservableCollection<Plane> _listOfPlanes = new ObservableCollection<Plane>();
        public ObservableCollection<Plane> ListOfPlanes
        { get => _listOfPlanes; }

        public Airport() { }

        public Airport(string airportName)
        {
            AirportName = airportName;
        }

        /// <summary>
        /// Constructor for making an airport
        /// </summary>
        /// <param name="AirportName"></param>
        public Airport(string airportName, string terminalName, string taxiName, string runwayName, string gateName)
        {
            AirportName = airportName;
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
        /// Calls the terminal constructor and adds the resulting object to the list of terminals
        /// </summary>
        public Terminal AddNewTerminal(string terminalName)
        {
            Terminal newTerminal = new Terminal(terminalName);
            AllTerminals.Add(newTerminal);
            newTerminal.Airport = this;
            return newTerminal;
        }

        /// <summary>
        /// Adds a terminalobject to the list of terminals
        /// </summary>
        /// <param name="terminal"></param>
        public void AddExistingTerminal(Terminal terminal)
        {
            AllTerminals.Add(terminal);
            terminal.Airport = this;
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
        public Taxi AddNewTaxi(string taxiName, TaxiwayType taxiwayType)
        {
            Taxi newTaxi = new Taxi(taxiName,taxiwayType);
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
            AllFlights.Add(flight);
        }

        /// <summary>
        /// Adds a flight to the airport's completed flights
        /// </summary>
        public void AddCompletedFlight(Flight flight)
        {
            CompletedFlights.Add(flight);
        }
        /// <summary>
        /// Removes a completed flight from the airport's <see cref="AllFlights"> list.
        /// </summary>
        public void RemoveCompletedFlightFromAllFlights(Flight flight)
        {
            AllFlights.Remove(flight);
        }
        /// <summary>
        /// Prints flight number, frequency and scheduled day for all flights.
        /// </summary>
        public void PrintAllFlights()
        {
            foreach (Flight flight in AllFlights)
            {
                Console.WriteLine($"{flight.Number} - {flight.Frequency} - {flight.ScheduledDay}");
            }
        }

        
        /// <summary>
        /// Adds a planeobject to the list of available planes at this airport.
        /// </summary>
        /// <param name="plane"></param>
        public void AddPlaneToListOfAvailablePlanes(Plane plane)
        {
            ListOfPlanes.Add(plane);

        }

        /// <summary>
        /// Removes a plane from the list of available planes at this airport.
        /// </summary>
        /// <param name="plane"></param>
        public void RemovePlaneFromListOfPlanes(Plane plane)
        {
            ListOfPlanes.Remove(plane);
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
    }
}
