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


        /// <summary>
        /// Gets the name of the airport.
        /// </summary>
        /// <returns>A string containing the name of the airport.</returns>
        string GetAirportName();

        /// <summary>
        /// Retrieves all taxis available at the airport.
        /// </summary>
        /// <returns>A list of <see cref="Taxi"/> objects representing all taxis at the airport.</returns>
        List<Taxi> GetAllTaxis();

        /// <summary>
        /// Retrieves all runways at the airport.
        /// </summary>
        /// <returns>A list of <see cref="Runway"/> objects representing all runways at the airport.</returns>
        List<Runway> GetAllRunways();

        /// <summary>
        /// Retrieves all flights associated with the airport.
        /// </summary>
        /// <returns>A list of <see cref="Flight"/> objects representing all flights.</returns>
        List<Flight> GetAllFlights();




    }
}
