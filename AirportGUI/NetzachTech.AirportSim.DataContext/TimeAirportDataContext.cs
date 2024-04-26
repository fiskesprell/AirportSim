using AirportSimulation;
using NetzachTech.AirportSim.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportGUI.NetzachTech.AirportSim.DataContext
{
    public class TimeAirportDataContext
    {
        public Airport Airport { get; set; }
        public TimeSimulation TimeSimulation { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
