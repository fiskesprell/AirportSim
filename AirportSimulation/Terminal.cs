using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Terminal
    {
        // Instance variables
        private String TerminalName;
        private bool IsInternational = false;

        // Constructor
        public Terminal(string TerminalName, bool IsInternational)
        {
            this.TerminalName = TerminalName;
            this.IsInternational = IsInternational;
        }

    }
}
