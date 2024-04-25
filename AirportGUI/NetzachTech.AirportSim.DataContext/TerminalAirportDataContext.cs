﻿using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportGUI.NetzachTech.AirportSim.DataContext
{
    public class TerminalAirportDataContext
    {
        public Airport Airport { get; set; }
        public Terminal Terminal { get; set; }
    }
}
