//Code inspired by "Discrete Event Simulation: A Population Growth Example" By Arnaldo Perez
//https://learn.microsoft.com/en-us/archive/msdn-magazine/2016/march/csharp-discrete-event-simulation-a-population-growth-example

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class TimeSimulation
    {
        public int ElapsedDays { get; set } = 0;
        public int ElapsedHours { get; set } = 0;
        public int ElapsedMinutes { get; set } = 0;

        public TimeSimulation()
        {}

        private void SimulateTime(Airport airport, int days, int hours, int minutes)
        {
            //Legger inn en sjekk at det finnes minst et objekt av hver del av infrastrukturen, ellers vil ikke simuleringen begynne
            if (airport.allRunways.Count == 0 || airport.allTaxis.Count == 0 || airport.allTerminals.Count == 0)
            {
                throw new Exception "There is missing either a runway, terminal, or taxi";
            }
            int totalMinutes = 1440 * days + 60 * hours + minutes;

            for (int i = 0; i < totalMinutes; i++)
            {
                for each(var flight in airport.AllFlights) {
                    flight.updateElapsedTime(this);
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
