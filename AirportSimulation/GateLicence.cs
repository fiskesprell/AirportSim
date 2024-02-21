using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    [Flags]
    public enum GateLicence
    {
        /// <summary>
        /// Defines the types of aircraft licenses a gate can have, allowing for multiple categories simultaneously.
        /// </summary>
        /// <value>
        /// None, 0
        /// Commercial, 1 - Commercial flights allowed.
        /// Transport, 2 - Transport flights allowed.
        /// Personal, 4 - Personal flights allowed.
        /// Military 8 - Military flights allowed.
        /// </value>
        /// <remarks>Flags to allow multiple licences</remarks>
        None = 0,
        Commercial = 1,
        Transport = 2,
        Personal = 4,
        Military = 8
    }
}
