//Code inspired by "Discrete Event Simulation: A Population Growth Example" By Arnaldo Perez
//https://learn.microsoft.com/en-us/archive/msdn-magazine/2016/march/csharp-discrete-event-simulation-a-population-growth-example

using AirportSimulation;
using AirportSimulationCl;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.Time
{

    /// <summary>
    /// Manages the simulation of time within an airport context, tracking elapsed time and simulating events within a specified period.
    /// </summary>
    public class TimeSimulation : INotifyPropertyChanged
    {

        /// <summary>
        /// The number of days that have elapsed in the simulation.
        /// </summary>
        private int _elapsedDays = 0;
        public int ElapsedDays
        { get => _elapsedDays; set => _elapsedDays = value; }

        /// <summary>
        /// The number of hours that have elapsed in the simulation.
        /// </summary>
        private int _elapsedHours = 0;
        public int ElapsedHours
        { get => _elapsedHours; set => _elapsedHours = value; }

        /// <summary>
        /// The number of minutes that have elapsed in the simulation.
        /// </summary>
        private int _elapsedMinutes = 0;
        public int ElapsedMinutes
        { get => _elapsedMinutes; set => _elapsedMinutes = value; }

        /// <summary>
        /// The starting date and time for the simulation.
        /// </summary>
        private DateTime _startDate;
        public DateTime StartDate
        { get => _startDate; set => _startDate = value; }

        /// <summary>
        /// The ending date and time for the simulation.
        /// </summary>
        private DateTime _endDate;
        public DateTime EndDate
        { get => _endDate; set => _endDate = value; }

        public event EventHandler TimeUpdated;

        protected virtual void OnTimeUpdated()
        {
            TimeUpdated?.Invoke(this, EventArgs.Empty);
        }


        public TimeSimulation()
        { }


        /// <summary>
        /// Simulates time at the given airport from the given startdate to the enddate. If timeconfigurations have been made, give the timeconfigmanager as argument.
        /// </summary>
        /// <param name="timeConfigManager"></param>
        /// <param name="airport"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <exception cref="InvalidScheduledTimeException"></exception>
        /// <exception cref="InvalidInfrastructureException"></exception>
        public void SimulateTime(TimeConfigManager timeConfigManager, Airport airport, DateTime start, DateTime end)
        {

            Console.WriteLine("The simulation has now started:");
            StartDate = start;
            EndDate = end;

            TimeSpan timeDifference = end - start;

            if (timeDifference.TotalSeconds < 0)
                throw new InvalidScheduledTimeException("\nThe end date cannot be before the start date");

            airport.ScheduledStartDate = start;
            airport.ScheduledEndDate = end;

            int days = timeDifference.Days + 1;
            int hours = timeDifference.Hours;
            int minutes = timeDifference.Minutes + 1;

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

                        flight.updateElapsedTime(this);
                        flight.FlightSim(timeConfigManager, airport, this);
                    }
                }

                System.Threading.Thread.Sleep(100);
                OnTimeUpdated();

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

                if (i == totalMinutes - 1)
                {
                    Console.WriteLine("\nSimulation is now done");

                }

            }
        }

        /// <summary>
        /// Simulates the time running in an airport, processing events from a start date to an end date in DateTime format. Overload without TimeConfigManager
        /// </summary>
        /// <param name="airport"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <exception cref="InvalidScheduledTimeException"></exception>
        /// <exception cref="InvalidInfrastructureException"></exception>
        public void SimulateTime(Airport airport, DateTime start, DateTime end)
        {

            Console.WriteLine("The simulation has now started:");
            StartDate = start;
            EndDate = end;

            TimeConfigManager timeConfigManager = new TimeConfigManager();

            TimeSpan timeDifference = end - start;

            if (timeDifference.TotalSeconds < 0)
                throw new InvalidScheduledTimeException("\nThe end date cannot be before the start date");

            airport.ScheduledStartDate = start;
            airport.ScheduledEndDate = end;

            int days = timeDifference.Days + 1;
            int hours = timeDifference.Hours;
            int minutes = timeDifference.Minutes + 1;

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

                        flight.updateElapsedTime(this);
                        flight.FlightSim(timeConfigManager, airport, this);
                    }
                }

                System.Threading.Thread.Sleep(100);
                OnTimeUpdated();

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

                if (i == totalMinutes - 1)
                {
                    Console.WriteLine("\nSimulation is now done");

                }

            }
        }
    }
}
