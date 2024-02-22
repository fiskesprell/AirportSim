using AirportSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public class TimeSimulationBuilder
    {
        private readonly TimeSimulation _timeSimulation = new TimeSimulation();

        /// <summary>
        /// Sets a startdate for the simulation
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        public TimeSimulationBuilder setScheduledStartDate(DateTime startDate)
        {
            _timeSimulation.StartDate = startDate;
            return this;
        }

        /// <summary>
        /// Sets a enddate for the simulation
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public TimeSimulationBuilder setScheduledEndDate(DateTime endDate)
        {
            _timeSimulation.EndDate = endDate;
            return this;
        }

        public TimeSimulation Build() 
        { 
            return _timeSimulation; 
        }
    }
}
