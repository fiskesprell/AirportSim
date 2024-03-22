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
        /// <summary>
        /// Flight object associated with the event.
        /// </summary>
        public Flight flight;
        /// <summary>
        /// Plane object associated with the event.
        /// </summary>
        public Plane plane;
        /// <summary>
        /// Taxi object associated with the event.
        /// </summary>
        public Taxi taxi;
        /// <summary>
        /// Days elapsed since the simulation started.
        /// </summary>
        public int ElapsedDays;
        /// <summary>
        /// Hours elapsed since the simulation started.
        /// </summary>
        public int ElapsedHours;
        /// <summary>
        /// Minutes elapsed since the simulation started.
        /// </summary>
        public int ElapsedMinutes;

        /// <summary>
        /// Constructor for the FlightPlaneTaxiArgs class.
        /// </summary>
        /// <param name="flight">Flight object associated with the event.</param>
        /// <param name="plane">Plane object associated with the event.</param>
        /// <param name="taxi">Taxi object associated with the event.</param>
        /// <param name="ElapsedDays">Days elapsed since the simulation started.</param>
        /// <param name="ElapsedHours">Hours elapsed since the simulation started.</param>
        /// <param name="ElapsedMinutes">Minutes elapsed since the simulation started.</param>
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
        /// <summary>
        /// Prints all instance variables associated with the event.
        /// </summary>
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
