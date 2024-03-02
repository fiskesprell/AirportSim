//Code inspired by "Discrete Event Simulation: A Population Growth Example" By Arnaldo Perez
//https://learn.microsoft.com/en-us/archive/msdn-magazine/2016/march/csharp-discrete-event-simulation-a-population-growth-example

using AirportSimulationCl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{

    /// <summary>
    /// Manages the simulation of time within an airport context, tracking elapsed time and simulating events within a specified period.
    /// </summary>
    public class TimeSimulation
    {

        /// <summary>
        /// The number of days that have elapsed in the simulation.
        /// </summary>
        private int _elapsedDays = 0;
        public int ElapsedDays
        {get => _elapsedDays;set => _elapsedDays = value;}

        /// <summary>
        /// The number of hours that have elapsed in the simulation.
        /// </summary>
        private int _elapsedHours = 0;
        public int ElapsedHours
        {get => _elapsedHours;set => _elapsedHours = value;}

        /// <summary>
        /// The number of minutes that have elapsed in the simulation.
        /// </summary>
        private int _elapsedMinutes = 0;
        public int ElapsedMinutes
        {get => _elapsedMinutes;set => _elapsedMinutes = value;}

        /// <summary>
        /// The starting date and time for the simulation.
        /// </summary>
        private DateTime _startDate;
        public DateTime StartDate
        {get => _startDate;set => _startDate = value;}

        /// <summary>
        /// The ending date and time for the simulation.
        /// </summary>
        private DateTime _endDate;
        public DateTime EndDate
        {get => _endDate;set => _endDate = value;}


        public TimeSimulation()
        {}

        /// <summary>
        /// Simulates the time running in an airport, processing events from a start date to an end date in DateTime format.
        /// </summary>
        /// <param name="timeSimulation">The TimeSimulation instance.</param>
        /// <param name="airport">The airport to simulate.</param>
        /// <param name="start">The start date and time of the simulation.</param>
        /// <param name="end">The end date and time of the simulation.</param>
        public void SimulateTime(Airport airport, DateTime start, DateTime end)
        {
            this.StartDate = start;
            this.EndDate = end;

            TimeSpan timeDifference = end - start;

            //Eksempel pÅEhvordan bruke custom exceptions
            if (timeDifference.TotalSeconds < 0)
                throw new InvalidScheduledTimeException("\nThe end date cannot be before the start date");

            airport.ScheduledStartDate = start;
            airport.ScheduledEndDate = end;

            int days = timeDifference.Days +1;
            int hours = timeDifference.Hours;
            int minutes = timeDifference.Minutes+1;

            //Legger inn en sjekk at det finnes minst et objekt av hver del av infrastrukturen, ellers vil ikke simuleringen begynne
            if (airport.AllRunways.Count == 0)
            {
                throw new InvalidInfrastructureException("\nThere are no runways in this airport. Try adding one with airportObject.addRunway(string name).\n");
            }

            else if (airport.AllTaxis.Count == 0)
            {
                throw new InvalidInfrastructureException("\nThere are no taxiways in this airport. Try adding one with airportObject.addTaxi(string name)\n");
            }

            else if (airport.AllTerminals.Count == 0)
            {
                throw new InvalidInfrastructureException("\nThere are no terminals in this airport. Try adding one with airportObject.addTerminal(string name)\n");
            }
            int totalMinutes = 1440 * days + 60 * hours + minutes;

            for (int i = 0; i < totalMinutes; i++)
            {

                if (airport.AllFlights.Count() > 0)
                {
                    foreach (var taxi in airport.AllTaxis)
                    {
                        if (taxi.TaxiQueue.Count() != 0)
                        {
                            taxi.RemoveFromTaxiQueue();
                        }
                    }

                    foreach (var runway in airport.AllRunways)
                    {
                        if (runway.RunwayQueue.Count() != 0)
                        {
                            runway.RemoveFromRunwayQueue();
                        }
                    }

                    foreach (var flight in airport.AllFlights.ToList())
                    {
                        //Burde kanskje ha en sjekk her for at flyet har de riktige verdiene som trengs for ÅEkj¯re simuleringen?
                        //Da kan vi lettere gi en exception hvis det mangler f.eks. FlightDirection eller annen viktig property
                        flight.updateElapsedTime(this);
                        flight.FlightSim(airport, this);
                    }
                }

                if (ElapsedMinutes == 59)
                {
                    ElapsedHours += 1;
                    ElapsedMinutes = -1;
                }

                if (ElapsedHours == 24)
                {
                    ElapsedDays += 1;
                    ElapsedHours = 0;
                }
                ElapsedMinutes += 1;

                if (i == totalMinutes -1)
                {
                    Console.WriteLine("\nSimulation is now done");
                    
                }
                
            }
        }
    }
}
