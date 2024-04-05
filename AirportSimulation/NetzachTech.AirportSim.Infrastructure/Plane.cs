using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportSimulation;
using NetzachTech.AirportSim.FlightOperations;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace NetzachTech.AirportSim.Infrastructure
{
    public class Plane
    {
        /// <summary>
        /// the name of the plane.
        /// </summary>
        private string _planeName;
        public string PlaneName
        { get => _planeName; set => _planeName = value; }

        /// <summary>
        /// The model of the plane.
        /// </summary>
        private string _planeModel;
        public string PlaneModel
        { get => _planeModel; set => _planeModel = value; }

        /// <summary>
        /// Tailnumber representing the plane.
        /// </summary>
        private string _tailNumber;
        public string TailNumber
        { get => _tailNumber; set => _tailNumber = value; }

        /// <summary>
        /// Classification for what type of operations this plane is used for.
        /// </summary>
        private FlightType _flightType = FlightType.Commercial;
        public FlightType FlightType
        { get => _flightType; set => _flightType = value; }

        /// <summary>
        /// Classification for how wide the wingspan is.
        /// </summary>
        private PlaneSizeClassification _planeSizeClassification = PlaneSizeClassification.C;
        public PlaneSizeClassification PlaneSizeClassification
        { get => _planeSizeClassification; set => _planeSizeClassification = value; }

        /// <summary>
        /// Represents how long the plane is from cockpit to tail in meters.
        /// </summary>
        private int _planeLength;
        public int PlaneLength
        { get => _planeLength; set => _planeLength = value; }

        /// <summary>
        /// Represents how wide the body of the plane is in meters.
        /// </summary>
        private int _planeBodyWidth;
        public int PlaneBodyWidth
        { get => _planeBodyWidth; set => _planeBodyWidth = value; }

        /// <summary>
        /// Represents how wide the wingspan is in meters.
        /// </summary>
        private int _planeWingspan;
        public int PlaneWingspan
        { get => _planeWingspan; set => _planeWingspan = value; }

        /// <summary>
        /// Represents the top velocity of the plane when cruising in the air.
        /// </summary>
        private int _planeTopVelocity;
        public int PlaneTopVelocity
        { get => _planeTopVelocity; set => _planeTopVelocity = value; }

        /// <summary>
        /// Represents how much the plane weighs without cargo, fuel, and passangers
        /// </summary>
        private int _planeWeight;
        public int PlaneWeight
        { get => _planeWeight; set => _planeWeight = value; }

        /// <summary>
        /// Represents whether or not the plane is currently active.
        /// </summary>
        private bool _planeIsInUse;
        public bool PlaneIsInUse
        { get => _planeIsInUse; set => _planeIsInUse = value; }

        /// <summary>
        /// Represent which flight the plane currently is assigned to.
        /// </summary>
        private Flight _currentFlight;
        public Flight CurrentFlight
        { get => _currentFlight; set => _currentFlight = value; }

        /// <summary>
        /// Represents whether the plane is currently occupied or not
        /// </summary>
        private bool _planeIsAvailable = true;
        public bool PlaneIsAvailable
        { get => _planeIsAvailable; set => _planeIsAvailable = value; }

        /// <summary>
        /// Represents which date the plane lasat had its maintanance.
        /// </summary>
        private DateTime _planeLastMaintanance;
        public DateTime PlaneLastMaintanace
        { get => _planeLastMaintanance; set => _planeLastMaintanance = value; }

        /// <summary>
        /// Represents at what date the next maintanance is planned.
        /// </summary>
        private DateTime _scheduledMaintanance;
        public DateTime ScheduledMaintananace
        { get => _scheduledMaintanance; set => _scheduledMaintanance = value; }

        /// <summary>
        /// Represent which airport the plane currently is operating out of.
        /// </summary>
        private Airport _currentAirport;
        public Airport CurrentAirport
        { get => _currentAirport; set => _currentAirport = value; }

        /// <summary>
        /// Represents how many passangers the plane maximum can have on it.
        /// </summary>
        private int _planePassengerCapacity;
        public int PlanePassengerCapacity
        { get => _planePassengerCapacity; set => _planePassengerCapacity = value; }

        /// <summary>
        /// Represents how much cargo the plane can hold in kg.
        /// </summary>
        private int _planeCargoCapacity;
        public int PlaneCargoCapacity
        { get => _planeCargoCapacity; set => _planeCargoCapacity = value; }

        /// <summary>
        /// Log history for this plane. Includes all the flights this plane has been used for.
        /// </summary>
        private List<Flight> _planeHistory = new List<Flight>();
        public List<Flight> PlaneHistory
        { get => _planeHistory; }


        public Plane() { }

        /// <summary>
        /// Lands the plane at the airport.
        /// </summary>
        public void LandPlane(Flight flight) 
        {
            flight.AssignedRunway.FlightOnRunway = flight;
            
            flight.AssignedTaxi.AddToTaxiQueue(flight);
            flight.AssignedRunway.FlightOnRunway = null;
            flight.AssignedRunway.IsAvailable = true;

            if (flight.Logging)
            {
                string logMessage = $"Flight {flight.Number} landed on runway {flight.AssignedRunway.RunwayName} at Day:{flight.ElapsedDays + 1}, Time:{flight.ElapsedHours.ToString("D2")}:{flight.ElapsedMinutes.ToString("D2")}";
                flight.LogHistory.Add(logMessage);
            }

            flight.SetFlightStatus(FlightStatus.Arrived);
            this.CurrentAirport = flight.CurrentAirport;
        }


        /// <summary>
        /// Takes off the plane from the runway.
        /// </summary>
        public void TakeoffPlane(Flight flight) 
        {
            flight.AssignedRunway.FlightOnRunway = flight;
            

            if (flight.Logging)
            {
                string logMessage = $"Flight {flight.Number} took off at Day: {flight.ElapsedDays + 1}, Time: {flight.ElapsedHours.ToString("D2")}:{flight.ElapsedMinutes.ToString("D2")}.";
                flight.LogHistory.Add(logMessage);
            }

            flight.SetFlightStatus(FlightStatus.Departed);
            this.CurrentAirport = flight.DestinationAirport;
            flight.AssignedRunway.FlightOnRunway = null;
            flight.AssignedRunway.IsAvailable = true;

        }

        /// <summary>
        /// Parks the plane at the assigned gate
        /// </summary>
        public void ParkPlaneAtGate(Flight flight)
        {
            Gate gateToPark = flight.AssignedGate;

            if (gateToPark == null)
            {
                gateToPark = flight.FindAvailableGate();

            }

            flight.IsParked = true;
            flight.IsTraveling = false;
            gateToPark.CurrentHolder = flight;
        }


        /// <summary>
        /// Updates the time the next scheduled maintanance is due
        /// </summary>
        public void SchedulePlaneMaintanance() 
        {
            DateTime newMaintanance = this.PlaneLastMaintanace.AddDays(5);
            this.ScheduledMaintananace = newMaintanance;
        }

        /// <summary>
        /// Performs a maintanance on the plane and automatically schedules the next maintanance
        /// </summary>
        public void PerformPlaneMaintanace() 
        {
            this.PlaneIsAvailable = false;
            this.PlaneIsInUse = false;
            this.PlaneLastMaintanace = this.CurrentAirport.ScheduledStartDate.AddDays(this.CurrentFlight.ElapsedDays);
            this.SchedulePlaneMaintanance();
        }

        /// <summary>
        /// Adds the plane to the list of available planes to the given airport
        /// </summary>
        /// <param name="airport"></param>
        public void AssignPlaneToAirport(Airport airport) 
        {
            _currentAirport = airport;
            airport.AddPlaneToListOfAvailablePlanes(this);
        }

        /// <summary>
        /// Makes the plane unavailable and takes it out of use
        /// </summary>
        public void TakePlaneOutOfUse() 
        {
            this.PlaneIsAvailable = false;
            this.PlaneIsInUse = false;
        }

        /// <summary>
        /// Puts the plane back in use and sets it to available
        /// </summary>
        public void SetPlaneInUse() 
        {
            this.PlaneIsInUse = true;
            this.PlaneIsAvailable = true;
        }

        /// <summary>
        /// Adds the current flight into the log history and nulls Currentflight and PlaneIsAvailable
        /// </summary>
        public void CompleteFlight(Airport airport) 
        {
            if (this.CurrentFlight != null)
            {
                this.PlaneHistory.Add(this.CurrentFlight);
            }
            this.CurrentAirport = airport;
            this.CurrentFlight = null;
            this.PlaneIsAvailable = true;
        }

        /// <summary>
        /// Prints out each element in this planes log history
        /// </summary>
        public void PrintPlaneHistory() 
        {
            foreach (var log in PlaneHistory)
                Console.WriteLine(log);
        }




    }
}
