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
    /// Manages the collection of TimeConfig objects to determine the travel times between terminals and runways..
    /// </summary>
    public class TimeConfigManager

    {
        /// <summary>
        /// Represents the default travel time in minutes from any terminal to any runway when no specific TimeConfig object exists.
        /// </summary>
        private int defaultMinutes = 15;

        /// <summary>
        /// List of all created TimeConfig objects.
        /// </summary>
        private List<TimeConfig> _timeConfigs = new List<TimeConfig>();

        /// <summary>
        /// Provides access to the list of TimeConfig objects.
        /// </summary>
        public List<TimeConfig> TimeConfigs
        { get => _timeConfigs; }

        /// <summary>
        /// Adds a new TimeConfig object to the collection.
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
        /// Retrieves the travel time for a specific flight based on its assigned terminal and runway. Returns default time if no specific TimeConfig is found.
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
