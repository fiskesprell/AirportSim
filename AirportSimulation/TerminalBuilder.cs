using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class TerminalBuilder
    {
        private readonly Terminal _terminal = new Terminal();

        public TerminalBuilder AddTerminalName(string terminalName)
        {
            _terminal.TerminalName = terminalName;
            return this;
        }

        public TerminalBuilder SetIsInternational(bool isInternational)
        {
            _terminal.IsInternational = isInternational;
            return this;
        }

        public TerminalBuilder CreateAndAddNewGate(string gateName, GateLicence gateLicence)
        {
            Gate gate = new GateBuilder()
                            .AddGateName(gateName)
                            .AddGateLicence(gateLicence)
                            .Build();
            _terminal.ConnectedGates.Add(gate);
            return this;
        }

        public TerminalBuilder AddGateToTerminal(Gate gate)
        {
            _terminal.ConnectedGates.Add(gate);
            return this;
        }

        public Terminal Build()
        {
            return _terminal;
        }
    }
}
