using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetzachTech.AirportSim.EventArguments
{
    public class FlightPlaneRunwayArgs : EventArgs
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
        /// Runway object associated with the event.
        /// </summary>
        public Runway runway;

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

        // Constructor
        /// <summary>
        /// Constructor for the FlightPlaneRunwayArgs class.
        /// </summary>
        /// <param name="flight">Flight object associated with the event.</param>
        /// <param name="plane">Plane object associated with the event.</param>
        /// <param name="runway">Runway object associated with the event.</param>
        /// <param name="ElapsedDays">Days elapsed since the simulation started.</param>
        /// <param name="ElapsedHours">Hours elapsed since the simulation started.</param>
        /// <param name="ElapsedMinutes">Minutes elapsed since the simulation started.</param>
        public FlightPlaneRunwayArgs(Flight flight, Plane plane, Runway runway, int ElapsedDays, int ElapsedHours, int ElapsedMinutes)
        {
            this.flight = flight;
            this.plane = plane;
            this.runway = runway;
            this.ElapsedDays = ElapsedDays;
            this.ElapsedHours = ElapsedHours;
            this.ElapsedMinutes = ElapsedMinutes;
        }
    }
}
