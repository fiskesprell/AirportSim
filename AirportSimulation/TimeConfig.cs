using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    /// <summary>
    /// Class used to define how long it takes to drive from Terminal x to Runway x.
    /// </summary>
    public class TimeConfig
    {
        /// <summary>
        /// How many minutes it takes fro a plane to drive from the given runway to the given terminal
        /// </summary>
        private int _minutes;
        public int Minutes
        { get => _minutes; set => _minutes = value;}

        /// <summary>
        /// The chosen terminalobject for the timeconfiguration
        /// </summary>
        private Terminal _terminal;
        public Terminal Terminal
        { get => _terminal; set => _terminal = value; }

        /// <summary>
        /// The chosen runway for the timeconfiguration
        /// </summary>
        private Runway _runway;
        public Runway Runway
        { get => _runway; set => _runway = value; }

        /// <summary>
        /// 'Makes a timeconfig object that tells the program how long it takes to drive from the terminal to the runway
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="runway"></param>
        /// <param name="minutes"></param>
        public TimeConfig(Terminal terminal, Runway runway, int minutes) 
        {
            this.Terminal = terminal;
            this.Runway = runway;
            this.Minutes = minutes;
        }


    }
}
