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
        // todo: XML-comments
        // Instance Variables
        public Flight flight;

        public Plane plane;

        public Gate gate;

        public int ElapsedDays;

        public int ElapsedHours;

        public int ElapsedMinutes;

        // Constructor
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
