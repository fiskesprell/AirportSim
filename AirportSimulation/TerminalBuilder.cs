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

        /// <summary>
        /// Adds a string for the terminal name
        /// </summary>
        /// <param name="terminalName"></param>
        /// <returns></returns>
        public TerminalBuilder AddTerminalName(string terminalName)
        {
            _terminal.TerminalName = terminalName;
            return this;
        }

        /// <summary>
        /// Adds a boolean to represents if this terminal accepts international flights
        /// </summary>
        /// <param name="isInternational"></param>
        /// <returns></returns>
        public TerminalBuilder SetIsInternational(bool isInternational)
        {
            _terminal.IsInternational = isInternational;
            return this;
        }

        /// <summary>
        /// Creates a gate object and adds it the the list of gates in this terminal
        /// </summary>
        /// <param name="gateName"></param>
        /// <param name="gateLicence"></param>
        /// <returns></returns>
        public TerminalBuilder CreateAndAddNewGate(string gateName, GateLicence gateLicence)
        {
            Gate gate = new GateBuilder()
                            .AddGateName(gateName)
                            .AddGateLicence(gateLicence)
                            .Build();
            _terminal.ConnectedGates.Add(gate);
            return this;
        }

        /// <summary>
        /// Adds an existing gate to the list of gates in this terminal
        /// </summary>
        /// <param name="gate"></param>
        /// <returns></returns>
        public TerminalBuilder AddGateToTerminal(Gate gate)
        {
            _terminal.ConnectedGates.Add(gate);
            gate.Terminal = _terminal;
            return this;
        }

        /// <summary>
        /// returns the finished terminal object
        /// </summary>
        /// <returns></returns>
        public Terminal Build()
        {
            return _terminal;
        }
    }
}
