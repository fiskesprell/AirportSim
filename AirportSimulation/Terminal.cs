using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{

    /// <summary>
    /// Represents an airport terminal, distinguishing between international and domestic flights and managing connected gates.
    /// </summary>
    public class Terminal
    {
        /// <summary>
        /// The name of the terminal.
        /// </summary>
        public string TerminalName { get; set; }

        /// <summary>
        /// Whether the Terminal is used by international flights or not. <br/>
        /// True = Used by international flights <br/>
        /// False = Used by domestic flights <br/>
        /// Default value is false.
        /// </summary>
        public bool IsInternational { get; set; } = false;

        /// <summary>
        /// List of the gates connected to this terminal.
        /// </summary>
        private List<Gate> ConnectedGates = new List<Gate>();

        /// <summary>
        /// Initializes a new instance of the Terminal class.
        /// </summary>
        /// <param name="TerminalName">The name for the terminal.</param>
        public Terminal(string TerminalName)
        {
            this.TerminalName = TerminalName;
        }

        /// <summary>
        /// Creates gate object and adds it to this terminal
        /// </summary>
        /// <param name=""></param>
        public Gate AddGate(string name)
        {
            Gate newGate = new Gate(name);
            ConnectedGates.Add(newGate);
            Console.WriteLine("Terminalen " + TerminalName + " har fått tildelt gate " + newGate.GetGateName());
            return newGate;
        }

        /// <summary>
        /// Gets the list of all gates connected to this terminal.
        /// </summary>
        /// <returns>List of connected gates.</returns>
        public List<Gate> GetConnectedGates()
        {
            return ConnectedGates;
        }

        /// <summary>
        /// Checks if the terminal is designated for international flights.
        /// </summary>
        /// <returns>True if the terminal is for international flights, otherwise false.</returns>
        public bool GetIsInternational() 
        { 
            return IsInternational; 
        }

        /// <summary>
        /// Sets the terminal's designation for international flights.
        /// </summary>
        /// <param name="isInternational">Boolean value indicating whether the terminal is for international flights.</param>
        public void SetIsInternational(bool isInternational)
        {
            IsInternational = isInternational;
        }
    }
}
