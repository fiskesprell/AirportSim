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

        public void simulateTime(Airport airport, DateTime start, DateTime end)
        {
            //Denne funker ikke og må fikses. Da jeg kjørte dette eksempelet så ble days = -3, og det er ikke riktig
            TimeSpan timeDifference = start - end;
            Console.WriteLine("Dette er timeDifference i simulateTime metoden: " + timeDifference);

            airport.setScheduledStartDate(start);
            airport.setScheduledEndDate(end);

            int days = timeDifference.Days;
            int hours = timeDifference.Hours;
            int minutes = timeDifference.Minutes;
            Console.WriteLine("Dette er days i simulateTime metoden: " + days);
            Console.WriteLine("Dette er hours i simulateTime metoden: " + hours);
            Console.WriteLine("Dette er minutes i simulateTime metoden: " + minutes);

            Console.WriteLine("I dette eksempelet skal days være 1, hours være 0, minutes være 0. Gjerne prøv med forskjellige verdier for å se at det funker");

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
