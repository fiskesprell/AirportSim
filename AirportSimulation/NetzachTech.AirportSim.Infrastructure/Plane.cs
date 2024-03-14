using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportSimulation;
using NetzachTech.AirportSim.FlightOperations;


namespace NetzachTech.AirportSim.Infrastructure
{
    public class Plane
    {
        //Beskrivelse
        private string _planeName;
        public string PlaneName
        { get => _planeName; set => _planeName = value; }

        private string _planeModel;
        public string PlaneModel
        { get => _planeModel; set => _planeModel = value; }

        private string _tailNumber;
        public string TailNumber
        { get => _tailNumber; set => _tailNumber = value; }

        private FlightType _flightType;
        public FlightType FlightType
        { get => _flightType; set => _flightType = value; }

        //Funksjonalitet
        private int _planeLength;
        public int PlaneLength
        { get => _planeLength; set => _planeLength = value; }

        private int _planeBodyWidth;
        public int PlaneBodyWidth
        { get => _planeBodyWidth; set => _planeBodyWidth = value; }

        private int _planeWingspan;
        public int PlaneWingspan
        { get => _planeWingspan; set => _planeWingspan = value; }

        private int _planeTopVelocity;
        public int PlaneTopVelocity
        { get => _planeTopVelocity; set => _planeTopVelocity = value; }

        private int _planeWeight;
        public int PlaneWeight
        { get => _planeWeight; set => _planeWeight = value; }

        private double _planeFuelBurnRate;
        public double PlaneFuelBurnRate
        { get => _planeFuelBurnRate; set => _planeFuelBurnRate = value; }

        //Tilgjengelighet
        private bool _planeIsInUse;
        public bool PlaneIsInUse
        { get => _planeIsInUse; set => _planeIsInUse = value; }

        private bool _planeIsAvailable;
        public bool PlaneIsAvailable
        { get => _planeIsAvailable; set => _planeIsAvailable = value; }

        private DateTime _planeLastMaintanance;
        public DateTime PlaneLastMaintanace
        { get => _planeLastMaintanance; set => _planeLastMaintanance = value; }

        private DateTime _scheduledMaintanance;
        public DateTime ScheduledMaintananace
        { get => _scheduledMaintanance; set => _scheduledMaintanance = value; }

        private Airport _currentAirport;
        public Airport CurrentAirport
        { get => _currentAirport; set => _currentAirport = value; }

        //Kapasitet
        private int _planePassengerCapacity;
        public int PlanePassengerCapacity
        { get => _planePassengerCapacity; set => _planePassengerCapacity = value; }

        private int _planeCargoCapacity;
        public int PlaneCargoCapacity
        { get => _planeCargoCapacity; set => _planeCargoCapacity = value; }


        private int _planeFuelCapacity;
        public int PlaneFuelCapacity
        { get => _planeFuelCapacity; set => _planeFuelCapacity = value; }

        //Historikk
        private List<Flight> _planeHistory = new List<Flight>();
        public List<Flight> PlaneHistory
        { get => _planeHistory; }


        public Plane() { }

        private void LandPlane() { }

        private void TakeoffPlane() { }

        private void ParkPlaneAtGate() { }

        private void SchedulePlaneMaintanance() { }

        private void PerformPlaneMaintanace() { }

        private void AssignPlaneToAirport() { }

        private void TakePlaneOutOfUse() { }

        private void SetPlaneInUse() { }

        private void PrintPlaneHistory() { }




    }
}
