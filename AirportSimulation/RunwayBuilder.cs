using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class RunwayBuilder
    {
        private readonly Runway _runway = new Runway();

        /// <summary>
        /// Sets the name of the runway.
        /// </summary>
        /// <param name="name">The name to set for the runway.</param>
        /// <returns></returns>
        public RunwayBuilder WithName(string name)
        {
            _runway.RunwayName = name;
            return this;
        }

        /// <summary>
        /// Adds taxiway to the ConnectedTaxi list.
        /// </summary>
        /// <param name="taxi">The taxiway to connect.</param>
        /// <returns></returns>
        public RunwayBuilder AddConnectedTaxi(Taxi taxi)
        {
            _runway.ConnectedTaxi.Add(taxi);
            return this;
        }

        /// <summary>
        /// Sets the availability of the runway.
        /// </summary>
        /// <param name="isAvailable">The availability status to set.</param>
        /// <returns></returns>
        public RunwayBuilder IsAvailable(bool isAvailable)
        {
            _runway.IsAvailable = isAvailable;
            return this;
        }

        /// <summary>
        /// Adds a flight to the runway queue.
        /// </summary>
        /// <param name="flight">The flight to add to the RunwayQueue.</param>
        /// <returns></returns>
        public RunwayBuilder AddFlightInQueue(Flight flight)
        {
            _runway.RunwayQueue.Enqueue(flight);
            return this;
        }

        /// <summary>
        /// Sets the flight currently on the runway.
        /// </summary>
        /// <param name="flight">The flight to set on the runway.</param>
        /// <returns></returns>
        public RunwayBuilder AddFlightOnRunway(Flight flight)
        {
            _runway.FlightOnRunway = flight;
            _runway.IsAvailable = false;
            return this;
        }

        /// <summary>
        /// Builds and returns the configured Runway object.
        /// </summary>
        /// <returns>The built Runway object.</returns>
        public Runway Build()
        {
            return _runway;
        }
    }
}
