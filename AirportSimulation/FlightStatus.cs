using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulationCl
{
    public enum FlightStatus
    {
        /// <summary>
        /// Defines the current status of a flight.
        /// </summary>
        /// <value>
        /// OnTime - Flight is on time.
        /// ArrivingDelayed - Flight arrival is delayed.
        /// DepartingDelayed - Flight departure is delayed.
        /// Boarding - Passengers are currently boarding.
        /// Departing - Flight is in the process of departing.
        /// Departed - Flight has left the airport.
        /// Arrived - Bør denne byttes med inTaxi?.
        /// Landed - Flight has landed and is on the runway.
        /// Completed - Flight is completed.
        /// OnWayToGate - Flight is en route to its assigned gate.
        /// Offloading - Passengers and cargo are being offloaded.
        /// </value>
        OnTime,
        ArrivingDelayed,
        DepartingDelayed,
        Boarding,
        Departing,
        Departed,
        Arrived,
        Landed,
        Completed,
        OnWayToGate,
        Offloading,
    }
}
