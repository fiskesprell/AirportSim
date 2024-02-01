using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public interface IAirport
    {
        /// <summary>
        /// The name of your Airport.
        /// </summary>
        string AirportName { get; }
        /// <summary>
        /// A list containing all the taxis in this airport
        /// </summary>
        List<Taxi> AllTaxis { get; set; }
        /// <summary>
        /// A list containing all the runways in this airport
        /// </summary>
        List<Runway> AllRunways { get; set; }
        /// <summary>
        /// A list containing all the gates in this airport
        /// </summary>
        List<Gate> AllGates { get; set; }
        /// <summary>
        /// A list containing all the flights in this airport
        /// </summary>
        List<Flight> AllFlights { get; set; }
        /// <summary>
        /// This property keeps track of the days that have passed in the simulation
        /// </summary>
        int ElapsedDays { get; set; }
        /// <summary>
        /// This property keeps track of the hours that have passed in the simulation
        /// </summary>
        int ElapsedHours { get; set; }
        /// <summary>
        /// This property keeps track of the minutes that have passed in the simulation
        /// </summary>
        int ElapsedMinutes { get; set; }

        /// <summary>
        /// Method to add a runway to this airport
        /// </summary>
        /// <param name="name"></param>
        void addRunway(string name);
        
        /// <summary>
        /// Method to add a taxi to this airport
        /// </summary>
        /// <param name="name"></param>
        void addTaxi(string name);
        
        /// <summary>
        /// Method to add a terminal to this airport
        /// </summary>
        /// <param name="name"></param>
        Terminal addTerminal(string name);
        
        /// <summary>
        /// Method to add a flight to this airport
        /// </summary>
        void addFlight();
        
        /// <summary>
        /// Method to add a gate to this airport
        /// </summary>
        /// <param name="name"></param>
        void addGate(string name);

    }
}
