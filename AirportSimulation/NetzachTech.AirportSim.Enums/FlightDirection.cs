using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl.NetzachTech.AirportSim.Enums
{
    public enum FlightDirection
    {
        /// <summary>
        /// Defines the direction of the flight as incoming, outgoing or other.
        /// </summary>
        /// <value>
        /// Incoming - Flight arriving at the airport.
        /// Outgoing - Flight departing from the airport.
        //∕ Other - Flight heading elsewhere, i.e. maintenance, storage hangar.
        ///</value>
        Incoming,
        Outgoing,
        Other
    }
}
