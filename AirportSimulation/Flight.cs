using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    enum FlightType
    {
        Commercial,
        Transport,
        Personal,
        Military
    }

    enum FlightStatus
    {
        OnTime,
        Delayed,
        Boarding,
        Departed,
        Arrived
    }

    internal class Flight
    {
        private string Number { get; set; }
        private string Company { get; set; } = "Norwegian";
        private FlightType FlightType { get; set; } = FlightType.Commercial;
        private Gate AssignedGate { get; set; }
        private bool IsInternational { get; set; } = false;
        private DateTime DepartureTime { get; set; }
        private DateTime ArrivalTime { get; set; }
        private string Destination { get; set; }
        private DateTime LastMaintanace { get; set; }
        private FlightStatus Status = FlightStatus.OnTime;

    }

    public Flight(string number, string destination, DateTime departure, DateTime arrival)
    {
        Number = number;
        Destination = destination;
        DepartureTime = departure;
        ArrivalTime = arrival;

    }

    public void takeoff()
    {
        //noe
    }

    public void Land()
    {
        //noe
    }

    public void ParkGate(Gate gate)
    {
        AssignedGate = gate;
        gate.IsAvailable.set(false);
        //Parkere ved gate og sette gate til unavailable
    }


    public void performMaintanance()
    {
        //noe
    }

    public void changeStatus(FlightStatus status)
    {
        status = status;
    }




}
