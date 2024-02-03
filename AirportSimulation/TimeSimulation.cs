//Code inspired by "Discrete Event Simulation: A Population Growth Example" By Arnaldo Perez
//https://learn.microsoft.com/en-us/archive/msdn-magazine/2016/march/csharp-discrete-event-simulation-a-population-growth-example

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public class TimeSimulation
    {
        public int ElapsedDays { get; set; } = 0;
        public int ElapsedHours { get; set; } = 0;
        public int ElapsedMinutes { get; set; } = 0;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public TimeSimulation()
        {}

        public void simulateTime(TimeSimulation timeSimulation, Airport airport, DateTime start, DateTime end)
        {
            this.StartDate = start;
            this.EndDate = end;

            TimeSpan timeDifference = end - start;

            airport.setScheduledStartDate(start);
            airport.setScheduledEndDate(end);

            int days = timeDifference.Days +1;
            int hours = timeDifference.Hours;
            int minutes = timeDifference.Minutes;

            //Legger inn en sjekk at det finnes minst et objekt av hver del av infrastrukturen, ellers vil ikke simuleringen begynne
            if (airport.getAllRunways().Count == 0 || airport.getAllTaxis().Count == 0 || airport.getAllTerminals().Count == 0)
            {
                throw new Exception("There is missing either a runway, terminal, or taxi");
            }
            int totalMinutes = 1440 * days + 60 * hours + minutes;

            for (int i = 0; i < totalMinutes; i++)
            {
                if (airport.getAllFlights().Count() > 0)
                {
                    foreach (var taxi in airport.getAllTaxis())
                    {
                        if (taxi.getTaxiQueue().Count() != 0)
                        {
                            taxi.removeFromQueue();
                        }
                    }

                    foreach (var runway in airport.getAllRunways())
                    {
                        if (runway.getRunwayQueue().Count() != 0)
                        {
                            runway.dequeueFlight();
                        }
                    }

                    foreach (var flight in airport.getAllFlights().ToList())
                    {
                        flight.updateElapsedTime(timeSimulation);
                        flight.flightSim(airport, timeSimulation);
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
                    Console.WriteLine("Simulation is now done");
                    Console.WriteLine("Day: " + ElapsedDays + " at: " +ElapsedHours + ":" + ElapsedMinutes);
                    //Vi må endre slik
                }
                
            }
        }

        public DateTime getStartDate()
        {
            return this.StartDate;
        }
    }
}
