using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetzachTech.AirportSim.Infrastructure;

namespace NetzachTech.AirportSim.Time
{
    /// <summary>
    /// Class used to hold and loop through all the TimeConfig objects.
    /// </summary>
    public class TimeConfigManager

    {
        /// <summary>
        /// Default drivingtime from any given terminal to any given runway. If an timeconfigobject is not created, this wil be the time used.
        /// </summary>
        private int defaultMinutes = 15;

        /// <summary>
        /// A list containing all the created timeconfig objects
        /// </summary>
        private List<TimeConfig> _timeConfigs = new List<TimeConfig>();
        public List<TimeConfig> TimeConfigs
        { get => _timeConfigs; }

        /// <summary>
        /// Creates a timeconfig object and adds it to the list of all timeconfig objects.
        /// </summary>
        /// <param name="terminal"></param>
        /// <param name="runway"></param>
        /// <param name="minutes"></param>
        public void AddTimeConfig(Terminal terminal, Runway runway, int minutes)
        {
            var timeConfig = new TimeConfig(terminal, runway, minutes);
            _timeConfigs.Add(timeConfig);
        }


        /// <summary>
        /// This method looks at the flight given as argument and checks the list of all timeconfigobjects and sees if it has been created a timeconffigobject for the terminal and runway that has been assigned to this flight. 
        /// If it find an object, it will return the time it takes to drive, otherwise it returns the default value
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public int GetTravelTime(Flight flight)
        {
            Terminal terminal = flight.AssignedGate.Terminal;
            Runway runway = flight.AssignedRunway;
            var config = _timeConfigs.FirstOrDefault(tc => tc.Terminal == terminal && tc.Runway == runway);
            if (config != null)
            {
                return config.Minutes;
            }
            return defaultMinutes;
        }
    }
}
