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

        public TimeSimulation()
        {}

        private void SimulateTime(Airport airport, DateTime start, DateTime end)
        {
            TimeSpan timeDifference = start - end;

            int days = timeDifference.Days;
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
                foreach(var flight in airport.getAllFlights()) {
                    flight.updateElapsedTime(airport);
                }
                if (ElapsedMinutes == 60)
                {
                    ElapsedHours += 1;
                    ElapsedMinutes = 0;
                }

                if (ElapsedHours == 24)
                {
                    ElapsedDays += 1;
                    ElapsedHours = 0;
                }
                ElapsedMinutes += 1;
            }
        }
    }
}
