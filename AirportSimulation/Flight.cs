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

    enum Frequency
    {
        OneTime,
        Daily,
        Weekly
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
        private Frequency Frequency { get; set; } = Frequency.OneTime;

    }

    public Flight(string Number, string Destination, DateTime DepartureTime, DateTime Arrival)
    {
        this.Number = Number;
        this.Destination = Destination;
        this.DepartureTime = DepartureTime;
        this.ArrivalTime = Arrival;

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
        Status = status;
    }

    public void 




}
