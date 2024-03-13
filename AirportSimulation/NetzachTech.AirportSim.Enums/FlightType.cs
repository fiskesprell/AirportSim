using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl.NetzachTech.AirportSim.Enums
{
    [Flags]
    public enum FlightType
    {
        /// <summary>
        /// Defines the types of flights that can be scheduled within the airport simulation.
        /// </summary>
        /// <value>
        /// Commercial - Represents a commercial flight.
        /// Transport - Represents a transport flight.
        /// Personal - Represents a personal flight.
        /// Military - Represents a military flight.
        /// </value>
        Commercial = 1,
        Transport = 2,
        Personal = 4,
        Military = 8,
    }
}
