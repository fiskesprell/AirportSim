using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl.NetzachTech.AirportSim.Infrastructure
{

    /// <summary>
    /// Represents an airport terminal, distinguishing between international and domestic flights and managing connected gates.
    /// </summary>
    public class Terminal
    {
        /// <summary>
        /// The name of the terminal.
        /// </summary>
        private string _terminalName;
        public string TerminalName
        { get => _terminalName; set => _terminalName = value; }

        /// <summary>
        /// Whether the Terminal is used by international flights or not. <br/>
        /// True = Used by international flights <br/>
        /// False = Used by domestic flights <br/>
        /// Default value is false.
        /// </summary>
        private bool _isInternational = false;
        public bool IsInternational
        { get => _isInternational; set => _isInternational = value; }

        /// <summary>
        /// List of the gates connected to this terminal.
        /// </summary>
        private List<Gate> _connectedGates = new List<Gate>();
        public List<Gate> ConnectedGates
        { get => _connectedGates; }

        public Terminal() { }

        /// <summary>
        /// Initializes a new instance of the Terminal class.
        /// </summary>
        /// <param name="TerminalName">The name for the terminal.</param>
        public Terminal(string terminalName)
        {
            TerminalName = terminalName;
        }

        /// <summary>
        /// Creates gate object and adds it to this terminal
        /// </summary>
        /// <param name=""></param>
        public Gate AddNewGate(string name)
        {
            Gate newGate = new Gate(name);
            ConnectedGates.Add(newGate);
            Console.WriteLine("Terminalen " + TerminalName + " har fått tildelt gate " + newGate.GateName);
            return newGate;
        }

        /// <summary>
        /// Adds a gateobject to the list of gates for this terminal
        /// </summary>
        /// <param name="gate"></param>
        public void AddExistingGate(Gate gate)
        {
            ConnectedGates.Add(gate);
        }

        /// <summary>
        /// Prints out the relevant information about this terminal. This includes the terminal’s name, 
        /// whether it is international or not, and the terminal’s connected gates.
        /// </summary>
        public void PrintTerminalInfo()
        {
            Console.WriteLine("Terminalname: " + TerminalName);
            Console.WriteLine("International: " + IsInternational);
            Console.WriteLine("Gates: ");

            foreach (Gate gate in ConnectedGates)
            {
                Console.WriteLine("Gatename: " + gate.GateName);
            }

        }

        internal void AddExistingGate(Gate gate)
        {
            throw new NotImplementedException();
        }
    }
}
