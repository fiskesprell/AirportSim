using NetzachTech.AirportSim.FlightOperations;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.Builders
{
    public class GateBuilder
    {
        private readonly Gate _gate = new Gate();

        /// <summary>
        /// Adds a string for the gate name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GateBuilder AddGateName(string name)
        {
            _gate.GateName = name;
            return this;
        }

        /// <summary>
        /// Adds a licence to this gate
        /// </summary>
        /// <param name="gateLicence"></param>
        /// <returns></returns>
        public GateBuilder AddGateLicence(GateLicence gateLicence)
        {
            _gate.Licence |= gateLicence;
            return this;
        }


        /// <summary>
        /// Returns the finished gate object 
        /// </summary>
        /// <returns></returns>
        public Gate Build() 
        {
            return _gate;
        }
    }
}
