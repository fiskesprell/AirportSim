using AirportSimulation;
using NetzachTech.AirportSim.FlightOperations;
using NetzachTech.AirportSim.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.Builders
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
        /// Sets an enddate for the simulation
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public TimeSimulationBuilder setScheduledEndDate(DateTime endDate)
        {
            _timeSimulation.EndDate = endDate;
            return this;
        }

        /// <summary>
        /// Returns a TimeSimulation object with the given configuration of the TimeSimulationBuilder object
        /// </summary>
        /// <returns></returns>
        public TimeSimulation Build() 
        { 
            return _timeSimulation; 
        }
    }
}
