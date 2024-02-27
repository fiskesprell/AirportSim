using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public class TaxiBuilder
    {
        private readonly Taxi _taxi = new Taxi();


        /// <summary>
        /// Sets the name of the taxiway.
        /// </summary>
        /// <param name="name">The name to set for the taxiway</param>
        /// <returns></returns>
        public TaxiBuilder AddTaxiName(string name)
        {
            _taxi.TaxiName = name;
            return this;
        }

        /// <summary>
        /// Adds a gate to the taxi's ConnectedGates list.
        /// </summary>
        /// <param name="gate">The gate object to add.</param>
        /// <returns></returns>

        public TaxiBuilder AddConnectedGate(Gate gate)
        {
            _taxi.ConnectedGates.Add(gate);
            return this;
        }

        /// <summary>
        /// Adds a runway to the taxi's ConnectedGates list.
        /// </summary>
        /// <param name="runway">The runway object to add.</param>
        /// <returns></returns>
        public TaxiBuilder AddConnectedRunway(Runway runway)
        {
            _taxi.ConnectedRunways.Add(runway);
            return this;
        }

        /// <summary>
        /// Sets the availability of the taxiway.
        /// </summary>
        /// <param name="isAvailable">The availability status to set.</param>
        /// <returns>The builder instance for chaining.</returns>
        public TaxiBuilder IsAvailable(bool isAvailable)
        {
            _taxi.IsAvailable = isAvailable;
            return this;
        }

        /// <summary>
        /// Returns the built Taxi object.
        /// </summary>
        /// <returns>The Taxi object.</returns>
        public Taxi Build()
        {
            return _taxi;
        }
    }
}
