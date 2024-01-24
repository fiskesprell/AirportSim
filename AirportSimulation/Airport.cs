using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Airport
    {
        // Instance Variables
        private string AirportName { get; set; }
            

        // Constructor
        public Airport(string AirportName)
        {
            this.AirportName = AirportName;
        }

        // Methods
        public void AddTerminal()
        {
            // Ikke implementert enda
        }

        public void AddRunway()
        {
            // Ikke implementert enda
        }

        public void AddGate()
        {
            // Ikke implementert enda
        }

    }
}
