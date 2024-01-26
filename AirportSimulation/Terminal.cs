using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Terminal
    {
        // Instance Variables
        /// <summary>
        /// The name of your Terminal.
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
        private List<Gate> connectedGates = new List<Gate>();



        // Constructor
        public Terminal(string TerminalName)
        {
            this.TerminalName = TerminalName;
        }

        //Legge til en gate i terminalen. Lurer på om vi skal kalle på Gate konstruktøren i denne 
        //metoden slik at folk ikke trenger å lage et gate objekt for deretter å legge til?
        public void addGate(Gate gate)
        {
            connectedGates.Add(gate);
        }

        // Legge til noe for sikkerhetsjekk, spesielt hvis det er utland?


        // Passjekk for utland?
    }
}
