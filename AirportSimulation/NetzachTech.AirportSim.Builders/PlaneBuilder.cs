using AirportSimulation;
using NetzachTech.AirportSim.FlightOperations;
using NetzachTech.AirportSim.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetzachTech.AirportSim.Builders
{
    public class PlaneBuilder
    {

        private readonly Plane _plane = new Plane();

        /// <summary>
        /// Adds a string as a name of the plane
        /// </summary>
        /// <param name="planeName"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneName(string planeName)
        {
            _plane.PlaneName = planeName;
            return this;
        }

        public PlaneBuilder AddFlightType(FlightType flightType)
        {
            _plane.FlightType = flightType;
            return this;
        }

        /// <summary>
        /// Adds a string as the modelname of the plane
        /// </summary>
        /// <param name="planeModel"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneModel(string planeModel)
        {
            _plane.PlaneModel = planeModel;
            return this;
        }

        /// <summary>
        /// Adds a string for the tailnumber of the plane
        /// </summary>
        /// <param name="tailNumber"></param>
        /// <returns></returns>
        public PlaneBuilder AddTailNumber(string tailNumber)
        {
            _plane.TailNumber = tailNumber;
            return this;
        }

        /// <summary>
        /// Adds an int for the length of the plane
        /// </summary>
        /// <param name="planeLength"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneLength(int planeLength)
        {
            _plane.PlaneLength = planeLength;
            return this;
        }

        /// <summary>
        /// Adds an int for the bodywidth of the plane
        /// </summary>
        /// <param name="planeBodyWidth"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneBodywidth(int planeBodyWidth)
        {
            _plane.PlaneBodyWidth = planeBodyWidth;
            return this;
        }

        /// <summary>
        /// Adds an int for the wingspan of the plane
        /// </summary>
        /// <param name="wingspan"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneWinspan(int wingspan)
        {
            _plane.PlaneWingspan = wingspan;
            return this;
        }

        /// <summary>
        /// Addsa an int for the top velocity for the plane
        /// </summary>
        /// <param name="topVelocity"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneTopVelocity(int topVelocity)
        {
            _plane.PlaneTopVelocity = topVelocity;
            return this;
        }

        /// <summary>
        /// Adds an int for the weight of the plane
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneWeight(int weight)
        {
            _plane.PlaneWeight = weight;
            return this;
        }

        /// <summary>
        /// Adds a double for the fuel burnrate of the plane
        /// </summary>
        /// <param name="fuelBurnRate"></param>
        /// <returns></returns>
        public PlaneBuilder AddFuelBurnRate(double fuelBurnRate)
        {
            _plane.PlaneFuelBurnRate = fuelBurnRate;
            return this;
        }

        /// <summary>
        /// Adds a boolean to represent if the plane is currently operational
        /// </summary>
        /// <param name="isInUse"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneIsInUse(bool isInUse)
        {
            _plane.PlaneIsInUse = isInUse;
            return this;
        }

        /// <summary>
        /// Adds a boolean to represent of the plane is currently occupied for a flight
        /// </summary>
        /// <param name="isAvailable"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneIsAvailable(bool isAvailable)
        {
            _plane.PlaneIsAvailable = isAvailable;
            return this;
        }

        /// <summary>
        /// Adds a DateTime for when the plane last had a maintanance
        /// </summary>
        /// <param name="lastMaintanance"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneLastMaintanance(DateTime lastMaintanance)
        {
            _plane.PlaneLastMaintanace = lastMaintanance;
            return this;
        }

        /// <summary>
        /// Adds a DateTime for when the next scheduled maintanance is
        /// </summary>
        /// <param name="scheduledDate"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneScheduledMaintanance(DateTime scheduledDate)
        {
            _plane.ScheduledMaintananace = scheduledDate;
            return this;
        }

        /// <summary>
        /// Adds an int for the max passengercapacity for the plane
        /// </summary>
        /// <param name="planePassengerCapacity"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlanePassengerCapacity(int planePassengerCapacity)
        {
            _plane.PlanePassengerCapacity = planePassengerCapacity;
            return this;
        }

        /// <summary>
        /// Adds an int for the max cargocapacity the plane has
        /// </summary>
        /// <param name="planeCargoCapacity"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneCargoCapacity(int planeCargoCapacity)
        {
            _plane.PlaneCargoCapacity = planeCargoCapacity;
            return this;
        }

        /// <summary>
        /// Adds an int for the max fuelcapacity the plane has
        /// </summary>
        /// <param name="planeFuelCapacity"></param>
        /// <returns></returns>
        public PlaneBuilder AddPlaneFuelCapacity(int planeFuelCapacity)
        {
            _plane.PlaneFuelCapacity = planeFuelCapacity;
            return this;
        }

        /// <summary>
        /// Adds aflight object to the list of flights this plane has been used for
        /// </summary>
        /// <param name="flight"></param>
        /// <returns></returns>
        public PlaneBuilder AddFlightToHistory(Flight flight)
        {
            _plane.PlaneHistory.Add(flight);
            return this;
        }

        public PlaneBuilder AddPlaneToAirport(Airport airport)
        {
            _plane.CurrentAirport = airport;
            return this;
        }

        /// <summary>
        /// returns the finished Plane object
        /// </summary>
        /// <returns></returns>
        public Plane Build()
        {
            return _plane;
        }
    }
}
