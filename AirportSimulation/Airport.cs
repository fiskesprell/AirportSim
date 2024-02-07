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
        public string AirportName { get; set; }
        /// <summary>
        /// List containing all terminals in this airport
        /// </summary>
        public List<Terminal> AllTerminals = new List<Terminal>();
        /// <summary>
        /// List containing all runways in this airport
        /// </summary>
        public List<Runway> AllRunways { get; set; } = new List<Runway>();
        /// <summary>
        /// List containing all taxiways in this airport
        /// </summary>
        public List<Taxi> AllTaxis { get; set; } = new List<Taxi>();
       /// <summary>
        /// List containing all flights in this airport
        /// </summary>
        public List<Flight> AllFlights { get; set; } = new List<Flight>();
       /// <summary>
        /// List containing all completed flights in this airport
        /// </summary>
        public List<Flight> CompletedFlights { get; set; } = new List<Flight> ();

        

        private DateTime ScheduledStartDate { get; set; }
        private DateTime ScheduledEndDate { get; set; }
            

        /// <summary>
        /// Constructor for making an airport
        /// </summary>
        /// <param name="AirportName"></param>
        public Airport(string airportName, string terminalName, string taxiName, string runwayName, string gateName)
        {
            this.AirportName = airportName;
            Terminal terminal = AddTerminal(terminalName);
            //terminal.setIsInternational(true);
            Taxi taxi = AddTaxi(taxiName);
            Runway runway = AddRunway(runwayName);
            Gate gate = terminal.AddGate(gateName);
            gate.AddTaxi(taxi);
            taxi.AddConnectedGate(gate);
            taxi.AddConnectedRunway(runway);
            runway.AddConnectedTaxi(taxi);
        }

        /// <summary>
        /// Calls the terminal constructor and adds the resulting object to the list of terminals
        /// </summary>
        public Terminal AddTerminal(string name)
        {
            Terminal newTerminal = new Terminal(name);
            AllTerminals.Add(newTerminal);
            return newTerminal;
        }

        /// <summary>
        /// Calls the runway constructor and adds the resulting object to the list of runways
        /// </summary>
        public Runway AddRunway(string name)
        {
            Runway newRunway = new Runway(name);
            AllRunways.Add(newRunway);
            return newRunway;
        }

        /// <summary>
        /// Calls the taxi constructor and adds the resulting object to the list of taxiways
        /// </summary>
        public Taxi AddTaxi(string name)
        {
            Taxi newTaxi = new Taxi(name);
            AllTaxis.Add(newTaxi);
            return newTaxi;
        }

        public void AddFlight(Flight flight)
        {
            this.AllFlights.Add(flight);
        }

        public List<Flight> GetAllFlights()
        {
            return this.AllFlights;
        }

        public List<Runway> GetAllRunways()
        {
            return AllRunways;
        }

        public List<Taxi> GetAllTaxis()
        {
            return AllTaxis;
        }

        public List<Terminal> GetAllTerminals()
        {
            return AllTerminals;
        }

        public DateTime GetScheduledStartDate()
        {
            return ScheduledStartDate;
        }

        public void SetScheduledStartDate(DateTime scheduledStartDate)
        {
            ScheduledStartDate = scheduledStartDate;
        }

        public DateTime GetScheduledEndDate()
        {
            return ScheduledEndDate;
        }

        public void SetScheduledEndDate(DateTime scheduledEndDate)
        {
            ScheduledEndDate = scheduledEndDate;
        }

        public string GetAirportName()
        {
            return AirportName;
        }

        public void AddCompletedFlight(Flight flight)
        {
            this.CompletedFlights.Add(flight);
        }

        public void RemoveCompletedFlightFromAllFlights(Flight flight)
        {
            this.AllFlights.Remove(flight);
        }

        public void PrintAllFlights()
        {
            foreach(Flight flight in this.AllFlights)
            {
                Console.WriteLine($"{flight.GetFlightNumber} - {flight.GetFlightFrequency} - {flight.GetScheduledDay}");
            }
        }


    }
}
