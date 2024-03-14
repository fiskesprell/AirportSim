using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.Infrastructure
{
    /// <summary>
    /// Represents plane size classifications based on wingspan and common planes.
    /// </summary>
    [Flags]
    public enum PlaneSizeClassification
    {
        /// <summary>
        /// Category A: Wingspan < 15 meters. Example: Cessna 172.
        /// </summary>
        A,

        /// <summary>
        /// Category B: Wingspan 15-24 meters. Example: Bombardier Q400.
        /// </summary>
        B,

        /// <summary>
        /// Category C: Wingspan 24-36 meters. Examples: Boeing 737, Airbus A320.
        /// </summary>
        C,

        /// <summary>
        /// Category D: Wingspan 36-52 meters. Examples: Boeing 767, Airbus A330.
        /// </summary>
        D,

        /// <summary>
        /// Category E: Wingspan 52-65 meters. Examples: Boeing 777, Airbus A340.
        /// </summary>
        E,

        /// <summary>
        /// Category F: Wingspan 65-80 meters. Examples: Airbus A380, Antonov An-124 Ruslan.
        /// </summary>
        F
    }
}
