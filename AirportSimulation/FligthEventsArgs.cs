using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class FlightEventArgs : EventArgs
    {
        private string _name;
        public string Name
        { get => _name; }

        private Plane _plane;
        public Plane Plane
        { get =>  _plane; }

        private List<string> _flightLog;
        public List<string> FlightLog
        { get => _flightLog; }

        public FlightEventArgs(string name, Plane plane, List<string> flightLog)
        {
            _name = name;
            _plane = plane;
            _flightLog = flightLog;
        }
    }
}
