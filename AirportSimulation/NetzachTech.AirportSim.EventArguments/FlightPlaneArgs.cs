using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Plane = NetzachTech.AirportSim.Infrastructure.Plane;

namespace NetzachTech.AirportSim.EventArguments
{
    public class FlightPlaneArgs : EventArgs
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
        /// Constructor for the FlightPlaneArgs class.
        /// </summary>
        /// <param name="flight">Flight object associated with the event.</param>
        /// <param name="plane">Plane object associated with the event.</param>
        /// <param name="ElapsedDays">Days elapsed since the simulation started.</param>
        /// <param name="ElapsedHours">Hours elapsed since the simulation started.</param>
        /// <param name="ElapsedMinutes">Minutes elapsed since the simulation started.</param>
        public FlightPlaneArgs(Flight flight, Plane plane, int ElapsedDays, int ElapsedHours, int ElapsedMinutes)
        {
            this.flight = flight;
            this.plane = plane;
            this.ElapsedDays = ElapsedDays;
            this.ElapsedHours = ElapsedHours;
            this.ElapsedMinutes = ElapsedMinutes;
        }
    }
}
