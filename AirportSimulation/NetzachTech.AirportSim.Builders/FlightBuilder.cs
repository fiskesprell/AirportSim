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
    public class FlightBuilder
    {
        private readonly Flight _flight = new Flight();


        /// <summary>
        /// Sets the flight number.
        /// </summary>
        /// <param name="flight number">The flight number to set.</param>
        /// <returns></returns>
        public FlightBuilder AddFlightNumber(string number)
        {
            _flight.Number = number;
            return this;
        }

        /// <summary>
        /// Sets the company operating the flight.
        /// </summary>
        /// <param name="company">The company to set.</param>
        /// <returns></returns>
        public FlightBuilder AddFlightCompany(string company)
        {
            _flight.Company = company;
            return this;
        }

        /// <summary>
        /// Sets the flight type.
        /// </summary>
        /// <param name="flight type">The flight type to set.</param>
        /// <returns></returns>
        public FlightBuilder AddFlightType(FlightType flightType)
        {
            _flight.FlightType = flightType;
            return this;
        }

        /// <summary>
        /// Sets the assigned gate.
        /// </summary>
        /// <param name="gate">The assigned gate to set.</param>
        /// <returns></returns>
        public FlightBuilder AddAssignedGate(Gate gate)
        {
            _flight.AssignedGate = gate;
            return this;
        }

        /// <summary>
        /// Sets the flight as international or domestic.
        /// </summary>
        /// <param name="isInternational">The international/domestic status to set.</param>
        /// <returns></returns>
        public FlightBuilder AddIsInternational(bool isInternational)
        {
            _flight.IsInternational = isInternational;
            return this;
        }

        /// <summary>
        /// Sets the flights scheduled arrival or departure.
        /// </summary>
        /// <param name="scheduled">The scheduled time to set.</param>
        /// <returns></returns>
        public FlightBuilder AddFlightSchedule(DateTime scheduledDay, int scheduledHour, int scheduledMinutes)
        {
            _flight.ScheduledDay = scheduledDay;
            _flight.ScheduledHour = scheduledHour;
            _flight.ScheduledMinutes = scheduledMinutes;
            return this;
        }

        /// <summary>
        /// Sets the flights destination airport.
        /// </summary>
        /// <param name="destination airport">The airport to set.</param>
        /// <returns></returns>
        public FlightBuilder AddDestinationAirport(Airport airport)
        {
            _flight.DestinationAirport = airport;
            return this;
        }

        /// <summary>
        /// Sets the flight status.
        /// </summary>
        /// <param name="flight status">The flight status to set.</param>
        /// <returns></returns>
        public FlightBuilder AddFlightStatus(FlightStatus flightStatus)
        {
            _flight.Status = flightStatus;
            return this;
        }

        /// <summary>
        /// Sets the flight frequency.
        /// </summary>
        /// <param name="flight frequency">The flight frequency to set.</param>
        /// <returns></returns>
        public FlightBuilder AddFlightFrequency(FlightFrequency flightFrequency)
        {
            _flight.Frequency = flightFrequency;
            return this;
        }

        /// <summary>
        /// Sets the flight direction.
        /// </summary>
        /// <param name="flight direction">The flight direction to set.</param>
        /// <returns></returns>
        public FlightBuilder AddFlightDirection(FlightDirection direction)
        {
            _flight.FlightDirection = direction;
            return this;
        }

        /// <summary>
        /// Sets the plane used for the flight.
        /// </summary>
        /// <param name="plane">The plane to set.</param>
        /// <returns></returns>
        public FlightBuilder AddPlane(Plane plane)
        {
            _flight.AssignedPlane = plane;
            return this;
        }

        /// <summary>
        /// Sets the logging status for the flight.
        /// </summary>
        /// <param name="true/false">The logging status to set.</param>
        /// <returns></returns>
        public FlightBuilder AddLogging(bool logging)
        {
            _flight.Logging = logging;
            return this;
        }

        /// <summary>
        /// Sets the desired taxi for the flight.
        /// </summary>
        /// <param name="taxi">The taxi to set.</param>
        /// <returns></returns>
        public FlightBuilder AddAssignedTaxi(Taxi taxi)
        {
            _flight.AssignedTaxi = taxi;
            return this;
        }

        /// <summary>
        /// Sets the desired runway for the flight.
        /// </summary>
        /// <param name="plane">The runway to set.</param>
        /// <returns></returns>
        public FlightBuilder AddAssignedRunway(Runway runway)
        {
            _flight.AssignedRunway = runway;
            return this;
        }

        /// <summary>
        /// Builds and returns the configured Flight object.
        /// </summary>
        /// <returns>The built Flight object.</returns>
        public Flight Build()
        {
            return _flight;
        }



    }

}
