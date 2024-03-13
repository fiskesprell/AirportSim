using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class TimeConfigManager
        
    {
        private int defaultMinutes = 29;
        private int defaultSeconds = 0;
        private List<TimeConfig> _timeConfigs = new List<TimeConfig>();
        public List<TimeConfig> TimeConfigs 
        { get => _timeConfigs; }

        public void AddTimeConfig(Terminal terminal, Runway runway, int minutes)
        {
            var timeConfig = new TimeConfig(terminal, runway, minutes);
            _timeConfigs.Add(timeConfig);
        }

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
