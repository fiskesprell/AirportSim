using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class TimeConfig
    {
        private int _minutes;
        public int Minutes
        { get => _minutes; set => _minutes = value;}

        private Terminal _terminal;
        public Terminal Terminal
        { get => _terminal; set => _terminal = value; }

        private Runway _runway;
        public Runway Runway
        { get => _runway; set => _runway = value; }

        public TimeConfig(Terminal terminal, Runway runway, int minutes) 
        {
            this.Terminal = terminal;
            this.Runway = runway;
            this.Minutes = minutes;
        }


    }
}
