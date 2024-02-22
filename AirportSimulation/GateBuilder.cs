using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class GateBuilder
    {
        private readonly Gate _gate = new Gate();

        public GateBuilder AddGateName(string name)
        {
            _gate.GateName = name;
            return this;
        }
        public GateBuilder AddGateLicence(GateLicence gateLicence)
        {
            _gate.Licence |= gateLicence;
            return this;
        }



        public Gate Build() 
        {
            return _gate;
        }
    }
}
