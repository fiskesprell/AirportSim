using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    public class Terminal
    {
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

        /// <summary>
        /// Creates agte object and adds it to this terminal
        /// </summary>
        /// <param name=""></param>
        public Gate AddGate(string name)
        {
            Gate newGate = new Gate(name);
            ConnectedGates.Add(newGate);
            Console.WriteLine("Terminalen " + TerminalName + " har fått tildelt gate " + newGate.GetGateName());
            return newGate;
        }

        public List<Gate> GetConnectedGates()
        {
            return ConnectedGates;
        }

        public bool GetIsInternational() 
        { 
            return IsInternational; 
        }

        public void SetIsInternational(bool isInternational)
        {
            IsInternational = isInternational;
        }
    }
}
