using AirportSimulationCl;
using NetzachTech.AirportSim.FlightOperations;
using NetzachTech.AirportSim.Infrastructure;
using NetzachTech.AirportSim.Time;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AirportSimulation
{
    public class Flight
    {
        /// <summary>
        /// Represents a single flight within the airport simulation, including its details and current status.
        /// </summary>
        /// <property>
        /// Number - The flight identifier number.
        /// Company - The company operating the flight.
        /// FlightType - The type of flight, as defined in the FlightType enum.
        /// AssignedGate - The gate assigned to the flight.
        /// IsInternational - Indicates if the flight is international, true if international, false if domestic.
        /// ScheduledDay - The scheduled day of departure or arrival.
        /// ScheduledHour - The scheduled hour of departure or arrival.
        /// ScheduledMinutes - The scheduled minute of departure or arrival.
        /// Destination - The flight's destination.
        /// Status - The current status of the flight, as defined in the FlightStatus enum.
        /// Frequency - The frequency of the flight.
        /// FlightDirection - The direction of the flight, as defined in the FlightDirection enum.
        /// ElapsedDays - The number of days elapsed since the flight was scheduled.
        /// ElapsedHours - The number of hours elapsed since the flight was scheduled.
        /// ElapsedMinutes - The number of minutes elapsed since the flight was scheduled.
        /// CurrentAirport - The airport where the flight is currently located or being simulated.
        /// IsParked - Indicates if the flight is currently parked at a gate.
        /// DesiredTaxi - The taxiway desired or assigned for the flight's taxi.
        /// DesiredRunway - The runway desired or assigned for the flight.
        /// IsTraveling - Indicates if the flight is currently in the traveling state.
        /// Logging - Indicates if logging is enabled for the flight events.
        /// HasLogged - Indicates if the flight has already logged an event.
        /// LogHistory - A collection of log entries related to the flight's operations.
        /// Delay - True = Delayed, False = On time.
        /// DelayInMinutes = Minutes the flight is delayed.
        /// </property>
        private string _number;
        public string Number
        { get => _number; set => _number = value;}
        
        private string _company;
        public string Company
        { get => _company; set => _company = value;}
        
        private FlightType _flightType = FlightType.Commercial;
        public FlightType FlightType
        { get => _flightType; set => _flightType = value;}
        
        private Gate _assignedGate;
        public Gate AssignedGate
        { get => _assignedGate; set => _assignedGate = value;}
        
        private bool _isInternational = false;
        public bool IsInternational
        { get => _isInternational; set => _isInternational = value;}

        private Plane _assignedPlane;
        public Plane AssignedPlane
        { get => _assignedPlane; set => _assignedPlane = value;}

        private DateTime _scheduledDay;
        public DateTime ScheduledDay
        { get => _scheduledDay; set => _scheduledDay = value;}

        private int _scheduledHour = 0;
        public int ScheduledHour
        { get => _scheduledHour; set => _scheduledHour = value;}

        private int _scheduledMinutes = 0;
        public int ScheduledMinutes
        { get => _scheduledMinutes; set => _scheduledMinutes = value;}

        private Airport _destinationAirport;
        public Airport DestinationAirport
        { get => _destinationAirport; set => _destinationAirport = value;}
        
        private FlightStatus _status = FlightStatus.OnTime;
        public FlightStatus Status
        { get => _status; set => _status = value; }

        private FlightFrequency _frequency = FlightFrequency.OneTime;
        public FlightFrequency Frequency
        { get => _frequency; set => _frequency = value;}

        private FlightDirection _flightDirection;
        public FlightDirection FlightDirection
        { get => _flightDirection; set => _flightDirection = value;}

        private int _elapsedDays = 0;
        public int ElapsedDays
        { get => _elapsedDays; set => _elapsedDays = value;}
           

        private int _elapsedHours = 0;
        public int ElapsedHours
        { get => _elapsedHours; set => _elapsedHours = value; }

        private int _elapsedMinutes = 0;
        public int ElapsedMinutes
        { get => _elapsedMinutes; set => _elapsedMinutes = value; }

        private Airport _currentAirport;
        public Airport CurrentAirport
        { get => _currentAirport; set => _currentAirport = value;}


        private bool _isParked = false;
        public bool IsParked
        { get => _isParked; set => _isParked = value;}

        private Taxi _assignedTaxi;
        public Taxi AssignedTaxi
        { get => _assignedTaxi; set => _assignedTaxi = value;}

        private Runway _assignedRunway;
        public Runway AssignedRunway
        { get => _assignedRunway; set => _assignedRunway = value;}

        private bool _isTraveling;
        public bool IsTraveling
        { get => _isTraveling; set => _isTraveling = value;}


        private bool _logging = true;
        public bool Logging
        { get => _logging; set => _logging = value;}


        private bool _hasLogged = false;
        public bool HasLogged
        { get => _hasLogged; set => _hasLogged = value;}


        private List<string> _logHistory = new List<string>();
        public List<string> LogHistory
        { get => _logHistory; }

        private bool _delayed = false;
        public bool Delayed
        { get => _delayed; set => _delayed = value;}

        private int _delayInMinutes = 0;
        public int DelayInMinutes
        { get => _delayInMinutes; set => _delayInMinutes = value;}


        // Outgoing
        //ScheduledXFindGateOutgoing
        private int ScheduledHourFindGateOutgoing = 1;
        private int ScheduledMinuteFindGateoutgoing = 45;

        //ScheduledXParkAtGateOutgoing
        private int ScheduledHourParkAtGateOutgoing = 1;
        private int ScheduledMinuteParkAtGateOutgoing = 30;
        
        //ScheduledXStartBoardingOutgoing
        private int ScheduledHourStartBoardingOutgoing = 1;
        private int ScheduledMinuteStartBoardingOutgoing = 0;
        
        //ScheduledXDepartFromGateOutgoing
        private int ScheduledHourDepartFromGateOutgoing = 0;
        private int ScheduledMinuteDepartFromGateOutgoing = 15;
        
        //ScheduledXTakeoffOutgoing
        private int ScheduledHourTakeoffOutgoing = 0;
        private int ScheduledMinuteTakeoffOutgoing = 0;
        
        // Incoming
        //ScheduledXFindGateIncoming
        private int ScheduledHourFindGateIncoming = 0;
        private int ScheduledMinuteFindGateIncoming = 0;
        
        //ScheduledXLeaveRunwayIncoming
        private int ScheduledHourLeaveRunwayIncoming = 0;
        private int ScheduledMinuteLeaveRunwayIncoming = 1;
        
        //ScheduledXParkAtGateIncoming
        private int ScheduledHourParkAtGateIncoming = 0;
        private int ScheduledMinuteParkAtGateIncoming = 11;
        
        //ScheduledXCompletedDisembarkation
        private int ScheduledHourCompletedDisembarkation = 0;
        private int ScheduledMinuteCompletedDisembarkation = 41;



        /// <summary>
        /// Creates a flight. Not the same as creating a plane. Needs a flightnumber, date and time of arrival/departure,
        /// direction of flight (incoming, outgoing or other) and an airport object (the airport it is either arriving to or departing from).
        /// </summary>
        /// <param name="flightNumber">Flight number. Commonly looks like "WN417".</param>
        /// <param name="destination">Airport the flight is going to.</param>
        /// <param name="travelDay">The date of departure.</param>
        /// <param name="travelHour">The hour of the departure. Follows the 24-hour clock. Putting 18 here means the flight leaves at 6PM (18:XX).</param>
        /// <param name="travelMinute">The minute of the departure. Putting 30 here means the flight will leave at XX:30.</param>
        /// <param name="direction">Either <c>Direction.Outgoing</c> or <c>Direction.Incoming</c>. </param>
        /// <param name="airport">The Airport to which the flight belongs.</param>
        /// <exception cref="ArgumentException"></exception>
        public Flight(string flightNumber, Airport destination, DateTime travelDay, int travelHour, int travelMinute, FlightDirection direction, Airport airport)
        {
            this.Number = flightNumber;
            this.DestinationAirport = destination;
            this.CurrentAirport = airport;
            //Assigne et ledig plane til flighten når det opprettes
            //this.AssignedPlane = AssignAvailablePlaneToFlight();

            if (travelHour > 23 || travelHour < 0)
                throw new InvalidScheduledTimeException("There are only 24 hours in a day. Expected values are between 0 and 23.");

            if (travelMinute > 59 || travelMinute < 0)
                throw new InvalidScheduledTimeException("There are only 60 minutes in an hour. Expected values are between 0 and 59");

            //Hvis de sender inn noe som ikke er en av kategoriene i Direction enumen så vil en exception kastes
            if (Enum.TryParse(direction.ToString(), out FlightDirection flightDirection))
            {
                this.FlightDirection = direction;

                if (this.FlightDirection == FlightDirection.Outgoing)
                {
                    this.ScheduledDay = travelDay;
                    this.ScheduledHour = travelHour;
                    this.ScheduledMinutes = travelMinute;
                }
                else
                {
                    this.ScheduledDay = travelDay;
                    this.ScheduledHour = travelHour;
                    this.ScheduledMinutes = travelMinute;
                }
            }
            else
            {
                throw new ArgumentException($"Invalid direction: {direction}. Expected values are {string.Join(", ", Enum.GetNames(typeof(FlightDirection)))}.", nameof(direction));
            }

        }//Slutt konstruktør

        /// <summary>
        /// Overload of a flight with additional parameters.
        /// </summary>
        /// <param name="flightNumber">Flight number. Commonly looks like "WN417".</param>
        /// <param name="destination">Name of Airport the flight is going to.</param>
        /// <param name="travelDay">The date of departure.</param>
        /// <param name="travelHour">The hour of the departure. Follows the 24-hour clock. Putting 18 here means the flight leaves at 6PM (18:XX).</param>
        /// <param name="travelMinute">The minute of the departure. Putting 30 here means the flight will leave at XX:30.</param>
        /// <param name="direction">Either <c>Direction.Outgoing</c>, <c>Direction.Incoming</c> or <c>Direction.Other</c> </param>
        /// <param name="airport">The Airport to which the flight belongs.</param>
        /// <param name="isInternational">Indicates whether the flight is international.</param>
        /// <param name="flightType">Specifies the type of flight, as defined in the <c>FlightType</c> enum.</param>
        /// <param name="frequency">Defines the frequency of the flight, such as one-time, daily, or weekly, as specified in the <c>Frequency</c> enum.</param>
        /// <param name="company">The name of the company operating the flight.</param>
        public Flight(string flightNumber, Airport destination, DateTime travelDay, int travelHour, int travelMinute, FlightDirection direction, Airport airport, bool isInternational, FlightType flightType, FlightFrequency frequency, string company)
        {
            this.Number = flightNumber;
            this.DestinationAirport = destination;
            this.CurrentAirport = airport;
            this.ScheduledDay = travelDay;
            this.ScheduledHour = travelHour;
            this.ScheduledMinutes = travelMinute;
            this.FlightType = flightType;
            this.Frequency = frequency;
            this.Company = company;
            this.FlightDirection = direction;
            this.IsInternational = isInternational;
            this.FlightType = flightType;

        }//Slutt overload konstruktør

        public Flight() { }


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
        public void FlightSim(TimeConfigManager timeConfigManager, Airport airport, TimeSimulation timeSimulation)
        {
            DateTime startSim = timeSimulation.StartDate;
            TimeSpan dayDifference = this.ScheduledDay - startSim;
            int adjustedTravelDay = dayDifference.Days;
            
            // ~~~~ Outgoing Flight ~~~~
            if (this.FlightDirection == FlightDirection.Outgoing)
            {
                //Kalle på convertTime for å få riktig klokkeslett 1 time og 45 min "tilbake" i tid
                //Dessverre kan man ikke overskrive variabler så må lage nye variabler hver gang
                (int OutgoingFindGateTimeHours, int OutgoingFindGateTimeMinutes) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, this.ScheduledHourFindGateOutgoing, this.ScheduledMinuteFindGateoutgoing);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == OutgoingFindGateTimeHours && ElapsedMinutes == OutgoingFindGateTimeMinutes)
                {
                    Gate availableGate = FindAvailableGate();

                    if (this.AssignedGate != null)
                    {
                        Taxi taxi = FindTaxi();
                        taxi.AddToTaxiQueue(this);
                        IsTraveling = true;
                    }
                    else
                    {
                        UpdateOutgoingNewHoursAndMinutesFromSetPoint(1);
                    }

                    
                }
                (int OutgoingParkAtGateTimeHours, int OutgoingParkAtGateTimeMinutes) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, this.ScheduledHourParkAtGateOutgoing, this.ScheduledMinuteParkAtGateOutgoing);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == OutgoingParkAtGateTimeHours && ElapsedMinutes == OutgoingParkAtGateTimeMinutes)
                {
                    ParkFlightAtGate(AssignedGate);
                    IsTraveling = false;
                }

                (int BoardingTimeHours, int BoardingTimeMinutes) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, this.ScheduledHourStartBoardingOutgoing, this.ScheduledMinuteStartBoardingOutgoing);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == BoardingTimeHours && ElapsedMinutes == BoardingTimeMinutes)
                {
                    StartDeparturePrep();
                    Runway correctRunway = this.FindRunway();
                    Taxi correctTaxi = this.FindTaxi();
                }

                if (this.AssignedGate != null && this.AssignedRunway != null)
                {
                    this.ScheduledMinuteDepartFromGateOutgoing = timeConfigManager.GetTravelTime(this);
                }

                (int DepartFromGateTimeHours, int DepartFromGateTimeMinutes) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, this.ScheduledHourDepartFromGateOutgoing, this.ScheduledMinuteDepartFromGateOutgoing);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == DepartFromGateTimeHours && ElapsedMinutes == DepartFromGateTimeMinutes)
                {

                    StartDeparture();
                    this.AssignedGate.TransferFlightToTaxi(this);
                }

              
                (int TakeoffTimeHours, int TakeoffTimeMinutes) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, this.ScheduledHourTakeoffOutgoing, this.ScheduledMinuteTakeoffOutgoing);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == TakeoffTimeHours && ElapsedMinutes == TakeoffTimeMinutes)
                {
                    if(AssignedRunway.FlightOnRunway == this)
                    {
                        TakeoffFlight(AssignedRunway);
                    }
                }
            }

            // ~~~~ Incoming Flight ~~~~
            else if (this.FlightDirection == FlightDirection.Incoming)
            {
                (int newHours1, int newMinutes1) = ConvertTimeForwards(ScheduledHour, ScheduledMinutes, this.ScheduledHourFindGateIncoming, this.ScheduledMinuteFindGateIncoming);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours1 && ElapsedMinutes == newMinutes1)
                {
                    IncomingFlightPreperation();
                    if (this.AssignedGate == null)
                    {
                        FindGateBackup();
                    }
                }

                if (this.AssignedGate != null && this.AssignedRunway != null)
                {
                    this.ScheduledMinuteParkAtGateIncoming = timeConfigManager.GetTravelTime(this);
                }

                (int newHours2, int newMinutes2) = ConvertTimeForwards(ScheduledHour, ScheduledMinutes, this.ScheduledHourLeaveRunwayIncoming, this.ScheduledMinuteLeaveRunwayIncoming);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours2 && ElapsedMinutes == newMinutes2)
                {
                    IncomingFlightFromRunwayToTaxi();
                }

                (int newHours3, int newMinutes3) = ConvertTimeForwards(ScheduledHour, ScheduledMinutes, this.ScheduledHourParkAtGateIncoming, this.ScheduledMinuteParkAtGateIncoming);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours3 && ElapsedMinutes == newMinutes3)
                {
                    IncomingFlightFromTaxiToGate();
                }

                (int newHours4, int newMinutes4) = ConvertTimeForwards(ScheduledHour, ScheduledMinutes, this.ScheduledHourCompletedDisembarkation, this.ScheduledMinuteCompletedDisembarkation);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours4 && ElapsedMinutes == newMinutes4)
                {
                    IncomingFlightFromGateToComplete();
                }


            }

            else
            {
                if (ElapsedHours == ScheduledHour && ElapsedMinutes == ScheduledMinutes - 20)
                {
                    Gate availableGate = this.FindAvailableGate();
                    Runway bestRunway = this.FindRunway();
                    bestRunway.AddToRunwayQueue(this);

                }
            }
            //Console.Write("ElapsedDays: " + ElapsedDays + " - AdjustedTravelDay: " + adjustedTravelDay + " - ElapsedMinutes: " + ElapsedMinutes + " - ElapsedHours: " + ElapsedHours + " - Logging: " + this.Logging + " - HasLogged: " + this.HasLogged + " - Status: " + this.Status + "\n");
            if (this.Logging && !(this.HasLogged) && ElapsedDays == (adjustedTravelDay+1) && ElapsedHours == 0 && ElapsedMinutes == 0 && (this.Status == FlightStatus.Departed || this.Status == FlightStatus.Completed))
            {
                Console.WriteLine("\nThis is the eventlog for flight: " + this.Number);
                foreach (string log in LogHistory)
                {
                    Console.WriteLine(log);
                }
                this.HasLogged = true;
            }


            if (this.Frequency == FlightFrequency.OneTime && (this.Status == FlightStatus.Arrived || this.Status == FlightStatus.Departed))
            {
                if (Logging && HasLogged == true)
                {
                    airport.AddCompletedFlight(this);
                    airport.RemoveCompletedFlightFromAllFlights(this);
                }
                //Setter AssignedPlane til null og gjøre planet ledig igjen
                //this.AssignedPlane.PlaneIsAvailable = true;
                //this.AssignedPlane = null;
            }
            
            
            if (this.Frequency == FlightFrequency.Daily && ( this.Status == FlightStatus.Departed || this.Status == FlightStatus.Completed ) && ElapsedHours == 1 && ElapsedMinutes == 0)
            {
                DateTime newDate = this.ScheduledDay.AddDays(1);
                this.ScheduledDay = newDate;
                this.SetFlightStatus(FlightStatus.OnTime);
                this.AssignedRunway = null;
                this.AssignedGate = null;
                this.AssignedTaxi = null;
                //Setter at flyet er nå på denne flyplassen og er ledig
                //this.AssignedPlane.CurrentAirport = this.DestinationAirport;
                //this.AssignedPlane.PlaneIsAvailable = true;
                //this.AssignedPlane = null;
                this.HasLogged = false;
                LogHistory.Clear();
                
            }

            if (this.Frequency == FlightFrequency.Weekly && (this.Status == FlightStatus.Departed || this.Status == FlightStatus.Completed) && ElapsedHours == 1 && ElapsedMinutes == 0)
            {
                DateTime newDate = this.ScheduledDay.AddDays(7);
                this.ScheduledDay = newDate;
                this.SetFlightStatus(FlightStatus.OnTime);
                this.AssignedRunway = null;
                this.AssignedGate = null;
                this.AssignedTaxi = null;
                //this.AssignedPlane.CurrentAirport = this.DestinationAirport;
                //this.AssignedPlane.PlaneIsAvailable = true;
                //this.AssignedPlane = null;
                this.HasLogged = false;
                LogHistory.Clear();
            }
        }//Slutt flightSim


        ///<summary>
        /// Method
        ///</summary>
        public void TakeoffFlight(Runway runway)
        {
            runway.FlightOnRunway = this;
            this.SetFlightStatus(FlightStatus.Departed);
            
            if (Logging)
            {
                string logMessage2 = $"Flight {Number} took off at Day: {ElapsedDays+1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}.";
                LogHistory.Add(logMessage2);
            }
            
            runway.FlightOnRunway = null;
            runway.IsAvailable = true;
        }//Slutt takeoff

        public void LandFlight(Runway runway)
        {
            runway.FlightOnRunway = this;
            this.SetFlightStatus(FlightStatus.Arrived);
            //Console.WriteLine("Day: " + ElapsedDays + " -  at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has landed");
            AssignedTaxi.AddToTaxiQueue(this);
            runway.FlightOnRunway = null;
            runway.IsAvailable = true;
            if (Logging)
            {
                string logMessage2 = $"Flight {Number} landed at Day: {ElapsedDays+1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}.";
                LogHistory.Add(logMessage2);
            }
        }//Slutt land


        /// <summary>
        /// Parks the flight at a specified gate and updates the flight and gate statuses accordingly.
        /// Throws an exception if the gate is null or not available.
        /// </summary>
        public void ParkFlightAtGate(Gate gate)
        {
            Gate gateToPark = this.AssignedGate;

            // If there's no pre-assigned gate, find an available one
            if (gateToPark == null)
            {
                gateToPark = FindAvailableGate();

            }
            if (gateToPark != null)

            {
                this.IsParked = true;
                this.IsTraveling = false;
                gateToPark.CurrentHolder = this;

            }
            if (Logging && FlightDirection == FlightDirection.Outgoing)
            {
                string logMessage = $"Flight {Number} parked at Gate: {gate.GateName} at Day: {ElapsedDays + 1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}.";
                LogHistory.Add(logMessage); 
            }
            
        }//Slutt parkGate

        /// <summary>
        /// This method will change the status of the flight. 
        /// </summary>
        public void SetFlightStatus(FlightStatus status)
        {
            if (Logging)
            {
                string logMessage2 = $"Flight {Number} changed its status to: {status} at Day: {ElapsedDays + 1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}.";
                LogHistory.Add(logMessage2);
            }
            this.Status = status;
        }//Slutt changeStatus

        /// <summary>
        /// This method will loop through all terminals, then all the gates in that terminal, and find a gate that is available and have the correct licence
        /// </summary>
        public Gate FindAvailableGate()
        {
            FlightType flightType = this.FlightType;

            bool foundTerminal = false;
            foreach (var terminal in CurrentAirport.AllTerminals)
            {
                if (terminal.IsInternational == this.IsInternational)
                {
                    foundTerminal = true;
                    bool foundGateLicence = false;
                    foreach(var gate in terminal.ConnectedGates)
                    {
                        if (gate.IsAvailable == true && gate.CheckGateLicence(this) == true)
                        {
                            this.AssignedGate = gate;
                            gate.IsAvailable = false;
                            foundGateLicence = true;
                            if (Logging)
                            {
                                string logMessage = $"Flight {Number} was assigned {gate.GateName} at Day: {ElapsedDays + 1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}.";
                                this.LogHistory.Add(logMessage);
                            }

                            if (this.AssignedGate == null)
                            {
                                if (this.FlightDirection == FlightDirection.Incoming)
                                {
                                    UpdateIncomingNewHoursAndMinutesFromSetPoint(6);
                                }
                                else if (this.FlightDirection == FlightDirection.Outgoing)
                                {
                                    UpdateOutgoingNewHoursAndMinutesFromSetPoint(1);
                                }
                            }
                            else
                            {
                                return gate;
                            }

                        }
                    }
                    
                    /* TODO: Remove or put back
                    if (!foundGateLicence)
                    {
                        throw new Exception($"\n\nException: You tried to simulate time for a flight with the FlightType: {this.FlightType}. There are no gates that have a licence for the assigned FlightType. Try adding more gates and add a licence for that FlightType or edit existing gates with addLicence()\n");
                    }
                    */
                }
                
            }
            if (!foundTerminal)
            {
                if (this.IsInternational)
                {
                    string international = "International";
                    throw new InvalidInfrastructureException($"\nThere are no terminals on this airport that accepts {international} flights. Try configuring your terminal(s) with setIsInternational(true) or add a terminal with the correct configuration\n");
                }
                else
                {
                    string international = "Domestic";
                    throw new InvalidInfrastructureException($"\nThere are no terminals on this airport that accepts {international} flights. Try configuring your terminal(s) with setIsInternational(false) or add a terminal with the correct configuration\n");
                }
            }

            return null;
        }//Slutt findAvailableGate

        /// <summary>
        /// This method will find an available taxiway depending on your gate and runway. This will return a taxi object
        /// </summary>
        public Taxi FindTaxi()
        {
            Taxi selectedTaxi = null;
            int minQueueLength = int.MaxValue;

            if (this.FlightDirection == FlightDirection.Outgoing)
            {
                if (this.AssignedGate.ConnectedTaxis.Count() == 0)
                {
                    throw new Exception("This gate is not connected to any taxis. Please make a new taxi and connect it, or connect an existing taxi");
                }
                // For outgoing flights, consider taxiways connected to the assigned gate
                if (this.AssignedGate != null && AssignedGate.ConnectedTaxis != null)
                {
                    foreach (Taxi taxi in AssignedGate.ConnectedTaxis)
                    {
                        if (taxi.IsAvailable && taxi.TaxiQueue.Count < minQueueLength)
                        {
                            selectedTaxi = taxi;
                            minQueueLength = taxi.TaxiQueue.Count;
                        }
                        this.AssignedTaxi = selectedTaxi;

                        if (Logging && FlightDirection == FlightDirection.Outgoing)
                        {
                            string logMessage = $"Flight {Number} was assigned {selectedTaxi.TaxiName} at Day: {ElapsedDays + 1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}.";
                            this.LogHistory.Add(logMessage);
                        }
                        return selectedTaxi;
                    }
                    
                }
            }

            else if (this.FlightDirection == FlightDirection.Incoming)
            {
                // For incoming flights, a different selection strategy is needed
                // This could involve selecting from a global list of taxiways, for example
                if (this.AssignedGate == null)
                {
                    FindAvailableGate();
                }

                foreach (Taxi taxi in CurrentAirport.AllTaxis)
                {
                    if (taxi.IsAvailable && taxi.TaxiQueue.Count < minQueueLength)
                    {
                        selectedTaxi = taxi;
                        minQueueLength = taxi.TaxiQueue.Count;
                    }
                    this.AssignedTaxi = selectedTaxi;
                    return selectedTaxi;
                }
                
            }
            return null;
        }//Slutt findTaxi

        /// <summary>
        /// This method will find a runway based on your taxi. This will return a runway object
        /// </summary>
        public Runway FindRunway()
        {
            Runway selectedRunway = null;
            int minQueueLength = int.MaxValue;

            if (this.FlightDirection == FlightDirection.Outgoing)
            {

                foreach (Runway runway in AssignedTaxi.ConnectedRunways)
                {
                    if (runway.RunwayQueue.Count < minQueueLength)
                    {
                        selectedRunway = runway;
                        minQueueLength = runway.RunwayQueue.Count;
                    }
                }
                AssignedRunway = selectedRunway;
                if (Logging)
                {
                    string logMessage = $"Flight {Number} was assigned {selectedRunway.RunwayName} at Day: {ElapsedDays + 1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}.";
                    this.LogHistory.Add(logMessage);
                }
                return selectedRunway;
            }

            else if (this.FlightDirection == FlightDirection.Incoming)
            {
                foreach (Runway runway in AssignedTaxi.ConnectedRunways)
                {
                    if (runway.RunwayQueue.Count < minQueueLength)
                    {
                        selectedRunway = runway;
                        minQueueLength = runway.RunwayQueue.Count;
                    }
                }
                AssignedRunway = selectedRunway;
                if (Logging)
                {
                    string logMessage = $"Flight {Number} was assigned {selectedRunway.RunwayName} at Day: {ElapsedDays + 1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}.";
                    this.LogHistory.Add(logMessage);
                }
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
        public (int, int) ConvertTimeBackwards(int hour, int minutes, int subtractedHours, int subtractedMinutes)
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

        public (int, int) ConvertTimeForwards(int hour, int minutes, int subtractedHours, int subtractedMinutes)
        {
            // TODO: Fiks hva som skjer om det går en dag fremover
            int newHours = hour + subtractedHours;
            int newMinutes = minutes + subtractedMinutes;

            while (newMinutes > 60)
            {
                newHours += 1;
                newMinutes -= 60;
            }

            return (newHours, newMinutes);

        }

        /// <summary>
        /// Starts departure preparations by changing flight status. Logging the event is handled here if enabled.
        /// </summary>
        /// <returns>AssignedGate</returns>
        public void StartDeparturePrep()
        {
            this.SetFlightStatus(FlightStatus.Boarding);
            if (Logging)
            {
                string logMessage2 = $"Flight {Number} started preparing for departure at Day: {ElapsedDays + 1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
                LogHistory.Add(logMessage2);
            }
        }   
        /// <summary>
        /// Starts flight departure and logs if enabled.
        /// </summary>
        public void StartDeparture()
        {
            this.SetFlightStatus(FlightStatus.Departing);
            this.AssignedGate.CurrentHolder = null;
            this.AssignedGate.IsAvailable = true;
            this.IsParked = false;
            this.IsTraveling = true;

            if (Logging)
            {
                string logMessage = $"Flight {Number} left the gate and started towards its runway at Day: {ElapsedDays + 1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
                LogHistory.Add(logMessage);
            }
        }

        public void IncomingFlightFromGateToComplete()
        {
            this.AssignedGate.IsAvailable = true;
            this.AssignedGate.CurrentHolder = null;
            this.SetFlightStatus(FlightStatus.Completed);
            this.AssignedRunway = null;
            this.AssignedTaxi = null;
            this.AssignedGate = null;

            if (Logging)
            {
                string logMessage = $"Flight {Number} has offloaded all its passangers and cargo and has completed its journey at Day: {ElapsedDays + 1}, Time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
                LogHistory.Add(logMessage);
            }
        }
        /// <summary>
        /// Method to find taxi for an incoming flight, and set flight status to: Landed.
        /// </summary>
        public void IncomingFlightPreperation()
        {
            // Dette finner Gate og setter this.AssignedGate = gate
            // Dette finner Taxi og setter this.DesiredTaxi = selectedTaxi;
            this.FindTaxi();
            // Dette finner Runway og setter this.DesiredRunway = desiredRunway
            this.AssignedRunway = FindRunway();
            this.SetFlightStatus(FlightStatus.Landed);

            // Logging
            if (Logging)
            {
                string logMessage = $"Flight {Number} landed on runway {this.AssignedRunway.RunwayName} at Day:{ElapsedDays + 1}, Time:{ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
                LogHistory.Add(logMessage);
            }
        }

        /// <summary>
        /// Method to add a flight to desired taxi queue and set flight status: OnWayToGate.
        /// </summary>
        public void IncomingFlightFromRunwayToTaxi()
        {
            this.SetFlightStatus(FlightStatus.OnWayToGate);

            if (Logging)
            {
                string logMessage = $"Flight {Number} exited the runway {this.AssignedRunway.RunwayName} and is entering the taxiway {this.AssignedTaxi.TaxiName} at Day:{ElapsedDays + 1}, Time:{ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
                LogHistory.Add(logMessage);
            }
        }
      /// < summary>
        /// Method for a flight that has arrived at gate.
        /// </summary>
        public void IncomingFlightFromTaxiToGate()
        {
            this.SetFlightStatus(FlightStatus.Offloading);

            // Logging
            if (Logging)
            {
                string logMessage = $"Flight {Number} exited the taxiway {this.AssignedTaxi.TaxiName} and parked at gate {this.AssignedGate.GateName} at Day: {ElapsedDays + 1}, Time:{ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
                LogHistory.Add(logMessage);
            }
        }

        // Methods to change time spent on Gate, Taxi, Runway etc.
        // Outgoing
        /// <summary>
        /// Hours & Minutes before takeoff to begin pre-boarding procedures.
        /// This includes finding available gate, finding correct taxiway, 
        /// and setting IsTraveling to true.
        /// Hour = 1 Minutes = 20 means this procedure begins 1 hour and 20 minutes
        /// before scheduled takeoff.
        /// </summary>
        /// <param name="hour">Hour(s) before takeoff to begin</param>
        /// <param name="minute">Minute(s) before takeoff to begin</param>
        public void SetOutgoingPreBoardingTime(int hour, int minute)
        {
            this.ScheduledHourFindGateOutgoing = hour;
            this.ScheduledMinuteFindGateoutgoing = minute;
        }

        /// <summary>
        /// Hours & Minutes before takeoff to begin onboarding procedures.
        /// This includes parking at gate and beginning onboarding.
        /// and setting IsTraveling to false.
        /// Hour = 1 Minutes = 20 means this procedure begins 1 hour and 20 minutes
        /// before scheduled takeoff.
        /// </summary>
        /// <param name="hour">Hour(s) before takeoff to begin</param>
        /// <param name="minute">Minute(s) before takeoff to begin</param>
        public void SetOutgoingBoardingTime(int hour, int minute)
        {
            this.ScheduledHourParkAtGateOutgoing = hour;
            this.ScheduledMinuteParkAtGateOutgoing = minute;
        }

        /// <summary>
        /// Hours & Minutes before takeoff to begin Departure Preperation procedures.
        /// This means setting Status to "Boarding"
        /// Hour = 1 Minutes = 20 means this procedure begins 1 hour and 20 minutes
        /// before scheduled takeoff.
        /// </summary>
        /// <param name="hour">Hour(s) before takeoff to begin</param>
        /// <param name="minute">Minute(s) before takeoff to begin</param>
        public void SetOutgoingDeparturePrepTime(int hour, int minute)
        {
            this.ScheduledHourStartBoardingOutgoing = hour;
            this.ScheduledMinuteStartBoardingOutgoing = minute;
        }

        /// <summary>
        /// Hours & Minutes before takeoff to begin Departure procedures.
        /// This includes:
        /// - finding a runway and taxi
        /// - Transferring flight from gate to taxiway
        /// Hour = 1 Minutes = 20 means this procedure begins 1 hour and 20 minutes
        /// before scheduled takeoff.
        /// </summary>
        /// <param name="hour">Hour(s) before takeoff to begin</param>
        /// <param name="minute">Minute(s) before takeoff to begin</param>
        public void SetOutgoingDepartureTime(int hour, int minute)
        {
            this.ScheduledHourDepartFromGateOutgoing = hour;
            this.ScheduledMinuteDepartFromGateOutgoing = minute;
        }

        /// <summary>
        /// Hours & Minutes before scheduled takeoff to begin takeoff.
        /// By default this value is set to 0 Hours 0 Minutes.
        /// This includes:
        /// - Taking off from Runway
        /// </summary>
        /// <param name="hour">Hour(s) before takeoff to begin</param>
        /// <param name="minute">Minute(s) before takeoff to begin</param>
        public void SetOutgoingTakeoffTime(int hour, int minute)
        {
            this.ScheduledHourTakeoffOutgoing = hour;
            this.ScheduledMinuteTakeoffOutgoing = minute;
        }

        // Incoming
        /// <summary>
        /// Hours & Minutes after scheduled landing to begin landing procedures
        /// By default this value is set to 0 hours 0 minutes
        /// This includes:
        /// - Finding Gate, Taxi and Runway
        /// - Simulating a landing.
        /// </summary>
        /// <param name="hour">Hour(s) after landing to begin</param>
        /// <param name="minute">Minute(s) after landing to begin</param>
        public void SetIncomingLandingTime(int hour, int minute)
        {
            this.ScheduledHourFindGateIncoming = hour;
            this.ScheduledMinuteFindGateIncoming = minute;
        }

        /// <summary>
        /// Hours & Minutes after scheduled landing to begin going from Runway
        /// to the taxiway.
        /// </summary>
        /// <param name="hour">Hour(s) after landing to begin</param>
        /// <param name="minute">Minute(s) after landing to begin</param>
        public void SetIncomingFinishedRunwayTime(int hour, int minute)
        {
            this.ScheduledHourLeaveRunwayIncoming = hour;
            this.ScheduledMinuteLeaveRunwayIncoming = minute;
        }
        /// <summary>
        /// Hours & Minutes after scheduled landing when the aircraft has 
        /// finished driving through the taxiway and begins offloading
        /// at the gate.
        /// </summary>
        /// <param name="hour">Hour(s) after landing to begin</param>
        /// <param name="minute">Minute(s) after landing to begin</param>
        public void SetIncomingFinishedTaxiTime(int hour, int minute)
        {
            this.ScheduledHourParkAtGateIncoming = hour;
            this.ScheduledMinuteParkAtGateIncoming = minute;
        }
        /// <summary>
        /// Hours & Minutes after scheduled landing for the aircraft to
        /// finish offloading passengers and complete the flight.
        /// </summary>
        /// <param name="hour">Hour(s) after landing to begin</param>
        /// <param name="minute">Minute(s) after landing to begin</param>
        public void SetIncomingFinishedGateTime(int hour, int minute)
        {
            this.ScheduledHourCompletedDisembarkation = hour;
            this.ScheduledMinuteCompletedDisembarkation = minute;
        }

        /// <summary>
        /// Inside the FlightSim() method in flight.cs, events like when to begin onboarding and when to takeoff are linked
        /// to instance variables called hourX and minuteX.
        /// This method updates all hours and minutes relating to OUTGOING flights.
        /// If an event isn't working, for example when there are no available gates, this method will
        /// try to run all following events one minute later than originally scheduled.
        /// </summary>
        /// <param name="fromWhichHourAndMinute">Number from 1-5. Setting this to 1 means hour1, hour2, hour3, hour4, and hour 5 are updated.
        /// Setting this to 4 means that only hour4 and hour5 are affected.</param>
        /// <exception cref="Exception"></exception>
        private void UpdateOutgoingNewHoursAndMinutesFromSetPoint(int fromWhichHourAndMinute)
        {
            if (this.FlightDirection == FlightDirection.Outgoing)
            {
                if (fromWhichHourAndMinute == 1)
                {
                    if (this.ScheduledMinuteFindGateoutgoing == 0)
                    {
                        this.ScheduledHourFindGateOutgoing -= 1;
                        this.ScheduledMinuteFindGateoutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteFindGateoutgoing -= 1;
                    }

                    if (this.ScheduledMinuteParkAtGateOutgoing == 0)
                    {
                        this.ScheduledHourParkAtGateOutgoing -= 1;
                        this.ScheduledMinuteParkAtGateOutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteParkAtGateOutgoing -= 1;
                    }

                    if (this.ScheduledMinuteStartBoardingOutgoing == 0)
                    {
                        this.ScheduledHourStartBoardingOutgoing -= 1;
                        this.ScheduledMinuteStartBoardingOutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteStartBoardingOutgoing -= 1;
                    }

                    if (this.ScheduledMinuteDepartFromGateOutgoing == 0)
                    {
                        this.ScheduledHourDepartFromGateOutgoing -= 1;
                        this.ScheduledMinuteDepartFromGateOutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteTakeoffOutgoing -= 1;
                    }

                    if (this.ScheduledMinuteTakeoffOutgoing == 0)
                    {
                        this.ScheduledHourTakeoffOutgoing -= 1;
                        this.ScheduledMinuteTakeoffOutgoing = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteTakeoffOutgoing -= 1;
                    }

                }
                else if (fromWhichHourAndMinute == 2)
                {
                    if (this.ScheduledMinuteParkAtGateOutgoing == 0)
                    {
                        this.ScheduledHourParkAtGateOutgoing -= 1;
                        this.ScheduledMinuteParkAtGateOutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteParkAtGateOutgoing -= 1;
                    }

                    if (this.ScheduledMinuteStartBoardingOutgoing == 0)
                    {
                        this.ScheduledHourStartBoardingOutgoing -= 1;
                        this.ScheduledMinuteStartBoardingOutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteStartBoardingOutgoing -= 1;
                    }

                    if (this.ScheduledMinuteDepartFromGateOutgoing == 0)
                    {
                        this.ScheduledHourDepartFromGateOutgoing -= 1;
                        this.ScheduledMinuteDepartFromGateOutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteTakeoffOutgoing -= 1;
                    }

                    if (this.ScheduledMinuteTakeoffOutgoing == 0)
                    {
                        this.ScheduledHourTakeoffOutgoing -= 1;
                        this.ScheduledMinuteTakeoffOutgoing = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteTakeoffOutgoing -= 1;
                    }
                }

                else if (fromWhichHourAndMinute == 3)
                {
                    if (this.ScheduledMinuteStartBoardingOutgoing == 0)
                    {
                        this.ScheduledHourStartBoardingOutgoing -= 1;
                        this.ScheduledMinuteStartBoardingOutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteStartBoardingOutgoing -= 1;
                    }

                    if (this.ScheduledMinuteDepartFromGateOutgoing == 0)
                    {
                        this.ScheduledHourDepartFromGateOutgoing -= 1;
                        this.ScheduledMinuteDepartFromGateOutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteTakeoffOutgoing -= 1;
                    }

                    if (this.ScheduledMinuteTakeoffOutgoing == 0)
                    {
                        this.ScheduledHourTakeoffOutgoing -= 1;
                        this.ScheduledMinuteTakeoffOutgoing = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteTakeoffOutgoing -= 1;
                    }
                }
                else if (fromWhichHourAndMinute == 4)
                {
                    if (this.ScheduledMinuteDepartFromGateOutgoing == 0)
                    {
                        this.ScheduledHourDepartFromGateOutgoing -= 1;
                        this.ScheduledMinuteDepartFromGateOutgoing = 59;
                    }
                    else
                    {
                        this.ScheduledMinuteTakeoffOutgoing -= 1;
                    }

                    if (this.ScheduledMinuteTakeoffOutgoing == 0)
                    {
                        this.ScheduledHourTakeoffOutgoing -= 1;
                        this.ScheduledMinuteTakeoffOutgoing = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteTakeoffOutgoing -= 1;
                    }
                }
                else if (fromWhichHourAndMinute == 5)
                {
                    if (this.ScheduledMinuteTakeoffOutgoing == 0)
                    {
                        this.ScheduledHourTakeoffOutgoing -= 1;
                        this.ScheduledMinuteTakeoffOutgoing = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteTakeoffOutgoing -= 1;
                    }
                }
                else
                {
                    throw new InvalidScheduledTimeException($"\n\nException: You did not provide a valid number for UpdateOutgoingNewHoursAndMinutesFromSetPoint(). Please set fromWhichHourAndMinute to be a number from 1 to 5.\n");
                }
            }
            else
            {
                throw new ArgumentException($"\n\nException: Error setting time forwards. Flight is not set as Outgoing. This method is meant to only be used on Outgoing flights. Please set flight.FlightDirection as Outgoing and try again.\n");
            }
        }

        /// <summary>
        /// Inside the FlightSim() method in flight.cs, events like when to begin onboarding and when to takeoff are linked
        /// to instance variables called hourX and minuteX.
        /// This method updates all hours and minutes relating to INCOMING flights.
        /// If an event isn't working, for example when there are no available gates, this method will
        /// try to run all following events one minute later than originally scheduled.
        /// </summary>
        /// <param name="fromWhichHourAndMinute">Number from 6-9. Setting this to 6 means hour6, hour7, hour8, and hour 9 are updated.
        /// Setting this to 8 means that only hour8 and hour9 are affected.</param>
        /// <exception cref="Exception"></exception>
        private void UpdateIncomingNewHoursAndMinutesFromSetPoint(int fromWhichHourAndMinute)
        {
            if (this.FlightDirection == FlightDirection.Incoming)
            {
                if (fromWhichHourAndMinute == 6)
                {
                    if (this.ScheduledMinuteFindGateIncoming == 59)
                    {
                        this.ScheduledHourFindGateIncoming += 1;
                        this.ScheduledMinuteFindGateIncoming = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteFindGateIncoming += 1;
                    }

                    if (this.ScheduledMinuteLeaveRunwayIncoming == 59)
                    {
                        this.ScheduledHourLeaveRunwayIncoming += 1;
                        this.ScheduledMinuteLeaveRunwayIncoming = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteLeaveRunwayIncoming += 1;
                    }

                    if (this.ScheduledMinuteParkAtGateIncoming == 59)
                    {
                        this.ScheduledHourParkAtGateIncoming += 1;
                        this.ScheduledMinuteParkAtGateIncoming = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteParkAtGateIncoming += 1;
                    }

                    if (this.ScheduledMinuteCompletedDisembarkation == 59)
                    {
                        this.ScheduledHourCompletedDisembarkation += 1;
                        this.ScheduledMinuteCompletedDisembarkation = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteCompletedDisembarkation += 1;
                    }
                }
                else if (fromWhichHourAndMinute == 7)
                {
                    if (this.ScheduledMinuteLeaveRunwayIncoming == 59)
                    {
                        this.ScheduledHourLeaveRunwayIncoming += 1;
                        this.ScheduledMinuteLeaveRunwayIncoming = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteLeaveRunwayIncoming += 1;
                    }

                    if (this.ScheduledMinuteParkAtGateIncoming == 59)
                    {
                        this.ScheduledHourParkAtGateIncoming += 1;
                        this.ScheduledMinuteParkAtGateIncoming = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteParkAtGateIncoming += 1;
                    }

                    if (this.ScheduledMinuteCompletedDisembarkation == 59)
                    {
                        this.ScheduledHourCompletedDisembarkation += 1;
                        this.ScheduledMinuteCompletedDisembarkation = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteCompletedDisembarkation += 1;
                    }
                }
                else if (fromWhichHourAndMinute == 8)
                {
                    if (this.ScheduledMinuteParkAtGateIncoming == 59)
                    {
                        this.ScheduledHourParkAtGateIncoming += 1;
                        this.ScheduledMinuteParkAtGateIncoming = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteParkAtGateIncoming += 1;
                    }

                    if (this.ScheduledMinuteCompletedDisembarkation == 59)
                    {
                        this.ScheduledHourCompletedDisembarkation += 1;
                        this.ScheduledMinuteCompletedDisembarkation = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteCompletedDisembarkation += 1;
                    }
                }
                else if (fromWhichHourAndMinute == 9)
                {
                    if (this.ScheduledMinuteCompletedDisembarkation == 59)
                    {
                        this.ScheduledHourCompletedDisembarkation += 1;
                        this.ScheduledMinuteCompletedDisembarkation = 0;
                    }
                    else
                    {
                        this.ScheduledMinuteCompletedDisembarkation += 1;
                    }
                }
                else
                {
                    throw new Exception($"\n\nException: You did not provide a valid number for UpdateIncomingNewHoursAndMinutesFromSetPoint(). Please set fromWhichHourAndMinute to be a number from 6 to 9.\n");
                }
            }
            else
            {
                throw new Exception($"\n\nException: Error setting time forwards. Flight is not set as Incoming. This method is meant to only be used on Incoming flights. Please set flight.FlightDirection as Incoming and try again.\n");
            }
        }

        /// <summary>
        /// TODO: Slette denne, finn en bedre metode.
        /// This method forces a gate on the flight if it has not previously found a gate.
        /// This method should not exist, we should have another measure in place. However, due to time
        /// limitations I had to implement this now. Please fix and delete later, me. Thanks.
        /// </summary>
        /// <returns></returns>
        private Gate FindGateBackup()
        {
            bool foundTerminal = false;
            foreach (var terminal in CurrentAirport.AllTerminals)
            {
                if (terminal.IsInternational == this.IsInternational)
                {
                    foreach (var gate in terminal.ConnectedGates)
                    {
                        this.AssignedGate = gate;
                        return gate;
                    }
                }
                else
                {
                    return null;
                }
            }

            if (this.AssignedGate == null)
            {
                UpdateIncomingNewHoursAndMinutesFromSetPoint(6);
                return null;
            }
            else
            {
                return this.AssignedGate;
            }
        }

        //Metode for å assigne et plane til flighten
        //Loope gjennom listen med available planes og finne et plane med riktig lisens?
        //og assigne det objektet til variablen AssignedPlane
        public Plane AssignAvailablePlaneToFlight()
        {

            if (this.CurrentAirport.ListOfPlanes.Count == 0)
            {
                throw new InvalidInfrastructureException("There are no planes in this airport.");
            }
            //Går gjennom alle flyene som er laget
            
            else
            {
                foreach (var plane in this.CurrentAirport.ListOfPlanes)
                {
                    //Sjekker at flyet er riktig type, at det er ledig, og at det er på flyplassen
                    if ((plane.FlightType & this.FlightType) == this.FlightType && plane.PlaneIsAvailable == true && plane.CurrentAirport == this.CurrentAirport)
                    {
                        //Setter flyet til instansvariabel og gjør det utilgjengelig
                        //Dvs at vi må endre på metoden som vi kaller helt til slutt når et fly har landet for å kunne gjøre det tilgjegenlig igjen
                        plane.PlaneIsAvailable = false;
                        return plane;
                    }
                    else
                        throw new InvalidInfrastructureException("There are no available planes with the correct FlightType in this airport");
                }
            }
            throw new InvalidInfrastructureException("There are no available planes in this airport");
        }

        internal void FlightSim(Airport airport, TimeSimulation timeSimulation)
        {
            throw new NotImplementedException();
        }
    }//Slutt Flight klassen
}//Slutt namespace


