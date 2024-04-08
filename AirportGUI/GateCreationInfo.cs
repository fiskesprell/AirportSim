using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetzachTech.AirportSim.Infrastructure;

namespace AirportGUI
{
    public class GateCreationInfo
    {
        public string GateName { get; set; }
        public Terminal Terminal { get; set; }

        public GateCreationInfo(string gateName, Terminal terminal)
        {
            GateName = gateName;
            Terminal = terminal;
        }
    }
}
