using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl.NetzachTech.AirportSim.EventArguments
{
    public class TimeUpdatedEventArgs : EventArgs
    {
        public int ElapsedDays {  get; set; }
        public int ElapsedHours { get; set; }
        public int ElapsedMinutes { get; set; }

        public TimeUpdatedEventArgs(int elapsedDays, int elapsedHours, int elapsedMinutes)
        {
            ElapsedDays = elapsedDays;
            ElapsedHours = elapsedHours;
            ElapsedMinutes = elapsedMinutes;
        }
    }
}
