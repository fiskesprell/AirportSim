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
        private string AirportName;

        // Constructor
        public Airport(string AirportName)
        {
            this.AirportName = AirportName;
        }
    }
}
