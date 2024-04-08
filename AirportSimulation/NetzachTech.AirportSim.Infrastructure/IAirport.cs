using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;

namespace NetzachTech.AirportSim.Infrastructure
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
        ObservableCollection<Taxi> AllTaxis { get; }
        /// <summary>
        /// A list containing all the runways in this airport
        /// </summary>
        ObservableCollection<Runway> AllRunways { get; }

        /// <summary>
        /// A list containing all the flights in this airport
        /// </summary>
        ObservableCollection<Flight> AllFlights { get; }


        /// <summary>
        /// Method to add a runway to this airport
        /// </summary>
        /// <param name="name"></param>
        Runway AddNewRunway(string name);

        /// <summary>
        /// Method to add a taxi to this airport
        /// </summary>
        /// <param name="name"></param>
        Taxi AddNewTaxi(string name, TaxiwayType taxiwayType);

        /// <summary>
        /// Method to add a terminal to this airport
        /// </summary>
        /// <param name="name"></param>
        Terminal AddNewTerminal(string name);

        /// <summary>
        /// Method to add a flight to this airport
        /// </summary>
        void AddExistingFlight(Flight flight);

    }
}
