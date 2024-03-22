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
    public class FlightPlaneGateArgs : EventArgs
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
        /// Gate object associated with the event.
        /// </summary>
        public Gate gate;

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
        /// Constructor for the FlightPlaneGateArgs class.
        /// </summary>
        /// <param name="flight">Flight object associated with the event.</param>
        /// <param name="plane">Plane object associated with the event.</param>
        /// <param name="gate">Gate object associated with the event.</param>
        /// <param name="ElapsedDays">Days elapsed since the simulation started.</param>
        /// <param name="ElapsedHours">Hours elapsed since the simulation started.</param>
        /// <param name="ElapsedMinutes">Minutes elapsed since the simulation started.</param>
        public FlightPlaneGateArgs(Flight flight, Plane plane, Gate gate, int ElapsedDays, int ElapsedHours, int ElapsedMinutes)
        {
            this.flight = flight;
            this.plane = plane;
            this.gate = gate;
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
                Console.WriteLine($"FLIGHT: {flight.Number}");
            }
            if (plane != null)
            {
                Console.WriteLine($"PLANE: {plane.TailNumber}");
            }
            if (gate != null)
            {
                Console.WriteLine($"GATE: {gate.GateName}");
            }

            Console.WriteLine($"ELAPSED TIME: {ElapsedDays} Days, {ElapsedHours} Hours, {ElapsedMinutes} Minutes.");
        }
    }
}
