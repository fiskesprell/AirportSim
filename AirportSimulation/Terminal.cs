﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Terminal
    {
        // Instance Variables
        public string TerminalName { get; set; }
        public bool IsInternational { get; set; } = false;
        private List<Gate> connectedGates = new List<Gate>;

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
