using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportGUI
{
    public class FlightCreationParameters
    {
        public string FlightNumber { get; set; }
        public string FlightDate { get; set; }
        public string FlightTime { get; set; }
        public string Destination { get; set; }
        public bool IsIncoming { get; set; }  // True means incoming, False means outgoing
    }
}
