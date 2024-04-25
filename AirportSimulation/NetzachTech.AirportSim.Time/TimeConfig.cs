using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetzachTech.AirportSim.Infrastructure;

namespace NetzachTech.AirportSim.Time
{
    /// <summary>
    /// Represents the duration required for an airplane to travel from a specific terminal to a runway..
    /// </summary>
    public class TimeConfig
    {
        /// <summary>
        /// Gets or sets the number of minutes it takes for a plane to travel from the specified runway to the specified terminal.
        /// </summary>
        private int _minutes;
        public int Minutes
        { get => _minutes; set => _minutes = value; }

        /// <summary>
        /// Gets or sets the terminal associated with the time configuration.
        /// </summary>
        private Terminal _terminal;
        public Terminal Terminal
        { get => _terminal; set => _terminal = value; }

        /// <summary>
        /// Gets or sets the runway associated with the time configuration.
        /// </summary>
        private Runway _runway;
        public Runway Runway
        { get => _runway; set => _runway = value; }

        /// <summary>
        /// Initializes a new instance of the TimeConfig class to define the time taken to travel from a terminal to a runway.
        /// </summary>
        /// <param name="terminal">The terminal from which the duration starts.</param>
        /// <param name="runway">The runway at which the duration ends.</param>
        /// <param name="minutes">The number of minutes it takes to complete the travel.</param>
        public TimeConfig(Terminal terminal, Runway runway, int minutes)
        {
            Terminal = terminal;
            Runway = runway;
            Minutes = minutes;
        }


    }
}
