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
        List<Taxi> AllTaxis { get; }
        /// <summary>
        /// A list containing all the runways in this airport
        /// </summary>
        List<Runway> AllRunways { get; }

        /// <summary>
        /// A list containing all the flights in this airport
        /// </summary>
        List<Flight> AllFlights { get; }
        

        /// <summary>
        /// Method to add a runway to this airport
        /// </summary>
        /// <param name="name"></param>
        Runway AddNewRunway(string name);
        
        /// <summary>
        /// Method to add a taxi to this airport
        /// </summary>
        /// <param name="name"></param>
        Taxi AddNewTaxi(string name);
        
        /// <summary>
        /// Method to add a terminal to this airport
        /// </summary>
        /// <param name="name"></param>
        Terminal AddNewTerminal(string name);
        
        /// <summary>
        /// Method to add a flight to this airport
        /// </summary>
        void AddNewFlight(Flight flight);


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
