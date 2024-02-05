using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AirportSimulation
{


    /// <summary>
    /// Defines the type of flight
    /// </summary>
    [Flags]
    public enum FlightType
    {
        /// <summary>
        /// Represents a commercial flight.
        /// </summary>
        Commercial = 1,
        /// <summary>
        /// Represents a transport flight.
        /// </summary>
        Transport = 2,
        /// <summary>
        /// Represents a personal flight.
        /// </summary>
        Personal = 4,
        /// <summary>
        /// Represents a military flight.
        /// </summary>
        Military = 8,
    }

    /// <summary>
    /// Defines the direction of a flight.
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Represents an incoming flight.
        /// </summary>
        Incoming,
        /// <summary>
        /// Represents an outgoing flight.
        /// </summary>
        Outgoing,
        /// <summary>
        /// Represents a flight heading elsewhere, i.e. maintenance, hangar.
        /// </summary>
        Other
    }

    /// <summary>
    /// Defines the status of a flight.
    /// </summary>
    public enum FlightStatus
    {
        /// <summary>
        /// Represents a flight that is on time.
        /// </summary>
        OnTime,
        /// <summary>
        /// Represents a delayed arrival.
        /// </summary>
        ArrivingDelayed,
        /// <summary>
        /// Represents a delayed departure.
        /// </summary>
        DepartingDelayed,
        /// <summary>
        /// Represents a flight boarding.
        /// </summary>
        Boarding,
        /// <summary>
        /// Represents a flight departing.
        /// </summary>
        Departing,
        /// <summary>
        /// Represents a flight that has departed.
        /// </summary>
        Departed,
        /// <summary>
        /// Represents a flight arriving.
        /// </summary>
        Arrived,
        /// <summary>
        /// Represents a flight that has landed.
        /// </summary>
        Landed,
        /// <summary>
        /// Represents a completed flight.
        /// </summary>
        Completed,
        OnWayToGate,
        Offloading,
    }

    /// <summary>
    /// Defines the frequency of a flight.
    /// </summary>
    public enum Frequency
    {
        /// <summary>
        /// Represents a flight scheduled one time only.
        /// </summary>
        OneTime,
        /// <summary>
        /// Represents a flight scheduled daily.
        /// </summary>
        Daily,
        /// <summary>
        /// Represents a flight scheduled weekly.
        /// </summary>
        Weekly
    }


    /// <summary>
    /// Represents a flight, including details such as flight number, company, type, and status.
    /// This class manages flight scheduling, gate/taxi/runway assignment, ELABORATE.
    /// </summary>
    /// <remarks>
    /// The class also supports logging of flight events.
    /// Default values are provided for properties like Company (Norwegian) and FlightType (Commercial) to enable simple instansiations of a simulation.
    /// </remarks>
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

        private bool HasLogged { get; set; } = false;

        private List<string> LogHistory { get; set; } = new List<string>();


        /// <summary>
        /// Creates a flight. Not the same as creating an aircraft. Requires a flight number, date and time of arrival/departure,
        /// direction of flight (incoming, outgoing, other) and an airport object (the airport it is either arriving to or departing from).
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

            // Attempts to parse the direction of the flight. Throws exception if it fails.
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

        }


        // Overload constructor
        /// <summary>
        /// Creates a flight. Not the same as creating an aircraft. Requires a flight number, date and time of arrival/departure,
        /// direction of flight (incoming, outgoing, other) and an airport object (the airport it is either arriving to or departing from).
        /// </summary>
        /// <param name="number">Flight number. Commonly looks like "WN 417".</param>
        /// <param name="destination">Name of Airport the flight is going to.</param>
        /// <param name="airport">Name of Airport the flight is departing from.</param>
        /// <param name="travelDay">The date of departure.</param>
        /// <param name="hour">The hour of the departure. Follows the 24-hour clock. Putting 18 here means the flight leaves at 6PM (18:XX).</param>
        /// <param name="minute">The minute of the departure. Putting 30 here means the flight will leave at XX:30.</param>
        /// <param name="flightType">Type of flight. Either <c>FlightType.Commercial</c>, <c>FlightType.Transport</c>, <c>FlightType.Personal</c> or <c>FlightType.Military</c></param>
        /// <param name="frequency">Frequency of the flight. Either <c>Frequency.OneTime</c>, <c>Frequency.Daily</c> or <c>Frequency.Weekly</c></param>
        /// <param name="company">Name of the airline operating the flight.</param>
        /// <param name="direction">Direction of the flight <c>Direction.Outgoing</c>, <c>Direction.Incoming</c> or <c>Direction.Other</c>. </param>
        /// <param name="isInternational">Denoted whether the flight is domestic or international. True if international, false is domestic. </param>
        /// <exception cref="ArgumentException"></exception>
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

        }

        /// <summary>
        /// This method will continously update the elapsed time for each flight object.
        /// </summary>
        public void updateElapsedTime(TimeSimulation timeSimulation)
        {
            this.ElapsedDays = timeSimulation.ElapsedDays;
            this.ElapsedHours = timeSimulation.ElapsedHours;
            this.ElapsedMinutes = timeSimulation.ElapsedMinutes;
        }


        /// <summary>
        /// This method keeps track of the chain of events during the simulation
        /// </summary>
        public void flightSim(Airport airport, TimeSimulation timeSimulation)
        {
            DateTime startSim = timeSimulation.getStartDate();
            TimeSpan dayDifference = this.ScheduledDay - startSim;
            int adjustedTravelDay = dayDifference.Days;
            
            // ~~~~ Outgoing Flight ~~~~
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
                    startDeparturePrep();
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

            // ~~~~ Incoming Flight ~~~~
            else if (this.FlightDirection == Direction.Incoming)
            {
                (int newHours1, int newMinutes1) = convertTime(ScheduledHour, ScheduledMinutes, 0, 0);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours1 && ElapsedMinutes == newMinutes1)
                {
                    IncomingFlightPreperation(airport);
                }

                (int newHours2, int newMinutes2) = convertTime(ScheduledHour, ScheduledMinutes, 0, -1);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours1 && ElapsedMinutes == newMinutes1)
                {
                    IncomingFlightFromRunwayToTaxi();
                }

                (int newHours3, int newMinutes3) = convertTime(ScheduledHour, ScheduledMinutes, 0, -11);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours1 && ElapsedMinutes == newMinutes1)
                {
                    IncomingFlightFromTaxiToGate();
                }

                (int newHours4, int newMinutes4) = convertTime(ScheduledHour, ScheduledMinutes, 0, -41);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours1 && ElapsedMinutes == newMinutes1)
                {
                    IncomingFlightFromGateToComplete();
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
            
            if (this.Logging && !(this.HasLogged) && ElapsedDays == adjustedTravelDay + 1 && ElapsedHours == 0 && ElapsedMinutes == 0 && (this.Status == FlightStatus.Departed || this.Status == FlightStatus.Completed))
            {
                Console.WriteLine("\nThis is the eventlog for flight: " + this.getFlightNumber());
                foreach(string log in LogHistory)
                {
                    Console.WriteLine(log);
                }
                this.HasLogged = true;
            }

            if (this.Frequency == Frequency.Daily && this.Status == FlightStatus.Departed && ElapsedHours == 1 && ElapsedMinutes == 0)
            {
                DateTime newDate = this.ScheduledDay.AddDays(1);
                setScheduledDay(newDate);
                setStatus(FlightStatus.OnTime);
                setDesiredRunway(null);
                setAssignedGate(null);
                setDesiredTaxi(null);
                setHasLogged(false);
                LogHistory.Clear();
                
            }

            if (this.Frequency == Frequency.Weekly && this.Status == FlightStatus.Departed && ElapsedHours == 1 && ElapsedMinutes == 0)
            {
                DateTime newDate = this.ScheduledDay.AddDays(7);
                setScheduledDay(newDate);
                setStatus(FlightStatus.OnTime);
                setDesiredRunway(null);
                setDesiredTaxi(null);
                setAssignedGate(null);
                setHasLogged(false);
                LogHistory.Clear();
            }
        }//Slutt flightSim


        /// <summary>This method handles takeoff from an assigned runway.</summary>
        public void takeoff(Runway runway)
        {
            runway.setFlightOnRunway(this);
            //broom broom
            this.Status = FlightStatus.Departed;
            if (ElapsedMinutes == 0)
            {
                string elapsedMinutes = "00";
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + elapsedMinutes + " flight " + this.Number + " has taken off\n");
            }
            else
            {
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has taken off\n");
            }
            
            if (Logging)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Flight {Number} took off at Day: {newMinutes}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                    LogHistory.Add(logMessage2);
                }
                else
                {
                    string logMessage = $"Flight {Number} took off at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                    LogHistory.Add(logMessage);
                }
                
            }
                
            runway.setFlightOnRunway(null);
            runway.setIsAvailable(true);
        }//Slutt takeoff


        /// <summary>This method handles landing on an assigned runway.</summary>
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
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Flight {Number} landed at day: {newMinutes}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                    LogHistory.Add(logMessage2);
                }
                else
                {
                    string landing = $"Flight {Number} landed at day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                    this.LogHistory.Add(landing);
                }
                
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
            if (Logging)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Flight {Number} parked at Gate: {gate.getGateName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{newMinutes}.";
                    LogHistory.Add(logMessage2);
                }
                else
                {
                    string logMessage = $"Flight {Number} parked at Gate: {gate.getGateName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                    LogHistory.Add(logMessage);
                }
                
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
        /// Sets the status of the flight.
        /// </summary>
        public void setStatus(FlightStatus status)
        {
            Status = status;
            if (Logging)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Flight {Number} changed its status to: {status} at Day: {ElapsedDays}, Time: {ElapsedHours}:{newMinutes}.";
                    LogHistory.Add(logMessage2);
                }
                else
                {
                    string statusChange = $"Flight {Number} changed its status to: {status} at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                    this.LogHistory.Add(statusChange);
                }

            }
        }//Slutt changeStatus

        /// <summary>
        /// This method will loop through all terminals and gates therein, and find a gate that is available and have the correct licence
        /// </summary>
        public Gate findAvailableGate()
        {
            FlightType flightType = this.getFlightType();

            bool foundTerminal = false;
            foreach (var terminal in CurrentAirport.getAllTerminals())
            {
                if (terminal.IsInternational == this.IsInternational)
                {
                    foundTerminal = true;
                    bool foundGateLicence = false;
                    foreach(var gate in terminal.getConnectedGates())
                    {
                        if (gate.getIsAvailable() == true && gate.checkGateLicence(this) == true)
                        {
                            this.AssignedGate = gate;
                            gate.setIsAvailable(false);
                            foundGateLicence = true;
                            if (Logging)
                            {
                                if (ElapsedMinutes == 0)
                                {
                                    string newMinutes = "00";
                                    string logMessage = $"Flight {Number} was assigned {gate.getGateName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{newMinutes}.";
                                    this.LogHistory.Add(logMessage);
                                }
                                else
                                {
                                    string gateLog = $"Flight {Number} was assigned {gate.getGateName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                                    this.LogHistory.Add(gateLog);
                                }
                                
                            }
                            Console.WriteLine("\nDay: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has found an available gate:  " + this.AssignedGate.getGateName());
                            return gate;
                        }
                    }
                    if (!foundGateLicence)
                    {
                        throw new Exception($"\n\nException: You tried to simulate time for a flight with the FlightType: {this.FlightType}. There are no gates that have a licence for the assigned FlightType. Try adding more gates and add a licence for that FlightType or edit existing gates with addLicence()\n");
                    }
                }
                
            }
            if (!foundTerminal)
            {
                if (this.IsInternational)
                {
                    string international = "International";
                    throw new Exception($"\n\nException: There are no terminals on this airport that accepts {international} flights. Try configuring your terminal(s) with setIsInternational(true) or add a terminal with the correct configuration\n");
                }
                else
                {
                    string international = "Domestic";
                    throw new Exception($"\n\nException: There are no terminals on this airport that accepts {international} flights. Try configuring your terminal(s) with setIsInternational(false) or add a terminal with the correct configuration\n");
                }
            }

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
                        if (Logging)
                        {
                            if (ElapsedMinutes == 0)
                            {
                                string newMinutes = "00";
                                string logMessage = $"Flight {Number} was assigned {selectedTaxi.getName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{newMinutes}.";
                                this.LogHistory.Add(logMessage);
                            }
                            else
                            {
                                string taxiLog = $"Flight {Number} was assigned {selectedTaxi.getName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                                this.LogHistory.Add(taxiLog);
                            }
                            
                        }
                        return selectedTaxi;
                    }
                    
                }
            }

            else if (this.FlightDirection == Direction.Incoming)
            {
                // For incoming flights, a different selection strategy is needed
                // This could involve selecting from a global list of taxiways, for example
                if (this.AssignedGate == null)
                {
                    findAvailableGate();
                }

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
                if (Logging)
                {
                    if (ElapsedMinutes == 0)
                    {
                        string newMinutes = "00";
                        string logMessage = $"Flight {Number} was assigned {selectedRunway.getRunwayName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{newMinutes}.";
                        this.LogHistory.Add(logMessage);
                    }
                    else
                    {
                        string runwayLog = $"Flight {Number} was assigned {selectedRunway.getRunwayName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                        this.LogHistory.Add(runwayLog);
                    }
                    
                }
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

        public FlightType getFlightType()
        {
            return this.FlightType;
        }

        public void startDeparturePrep()
        {
            this.Status = FlightStatus.Boarding;

            if (Logging)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Flight {Number} started preparing for departure at day: {ElapsedDays}, time: {ElapsedHours}:{newMinutes}";
                    LogHistory.Add(logMessage2);
                }
                else
                {
                    string logMessage = $"Flight {Number} started preparing for departure at day: {ElapsedDays}, time: {ElapsedHours}:{ElapsedMinutes}";
                    LogHistory.Add(logMessage);
                }
                
            }
            

        }   
        //Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " started boarding");

        public void startDeparting()
        {
            this.Status = FlightStatus.Departing;
            this.AssignedGate.setCurrentHolder(null);
            this.AssignedGate.setIsAvailable(true);
            this.IsParked = false;
            this.IsTraveling = true;

            if (Logging)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Flight {Number} left the gate and started towards its runway at day: {ElapsedDays}, time: {ElapsedHours}:{newMinutes}";
                    LogHistory.Add(logMessage2);
                }
                else
                {
                    string logMessage2 = $"Flight {Number} left the gate and started towards its runway at day: {ElapsedDays}, time: {ElapsedHours}:{ElapsedMinutes}";
                    LogHistory.Add(logMessage2);
                }
            }
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

        public List<string> getLogHistory()
        {
            return this.LogHistory;
        }

        public void setScheduledDay(DateTime scheduledDay)
        {
            this.ScheduledDay = scheduledDay;
        }

        public void setHasLogged(bool hasLogged)
        {
            this.HasLogged = hasLogged;
        }

        public void IncomingFlightPreperation(Airport airport)
        {
            foreach (Runway runway in airport.AllRunways)
            {
                if (runway.IsAvailable == true && runway.FlightOnRunway == null)
                {
                    // TODO:
                    // - Legge til håndtering av hva som skjer hvis det ikke er en runway med isAvailable = true & flightOnRunway = null
                    this.DesiredRunway = runway;
                    this.DesiredRunway.RunwayQueue.Enqueue(this);
                    this.Status = FlightStatus.Landed;
                    
                    if (Logging)
                    {
                        if (ElapsedMinutes == 0)
                        {
                            string newMinutes = "00";
                            string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:00 flight {Number} landed on runway {this.DesiredRunway.RunwayName}";
                            LogHistory.Add(logMessage2);
                        }
                        else
                        {
                            string newElapsedMinutes = "";
                            if (ElapsedMinutes < 10)
                            {
                                newElapsedMinutes = $"0{ElapsedMinutes}";
                            }
                            else 
                            {
                                newElapsedMinutes = $"{ElapsedMinutes}";
                            }
                            string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:{newElapsedMinutes} flight {Number} landed on runway {this.DesiredRunway.RunwayName}";
                            LogHistory.Add(logMessage2);
                        }
                    }

                    break;
                }
            }
        }

        public void IncomingFlightFromRunwayToTaxi()
        {
            foreach (Taxi taxi in this.DesiredRunway.ConnectedTaxi)
            {
                if (taxi.IsAvailable == true)
                {
                    // TODO:
                    // - Legge til håndtering av hva som skjer hvis det ikke er en TAXI med isAvailable = true
                    this.DesiredTaxi = taxi;
                    // addToQueue needs AssignedGate
                    foreach (Gate gate in this.DesiredTaxi.ConnectedGates)
                    {
                        // TODO:
                        // - Legge til håndtering av hva som skjer hvis det ikke er en GATE med isAvailable = true
                        if (gate.checkGateLicence(this) == true && gate.getIsAvailable() == true)
                        {
                            this.AssignedGate = gate;
                            break;

                        }
                    }

                    taxi.addToQueue(this);
                    this.DesiredRunway.RunwayQueue.Dequeue();
                    this.DesiredRunway = null;
                    this.Status = FlightStatus.OnWayToGate;

                    if (Logging)
                    {
                        if (ElapsedMinutes == 0)
                        {
                            string newMinutes = "00";
                            string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:00 flight {Number} exited the runway and is entering the taxiway {this.DesiredTaxi.Name}";
                            LogHistory.Add(logMessage2);
                        }
                        else
                        {
                            string newElapsedMinutes = "";
                            if (ElapsedMinutes < 10)
                            {
                                newElapsedMinutes = $"0{ElapsedMinutes}";
                            }
                            else
                            {
                                newElapsedMinutes = $"{ElapsedMinutes}";
                            }
                            string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:{ElapsedMinutes} flight {Number} exited the runway and is entering the taxiway {this.DesiredTaxi.Name}";
                            LogHistory.Add(logMessage2);
                        }
                    }

                    break;
                }
            }
        }

        public void IncomingFlightFromTaxiToGate()
        {
            if (this.AssignedGate == null)
            {
                foreach (Gate gate in this.DesiredTaxi.ConnectedGates)
                {
                    // TODO:
                    // - Legge til håndtering av hva som skjer hvis det ikke er en GATE med isAvailable = true
                    if (gate.checkGateLicence(this) == true);
                    // && gate.getIsAvailable() == false);
                    {
                        this.AssignedGate = gate;
                        break;

                    }
                }
            }


            if (Logging)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:00 flight {Number} exited the taxiway and is offloading passangers at {this.AssignedGate.getGateName()}";
                    LogHistory.Add(logMessage2);
                }
                else
                {
                    string newElapsedMinutes = "";
                    if (ElapsedMinutes < 10)
                    {
                        newElapsedMinutes = $"0{ElapsedMinutes}";
                    }
                    else
                    {
                        newElapsedMinutes = $"{ElapsedMinutes}";
                    }
                    string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:{ElapsedMinutes} flight {Number} exited the taxiway and is offloading passangers at {this.AssignedGate.getGateName()}";
                    LogHistory.Add(logMessage2);
                }
            }

            this.AssignedGate.setIsAvailable(false);
            this.AssignedGate.setCurrentHolder(this);

            this.Status = FlightStatus.Offloading;
            this.DesiredTaxi.removeFromQueue();
            this.DesiredTaxi = null;
        }

        public void IncomingFlightFromGateToComplete()
        {
            this.AssignedGate.setIsAvailable(true);
            this.AssignedGate.setCurrentHolder(null);
            this.AssignedGate = null;
            this.Status = FlightStatus.Completed;
        }

    }//Slutt Flight klassen
}//Slutt namespace


