using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public class Terminal
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
        private List<Gate> ConnectedGates = new List<Gate>();



        // Constructor
        public Terminal(string TerminalName)
        {
            this.TerminalName = TerminalName;
        }

        //Legge til en gate i terminalen. Lurer på om vi skal kalle på Gate konstruktøren i denne 
        //metoden slik at folk ikke trenger å lage et gate objekt for deretter å legge til?

        /// <summary>
        /// Creates agte object and adds it to this terminal
        /// </summary>
        /// <param name=""></param>
        public void addGate(string name)
        {
            Gate newGate = new Gate(name);
            ConnectedGates.Add(newGate);
            Console.WriteLine("Terminalen " + TerminalName + " Har fått tildelt gate " + newGate.getGateName());
        }

        public List<Gate> getConnectedGates()
        {
            return ConnectedGates;
        }

        public bool getIsInternational() 
        { 
            return IsInternational; 
        }

        // Legge til noe for sikkerhetsjekk, spesielt hvis det er utland?


        // Passjekk for utland?
    }
}
