using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class FlightEventSubscriber
    {
        public void SubscribeToFlightEvents(Flight flight) 
        {
            flight.Events.PlaneAssignedToFlight += HandlePlaneAssignedToFlight;
            flight.Events.StartedBoarding += HandleStartedBoarding;
            flight.Events.TookOff += HandleTookOff;
        }

        private void HandlePlaneAssignedToFlight(object sender, FlightEventArgs args)
        {
            Console.WriteLine($"Flight {args.Name} has been assigned a plane: {args.Plane}");
        }

        private void HandleStartedBoarding(object sender, FlightEventArgs e)
        {
            Console.WriteLine($"Flight {e.Name} has started boarding.");
        }

        private void HandleTookOff(object sender, FlightEventArgs e)
        {
            Console.WriteLine($"Flight {e.Name} has taken off.");
            Console.WriteLine("This is an event test");
        }
    }
}
