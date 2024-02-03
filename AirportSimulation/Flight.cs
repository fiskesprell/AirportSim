﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AirportSimulation
{
    [Flags]
    public enum FlightType
    {
        Commercial = 1,
        Transport = 2,
        Personal = 4,
        Military = 8,
    }

    public enum Direction
    {
        Incoming,
        Outgoing
    }

    public enum FlightStatus
    {
        OnTime,
        ArrivingDelayed,
        DepartingDelayed,
        Boarding,
        Departing,
        Departed,
        Arrived
    }

    public enum Frequency
    {
        OneTime,
        Daily,
        Weekly
    }


    public class Flight
    {
        private string Number { get; set; }
        private string Company { get; set; } = "Norwegian";
        private FlightType FlightType { get; set; } = FlightType.Commercial;
        private Gate AssignedGate { get; set; }
        private bool IsInternational { get; set; } = false;

        private DateTime ScheduledDay { get; set; }
        private int ScheduledHour { get; set; } = 0;
        private int ScheduledMinutes { get; set; } = 0;
        private string Destination { get; set; }
        
        private FlightStatus Status { get; set; } = FlightStatus.OnTime;
        private Frequency Frequency { get; set; } = Frequency.OneTime;
        private Direction FlightDirection { get; set; }
        public int ElapsedDays = 0;
        public int ElapsedHours = 0;
        public int ElapsedMinutes = 0;
        private Airport CurrentAirport;
        private bool IsParked = false;
        private Taxi DesiredTaxi {  get; set; }
        private Runway DesiredRunway { get; set; }

        private bool IsTraveling { get; set; }

        private bool Logging { get; set; } = true;

        private List<string> LoggingEvents { get; set; }


        /// <summary>
        /// Creates a flight. Not the same as creating an aircraft. Needs a flightnumber, date and time of arrival/departure,
        /// direction of flight (incoming, outgoing) and an airport object (the airport it is either arriving to or departing from).
        /// </summary>
        /// <param name="number">Flight number. Commonly looks like "WN 417".</param>
        /// <param name="destination">Name of Airport the flight is going to.</param>
        /// <param name="travelDay">The date of departure.</param>
        /// <param name="hour">The hour of the departure. Follows the 24-hour clock. Putting 18 here means the flight leaves at 6PM (18:XX).</param>
        /// <param name="minute">The minute of the departure. Putting 30 here means the flight will leave at XX:30.</param>
        /// <param name="direction">Either <c>Direction.Outgoing</c> or <c>Direction.Incoming</c>. </param>
        /// <param name="airport">The Airport to which the flight belongs.</param>
        /// <exception cref="ArgumentException"></exception>
        public Flight(string number, string destination, DateTime travelDay, int hour, int minute, Direction direction, Airport airport)
        {
            this.Number = number;
            this.Destination = destination;
            this.CurrentAirport = airport;

            //Hvis de sender inn noe som ikke er en av kategoriene i Direction enumen så vil en exception kastes
            if (Enum.TryParse(direction.ToString(), out Direction flightDirection))
            {
                this.FlightDirection = direction;

                if (this.FlightDirection == Direction.Outgoing)
                {
                    this.ScheduledDay = travelDay;
                    this.ScheduledHour = hour;
                    this.ScheduledMinutes = minute;
                }
                else
                {
                    this.ScheduledDay = travelDay;
                    this.ScheduledHour = hour;
                    this.ScheduledMinutes = minute;
                }
            }
            else
            {
                throw new ArgumentException($"Invalid direction: {direction}. Expected values are {string.Join(", ", Enum.GetNames(typeof(Direction)))}.", nameof(direction));
            }

        }//Slutt konstruktør

        public Flight(string number, string destination, DateTime travelDay, int hour, int minute, Direction direction, Airport airport, bool isInternational, FlightType flightType, Frequency frequency, string company)
        {
            this.Number = number;
            this.Destination = destination;
            this.CurrentAirport = airport;
            this.ScheduledDay = travelDay;
            this.ScheduledHour = hour;
            this.ScheduledMinutes = minute;
            this.FlightType = flightType;
            this.Frequency = frequency;
            this.Company = company;
            this.FlightDirection = direction;
            this.IsInternational = isInternational;
            this.FlightType = flightType;

        }//Slutt overload konstruktør

        /// <summary>
        /// This method will continously update the elapsed time for each flight object
        /// </summary>
        public void updateElapsedTime(TimeSimulation timeSimulation)
        {
            this.ElapsedDays = timeSimulation.ElapsedDays;
            this.ElapsedHours = timeSimulation.ElapsedHours;
            this.ElapsedMinutes = timeSimulation.ElapsedMinutes;
        }//Slutt updateElapsedTime


        /// <summary>
        /// This method keeps track of the chain of events during the simulation
        /// </summary>
        public void flightSim(Airport airport, TimeSimulation timeSimulation)
        {
            DateTime startSim = timeSimulation.getStartDate();
            TimeSpan dayDifference = this.ScheduledDay - startSim;
            int adjustedTravelDay = dayDifference.Days;

            if (this.FlightDirection == Direction.Outgoing)
            {
                //Kalle på convertTime for å få riktig klokkeslett 1 time og 45 min "tilbake" i tid
                //Dessverre kan man ikke overskrive variabler så må lage nye variabler hver gang
                (int newHours1, int newMinutes1) = convertTime(ScheduledHour, ScheduledMinutes, 1, 45);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours1 && ElapsedMinutes == newMinutes1)
                {
                    //Logg flight BRA123 har fått gate {this.AssignedGate} tildelt. F.eks
                    Gate availableGate = findAvailableGate();
                    Taxi taxi = findTaxi();
                    taxi.addToQueue(this);
                    IsTraveling = true;

                    
                }
                (int newHours2, int newMinutes2) = convertTime(ScheduledHour, ScheduledMinutes, 1, 30);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours2 && ElapsedMinutes == newMinutes2)
                {
                    parkGate(AssignedGate);
                    IsTraveling = false;
                }

                //Derfor blir det newHours1, newHours2, osv
                (int newHours3, int newMinutes3) = convertTime(ScheduledHour, ScheduledMinutes, 1, 0);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours3 && ElapsedMinutes == newMinutes3)
                {
                    startBoarding();
                }

                (int newHours4, int newMinutes4) = convertTime(ScheduledHour, ScheduledMinutes, 0, 15);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours4 && ElapsedMinutes == newMinutes4)
                {
                    startDeparting();
                    Runway correctRunway = this.findRunway();
                    Taxi correctTaxi = this.findTaxi();
                    this.AssignedGate.transferFlightToTaxi(this);
                    

                }

                (int newHours5, int newMinutes5) = convertTime(ScheduledHour, ScheduledMinutes, 0, 5);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours5 && ElapsedMinutes == newMinutes5)
                {
                    
                }

                (int newHours6, int newMinutes6) = convertTime(ScheduledHour, ScheduledMinutes, 0, 0);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours6 && ElapsedMinutes == newMinutes6)
                {
                    if(DesiredRunway.getFlightOnRunway() == this)
                    {
                        takeoff(DesiredRunway);
                    }
                }

            }
            else
            {
                if (ElapsedHours == ScheduledHour && ElapsedMinutes == ScheduledMinutes - 20)
                {
                    Gate availableGate = this.findAvailableGate();
                    Runway bestRunway = this.findRunway();
                    bestRunway.enqueueFlight(this);

                }
            }
            
            
            if (this.Frequency == Frequency.OneTime && (this.Status == FlightStatus.Arrived || this.Status == FlightStatus.Departed))
            {
                airport.addCompletedFlight(this);
                airport.removeCompletedFlightFromAllFlights(this);
            }

            /*
            if (this.Logging && ElapsedDays == adjustedTravelDay + 1 && ElapsedHours == 0 && ElapsedMinutes == 0)
            {
                foreach(var log in LoggingEvents)
                {
                    Console.WriteLine(log);
                }
            }*/

            if (this.Frequency == Frequency.Daily && this.Status == FlightStatus.Departed)
            {
                airport.removeCompletedFlightFromAllFlights(this);
                DateTime newDate = this.ScheduledDay.AddDays(1);
                Flight dailyFlight = new Flight(this.Number, this.Destination, newDate, this.ScheduledHour, this.ScheduledMinutes, this.FlightDirection, this.CurrentAirport, this.IsInternational, this.FlightType, this.Frequency, this.Company);
                dailyFlight.setDesiredRunway(null);
                dailyFlight.setDesiredTaxi(null);
                dailyFlight.setAssignedGate(null);
                dailyFlight.setStatus(FlightStatus.OnTime);
                if (airport.getAllFlights().Count() == 0)
                {
                    airport.addFlight(dailyFlight);
                }
                else
                {
                    foreach (var flight in airport.getAllFlights())
                    {
                        if (!string.Equals(flight.getFlightNumber(), dailyFlight.Number))
                        {
                            airport.addFlight(dailyFlight);
                        }
                    }
                }
                

                Console.WriteLine("\nDette er flightene i listen etter legger til nytt:");
                foreach (Flight flight in airport.getAllFlights())
                {
                    Console.Write(flight.getFlightNumber());
                }


            }

            if (this.Frequency == Frequency.Weekly && this.Status == FlightStatus.Departed)
            {
                DateTime newDate = this.ScheduledDay.AddDays(7);
                Flight weeklyFlight = new Flight(this.Number, this.Destination, newDate, this.ScheduledHour, this.ScheduledMinutes, this.FlightDirection, this.CurrentAirport, this.IsInternational, this.FlightType, this.Frequency, this.Company);
                weeklyFlight.setDesiredRunway(null);
                weeklyFlight.setDesiredTaxi(null);
                weeklyFlight.setAssignedGate(null);
                airport.addFlight(weeklyFlight);
                if (airport.getAllFlights().Count() == 0)
                {
                    airport.addFlight(weeklyFlight);
                }
                else
                {
                    foreach (var flight in airport.getAllFlights())
                    {
                        if (!string.Equals(flight.getFlightNumber(), weeklyFlight.Number))
                        {
                            airport.addFlight(weeklyFlight);
                        }
                    }
                }
            }
        }//Slutt flightSim

        public void takeoff(Runway runway)
        {
            runway.setFlightOnRunway(this);
            //broom broom
            this.Status = FlightStatus.Departed;
            if (ElapsedMinutes == 0)
            {
                string elapsedMinutes = "00";
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + elapsedMinutes + " flight " + this.Number + " has taken off");
            }
            Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has taken off");
            runway.setFlightOnRunway(null);
            runway.setIsAvailable(true);
        }//Slutt takeoff

        public void land(Runway runway)
        {
            runway.setFlightOnRunway(this);
            this.Status = FlightStatus.Arrived;
            Console.WriteLine("Day: " + ElapsedDays + " -  at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has landed");
            DesiredTaxi.addToQueue(this);
            runway.setFlightOnRunway(null);
            runway.setIsAvailable(true);
            if (Logging)
            {
                string landing = "Flyet har tatt av klokken" + ElapsedHours + ":" + ElapsedMinutes;
                this.LoggingEvents.Add(landing);
            }
        }//Slutt land


        /// <summary>
        /// Parks the flight at a specified gate and updates the flight and gate statuses accordingly.
        /// Throws an exception if the gate is null or not available.
        /// </summary>
        public void parkGate(Gate gate)
        {
            Gate gateToPark = this.AssignedGate;

            // If there's no pre-assigned gate, find an available one
            if (gateToPark == null)
            {
                gateToPark = findAvailableGate();

            }
            if (gateToPark != null)

            {
                this.IsParked = true;
                gateToPark.setCurrentHolder(this);

            }

            if (ElapsedMinutes == 0)
            {
                string elapsedMinutes = "00";
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + elapsedMinutes + " flight " + this.Number + " has parked at at " + AssignedGate.getGateName());
            }
            else
            {
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has parked at at " + AssignedGate.getGateName());
            }
        }//Slutt parkGate

        /// <summary>
        /// This method will change the status of the flight. 
        /// </summary>
        public void setStatus(FlightStatus status)
        {
            Status = status;
        }//Slutt changeStatus

        /// <summary>
        /// This method will loop through all terminals, then all the gates in that terminal, and find a gate that is available and have the correct licence
        /// </summary>
        public Gate findAvailableGate()
        {
            //Loope gjennom alle connected gates til alle terminaler som har samme bool verdi på innland utland
            foreach (var terminal in CurrentAirport.getAllTerminals())
            {
                if (terminal.IsInternational == this.IsInternational)
                {
                    foreach(var gate in terminal.getConnectedGates())
                    {
                        //Denne må fikses slik at den kan se om gaten har riktig lisens
                        if (gate.getIsAvailable() == true && gate.checkGateLicence(this) == true)
                        {
                            this.AssignedGate = gate;
                            gate.setIsAvailable(false);
                            Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has found an available gate:  " + this.AssignedGate.getGateName());
                            return gate;
                        }
                    }
                }
                
            }
            //Hvis den ikke finner en ledig gate så vil den returnere null
            return null;
        }//Slutt findAvailableGate

        /// <summary>
        /// This method will find an available taxiway depending on your gate and runway. This will return a taxi object
        /// </summary>
        public Taxi findTaxi()
        {
            Taxi selectedTaxi = null;
            int minQueueLength = int.MaxValue;

            if (this.FlightDirection == Direction.Outgoing)
            {
                if (this.AssignedGate.getConnectedTaxis().Count() == 0)
                {
                    throw new Exception("This gate is not connected to any taxis. Please make a new taxi and connect it, or connect an existing taxi");
                }
                // For outgoing flights, consider taxiways connected to the assigned gate
                if (this.AssignedGate != null && AssignedGate.getConnectedTaxis() != null)
                {
                    foreach (Taxi taxi in AssignedGate.getConnectedTaxis())
                    {
                        if (taxi.IsAvailable && taxi.TaxiQueue.Count < minQueueLength)
                        {
                            selectedTaxi = taxi;
                            minQueueLength = taxi.TaxiQueue.Count;
                        }
                        Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has been assigned " + selectedTaxi.getName());
                        this.DesiredTaxi = selectedTaxi;
                        return selectedTaxi;
                    }
                    
                }
            }

            else if (this.FlightDirection == Direction.Incoming)
            {
                // For incoming flights, a different selection strategy is needed
                // This could involve selecting from a global list of taxiways, for example

                foreach (Taxi taxi in CurrentAirport.getAllTaxis())
                {
                    if (taxi.IsAvailable && taxi.TaxiQueue.Count < minQueueLength)
                    {
                        selectedTaxi = taxi;
                        minQueueLength = taxi.TaxiQueue.Count;
                    }
                    Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has been assigned " + selectedTaxi.getName());
                    this.DesiredTaxi = selectedTaxi;
                    return selectedTaxi;
                }
                
            }
            return null;
        }//Slutt findTaxi

        /// <summary>
        /// This method will find a runway based on your taxi. This will return a runway object
        /// </summary>
        public Runway findRunway()
        {
            Runway selectedRunway = null;
            int minQueueLength = int.MaxValue;

            if (this.FlightDirection == Direction.Outgoing)
            {

                // For outgoing flights, consider runways suitable for take-off
                foreach (Runway runway in DesiredTaxi.getConnectedRunways())
                {
                    if (runway.RunwayQueue.Count < minQueueLength)
                    {
                        selectedRunway = runway;
                        minQueueLength = runway.RunwayQueue.Count;
                    }
                }
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has been assigned " + selectedRunway.getRunwayName());
                DesiredRunway = selectedRunway;
                return selectedRunway;
            }
            else if (this.FlightDirection == Direction.Incoming)
            {
                // For incoming flights, consider runways suitable for landing
                foreach (Runway runway in DesiredTaxi.getConnectedRunways())
                {
                    //Hva er IsAvailableForLanding?
                    if (runway.RunwayQueue.Count < minQueueLength)
                    {
                        selectedRunway = runway;
                        minQueueLength = runway.RunwayQueue.Count;
                    }
                }
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has been assigned " + selectedRunway.getRunwayName());
                DesiredRunway = selectedRunway;
                return selectedRunway;
            }
            return null;
            
        }//Slutt findRunway

        /// <summary>
        /// This method will take a scheduled time and correctly turn back the given subtracted time
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minutes"></param>
        /// <param name="subtractedHours"></param>
        /// <param name="subtractedMinutes"></param>
        /// <returns>A tuple with two ints, one represents hour, the other minute</returns>
        public (int, int) convertTime(int hour, int minutes, int subtractedHours, int subtractedMinutes)
        {
            int newHours = hour - subtractedHours;
            int newMinutes = minutes - subtractedMinutes;

            if (newHours < 0)
            {
                newHours = 24 + newHours;
            }

            if (newMinutes < 0)
            {
                newMinutes = 60 + newMinutes;
                newHours -= 1;
            }

            return (newHours, newMinutes);

        }

        /// <summary>
        /// Get method for the FlightStatus of a flight
        /// </summary>
        public FlightStatus getStatus()
        {
            return this.Status;
        }

        /// <summary>
        /// Get method for AssignedGate. This will return a gate object
        /// </summary>
        public Gate getAssignedGate()
        {
            return AssignedGate;
        }

        public void setAssignedGate(Gate gate)
        {
            AssignedGate = gate;
        }

        /// <summary>
        /// Get method IsInternational. This will return a bool value
        /// </summary>
        public bool getIsInternational()
        {
            return this.IsInternational;
        }

        public Direction getDirection()
        {
            return this.FlightDirection;
        }

        public void setDesiredTaxi(Taxi taxi)
        {
            DesiredTaxi = taxi;
        }

        public Taxi getDesiredTaxi()
        {
            return DesiredTaxi;
        }

        public void setDesiredRunway(Runway runway)
        {
            DesiredRunway = runway;
        }

        public Runway getDesiredRunway()
        {
            return DesiredRunway;
        }

        public Frequency getFlightFrequency()
        {
            return this.Frequency;
        }

        public void setFlightFrequency(Frequency frequency)
        {
            this.Frequency = frequency;
        }

        public void setCompany(string name)
        {
            this.Company = name;
        }

        public string getCompany()
        {
            return this.Company;
        }

        public void setFlightType(FlightType flightType)
        {
            this.FlightType = flightType;
        }

        public FlightType GetFlightType()
        {
            return this.FlightType;
        }

        public void startBoarding()
        {
            this.Status = FlightStatus.Boarding;
            Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " started boarding");
        }

        public void startDeparting()
        {
            this.Status = FlightStatus.Departing;
            this.AssignedGate.setCurrentHolder(null);
            this.AssignedGate.setIsAvailable(true);
        }

        public bool getIsTraveling()
        {
            return this.IsTraveling;
        }

        public void setIsTraveling(bool isTraveling)
        {
            this.IsTraveling = isTraveling;
        }

        public string getFlightNumber()
        {
            return this.Number;
        }

        public void setLogging(bool logging)
        {
            this.Logging = logging;
        }
    }//Slutt Flight klassen
}//Slutt namespace


