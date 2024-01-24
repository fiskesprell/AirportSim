using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Terminal
    {
        // Instance Variables
        public string TerminalName { get; set; }
        public bool IsInternational { get; set; } = false;

        // Constructor
        public Terminal(string TerminalName)
        {
            this.TerminalName = TerminalName;
        }
    }
}
