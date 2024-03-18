using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.EventArguments
{
    public class FlightPlaneRunwayArgs : EventArgs
    {
        // todo: XML-comments
        // Instance Variables
        public Flight flight;

        public Plane plane;

        public Runway runway;

        public int ElapsedDays;

        public int ElapsedHours;

        public int ElapsedMinutes;

        // Constructor
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
