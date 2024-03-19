using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.Infrastructure
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
        private bool _readyForInternational = false;
        public bool ReadyForInternational
        { get => _readyForInternational; set => _readyForInternational = value; }

        /// <summary>
        /// List of the gates connected to this terminal.
        /// </summary>
        private List<Gate> _connectedGates = new List<Gate>();
        public List<Gate> ConnectedGates
        { get => _connectedGates; }

        private Airport _airport;
        public Airport Airport
        { get => _airport; set => _airport = value;}

        private bool _strictlyInternational = false;
        public bool StrictlyInternational
        { get => _strictlyInternational; set => _strictlyInternational= value; }

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
            newGate.Terminal = this;
            return newGate;
        }

        /// <summary>
        /// Adds a gateobject to the list of gates for this terminal
        /// </summary>
        /// <param name="gate"></param>
        public void AddExistingGate(Gate gate)
        {
            ConnectedGates.Add(gate);
            gate.Terminal = this;
        }

        /// <summary>
        /// Prints out the relevant information about this terminal. This includes the terminal’s name, 
        /// whether it is international or not, and the terminal’s connected gates.
        /// </summary>
        public void PrintTerminalInfo()
        {
            Console.WriteLine("Terminalname: " + TerminalName);
            Console.WriteLine("International: " + ReadyForInternational);
            Console.WriteLine("Gates: ");

            foreach (Gate gate in ConnectedGates)
            {
                Console.WriteLine("Gatename: " + gate.GateName);
            }

        }

        /// <summary>
        /// Creates a new Taxi and Gate. These need to be connected and are therefore put in the same method.
        /// The new Gate is connected to the Taxi, and the taxi is then added to AllTaxis.
        /// An alternative to using this method to create a new gate would be to use airport.GetAllTerminals() 
        /// to get a list of find all terminals, create a new Gate object, 
        /// then loop through the list of terminals to find one you want to add it to by using
        /// terminal.AddConnectedGate().
        /// </summary>
        /// <param name="gateName">The name of your gate</param>
        /// <param name="taxiName">The name of your taxi</param>
        public void AddNewConnectedGateAndTaxi(string gateName, string taxiName, TaxiwayType taxiwayType)
        {
            Taxi newTaxi = new Taxi(taxiName, taxiwayType);
            Gate newGate = new Gate(gateName);
            newTaxi.AddConnectedGate(newGate);
            this.Airport.AllTaxis.Add(newTaxi);
        }
    }
}
