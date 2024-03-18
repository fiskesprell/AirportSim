using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.EventArguments
{
    public class FlightPlaneTaxiArgs : EventArgs
    {
        // todo: XML-comments
        // Instance Variables
        public Flight flight;

        public Plane plane;

        public Taxi taxi;

        public int ElapsedDays;

        public int ElapsedHours;

        public int ElapsedMinutes;

        // Constructor
        public FlightPlaneTaxiArgs(Flight flight, Plane plane, Taxi taxi, int ElapsedDays, int ElapsedHours, int ElapsedMinutes)
        {
            this.flight = flight;
            this.plane = plane;
            this.taxi = taxi;
            this.ElapsedDays = ElapsedDays;
            this.ElapsedHours = ElapsedHours;
            this.ElapsedMinutes = ElapsedMinutes;
        }

        // TODO: Delete? Test method.
        public void PrintAll()
        {
            if (flight != null)
            {
                Console.WriteLine($"FLIGHT: {this.flight.Number}");
            }
            if (plane != null)
            {
                Console.WriteLine($"PLANE: {this.plane.TailNumber}");
            }
            if (taxi != null)
            {
                Console.WriteLine($"RUNWAY: {this.taxi.TaxiName}");
            }

            Console.WriteLine($"ELAPSED TIME: {this.ElapsedDays} Days, {this.ElapsedHours} Hours, {this.ElapsedMinutes} Minutes.");
        }
    }
}
