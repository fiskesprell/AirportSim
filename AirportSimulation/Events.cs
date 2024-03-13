using AirportSimulation;
using AirportSimulationCl.NetzachTech.AirportSim.Infrastructure;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    internal class Events
    {
        // Delegate
        public delegate void FlightEventHandler(Flight Flight, Plane plane);

        // Events (ingen håndtering enda)
        public event FlightEventHandler? FlightGetsAssignedGate;
        public event FlightEventHandler? FlightGetsAssignedTaxi;
        public event FlightEventHandler? FlightGetsAssignedRunway;
        public event FlightEventHandler? FlightHasLeftRunway;
        public event FlightEventHandler? FlightHasLanded;
        public event FlightEventHandler? FlightHasBegunOnboarding;
        public event FlightEventHandler? FlightHasBegunOffloading;
        

    }
}
