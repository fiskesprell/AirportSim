using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportGUI.NetzachTech.AirportSim.DataContext
{
    public class RunwayAirportDataContext
    {
        public Airport Airport { get; set; }
        public Runway Runway { get; set; }
    }
}
