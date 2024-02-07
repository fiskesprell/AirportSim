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
    /// Defines the types of flights that can be scheduled within the airport simulation.
    /// </summary>
    /// <value>
    /// Commercial - Represents a commercial flight.
    /// Transport - Represents a transport flight.
    /// Personal - Represents a personal flight.
    /// Military - Represents a military flight.
    /// </value>
    [Flags]
    public enum FlightType
    {
        Commercial = 1,
        Transport = 2,
        Personal = 4,
        Military = 8,
    }
    /// <summary>
    /// Defines the direction of the flight as incoming, outgoing or other.
    /// </summary>
    /// <value>
    /// Incoming - Flight arriving at the airport.
    /// Outgoing - Flight departing from the airport.
    //∕ Other - Flight heading elsewhere, i.e. maintenance, storage hangar.
    </value>
    public enum Direction
    {
        Incoming,
        Outgoing,
        Other
    }

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
    public enum FlightStatus
    {
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
    /// <summary>
    /// Defines the frequency of a flight.
    /// </summary>
    /// <value>
    /// OneTime - Flight occurs only once.
    /// Daily - Flight occurs daily.
    /// Weekly - Flight occurs weekly.
    /// </value>
    public enum Frequency
    {
        OneTime,
        Daily,
        Weekly
    }

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
    /// </property>
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
        /// Creates a flight. Not the same as creating an aircraft. Needs a flightnumber, date and time of arrival/departure,
        /// direction of flight (incoming, outgoing or other) and an airport object (the airport it is either arriving to or departing from).
        /// </summary>
        /// <param name="number">Flight number. Commonly looks like "WN417".</param>
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

        /// <summary>
        /// Overload of a flight with additional parameters.
        /// </summary>
        /// <param name="number">Flight number. Commonly looks like "WN417".</param>
        /// <param name="destination">Name of Airport the flight is going to.</param>
        /// <param name="travelDay">The date of departure.</param>
        /// <param name="hour">The hour of the departure. Follows the 24-hour clock. Putting 18 here means the flight leaves at 6PM (18:XX).</param>
        /// <param name="minute">The minute of the departure. Putting 30 here means the flight will leave at XX:30.</param>
        /// <param name="direction">Either <c>Direction.Outgoing</c>, <c>Direction.Incoming</c> or <c>Direction.Other</c> </param>
        /// <param name="airport">The Airport to which the flight belongs.</param>
        /// <param name="isInternational">Indicates whether the flight is international.</param>
        /// <param name="flightType">Specifies the type of flight, as defined in the <c>FlightType</c> enum.</param>
        /// <param name="frequency">Defines the frequency of the flight, such as one-time, daily, or weekly, as specified in the <c>Frequency</c> enum.</param>
        /// <param name="company">The name of the company operating the flight.</param>
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
        public void FlightSim(Airport airport, TimeSimulation timeSimulation)
        {
            DateTime startSim = timeSimulation.GetStartDate();
            TimeSpan dayDifference = this.ScheduledDay - startSim;
            int adjustedTravelDay = dayDifference.Days;
            
            // ~~~~ Outgoing Flight ~~~~
            if (this.FlightDirection == Direction.Outgoing)
            {
                //Kalle på convertTime for å få riktig klokkeslett 1 time og 45 min "tilbake" i tid
                //Dessverre kan man ikke overskrive variabler så må lage nye variabler hver gang
                (int newHours1, int newMinutes1) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, 1, 45);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours1 && ElapsedMinutes == newMinutes1)
                {
                    //Logg flight BRA123 har fått gate {this.AssignedGate} tildelt. F.eks
                    Gate availableGate = FindAvailableGate();
                    Taxi taxi = FindTaxi();
                    taxi.AddToTaxiQueue(this);
                    IsTraveling = true;

                    
                }
                (int newHours2, int newMinutes2) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, 1, 30);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours2 && ElapsedMinutes == newMinutes2)
                {
                    ParkFlightAtGate(AssignedGate);
                    IsTraveling = false;
                }

                //Derfor blir det newHours1, newHours2, osv
                (int newHours3, int newMinutes3) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, 1, 0);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours3 && ElapsedMinutes == newMinutes3)
                {
                    StartDeparturePrep();
                }

                (int newHours4, int newMinutes4) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, 0, 15);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours4 && ElapsedMinutes == newMinutes4)
                {
                    StartDeparting();
                    Runway correctRunway = this.FindRunway();
                    Taxi correctTaxi = this.FindTaxi();
                    this.AssignedGate.transferFlightToTaxi(this);
                }

                (int newHours5, int newMinutes5) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, 0, 5);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours5 && ElapsedMinutes == newMinutes5)
                {
                    
                }

                (int newHours6, int newMinutes6) = ConvertTimeBackwards(ScheduledHour, ScheduledMinutes, 0, 0);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours6 && ElapsedMinutes == newMinutes6)
                {
                    if(DesiredRunway.GetFlightOnRunway() == this)
                    {
                        TakeoffFlight(DesiredRunway);
                    }
                }
            }

            // ~~~~ Incoming Flight ~~~~
            else if (this.FlightDirection == Direction.Incoming)
            {
                (int newHours1, int newMinutes1) = ConvertTimeForwards(ScheduledHour, ScheduledMinutes, 0, 0);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours1 && ElapsedMinutes == newMinutes1)
                {
                    NEWIncomingFlightPreperation();
                }

                (int newHours2, int newMinutes2) = ConvertTimeForwards(ScheduledHour, ScheduledMinutes, 0, 1);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours2 && ElapsedMinutes == newMinutes2)
                {
                    NEWIncomingFlightFromRunwayToTaxi();
                }

                (int newHours3, int newMinutes3) = ConvertTimeForwards(ScheduledHour, ScheduledMinutes, 0, 11);
                if (ElapsedDays == adjustedTravelDay && ElapsedHours == newHours3 && ElapsedMinutes == newMinutes3)
                {
                    NEWIncomingFlightFromTaxiToGate();
                }

                (int newHours4, int newMinutes4) = ConvertTimeForwards(ScheduledHour, ScheduledMinutes, 0, 41);
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
            
            
            if (this.Frequency == Frequency.OneTime && (this.Status == FlightStatus.Arrived || this.Status == FlightStatus.Departed))
            {
                airport.AddCompletedFlight(this);
                airport.RemoveCompletedFlightFromAllFlights(this);
            }
            
            if (this.Logging && !(this.HasLogged) && ElapsedDays == adjustedTravelDay + 1 && ElapsedHours == 0 && ElapsedMinutes == 0 && (this.Status == FlightStatus.Departed || this.Status == FlightStatus.Completed))
            {
                Console.WriteLine("\nThis is the eventlog for flight: " + this.GetFlightNumber());
                foreach(string log in LogHistory)
                {
                    Console.WriteLine(log);
                }
                this.HasLogged = true;
            }

            if (this.Frequency == Frequency.Daily && ( this.Status == FlightStatus.Departed || this.Status == FlightStatus.Completed ) && ElapsedHours == 1 && ElapsedMinutes == 0)
            {
                DateTime newDate = this.ScheduledDay.AddDays(1);
                SetScheduledDay(newDate);
                SetStatus(FlightStatus.OnTime);
                SetDesiredRunway(null);
                SetAssignedGate(null);
                SetDesiredTaxi(null);
                SetHasLogged(false);
                LogHistory.Clear();
                
            }

            if (this.Frequency == Frequency.Weekly && (this.Status == FlightStatus.Departed || this.Status == FlightStatus.Completed) && ElapsedHours == 1 && ElapsedMinutes == 0)
            {
                DateTime newDate = this.ScheduledDay.AddDays(7);
                SetScheduledDay(newDate);
                SetStatus(FlightStatus.OnTime);
                SetDesiredRunway(null);
                SetDesiredTaxi(null);
                SetAssignedGate(null);
                SetHasLogged(false);
                LogHistory.Clear();
            }
        }//Slutt flightSim


        ///<summary>
        /// Method
        ///</summary>
        public void TakeoffFlight(Runway runway)
        {
            runway.SetFlightOnRunway(this);
            //broom broom
            this.SetStatus(FlightStatus.Departed);
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
            
            runway.SetFlightOnRunway(null);
            runway.SetIsAvailable(true);
        }//Slutt takeoff

        public void LandFlight(Runway runway)
        {
            runway.SetFlightOnRunway(this);
            this.SetStatus(FlightStatus.Arrived);
            Console.WriteLine("Day: " + ElapsedDays + " -  at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has landed");
            DesiredTaxi.AddToTaxiQueue(this);
            runway.SetFlightOnRunway(null);
            runway.SetIsAvailable(true);
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
                gateToPark.SetCurrentHolder(this);

            }
            if (Logging && FlightDirection == Direction.Outgoing)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Flight {Number} parked at Gate: {gate.GetGateName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{newMinutes}.";
                    LogHistory.Add(logMessage2);
                }
                else
                {
                    string logMessage = $"Flight {Number} parked at Gate: {gate.GetGateName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                    LogHistory.Add(logMessage);
                }
                
            }
            

            if (ElapsedMinutes == 0)
            {
                string elapsedMinutes = "00";
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + elapsedMinutes + " flight " + this.Number + " has parked at at " + AssignedGate.GetGateName());
            }
            else
            {
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has parked at at " + AssignedGate.GetGateName());
            }
        }//Slutt parkGate

        /// <summary>
        /// This method will change the status of the flight. 
        /// </summary>
        public void SetStatus(FlightStatus status)
        {
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
            Status = status;
        }//Slutt changeStatus

        /// <summary>
        /// This method will loop through all terminals, then all the gates in that terminal, and find a gate that is available and have the correct licence
        /// </summary>
        public Gate FindAvailableGate()
        {
            FlightType flightType = this.GetFlightType();

            bool foundTerminal = false;
            foreach (var terminal in CurrentAirport.GetAllTerminals())
            {
                if (terminal.IsInternational == this.IsInternational)
                {
                    foundTerminal = true;
                    bool foundGateLicence = false;
                    foreach(var gate in terminal.GetConnectedGates())
                    {
                        if (gate.GetIsAvailable() == true && gate.CheckGateLicence(this) == true)
                        {
                            this.AssignedGate = gate;
                            gate.SetIsAvailable(false);
                            foundGateLicence = true;
                            if (Logging)
                            {
                                if (ElapsedMinutes == 0)
                                {
                                    string newMinutes = "00";
                                    string logMessage = $"Flight {Number} was assigned {gate.GetGateName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{newMinutes}.";
                                    this.LogHistory.Add(logMessage);
                                }
                                else
                                {
                                    string gateLog = $"Flight {Number} was assigned {gate.GetGateName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                                    this.LogHistory.Add(gateLog);
                                }
                                
                            }
                            Console.WriteLine("\nDay: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has found an available gate:  " + this.AssignedGate.GetGateName());
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
        public Taxi FindTaxi()
        {
            Taxi selectedTaxi = null;
            int minQueueLength = int.MaxValue;

            if (this.FlightDirection == Direction.Outgoing)
            {
                if (this.AssignedGate.GetConnectedTaxis().Count() == 0)
                {
                    throw new Exception("This gate is not connected to any taxis. Please make a new taxi and connect it, or connect an existing taxi");
                }
                // For outgoing flights, consider taxiways connected to the assigned gate
                if (this.AssignedGate != null && AssignedGate.GetConnectedTaxis() != null)
                {
                    foreach (Taxi taxi in AssignedGate.GetConnectedTaxis())
                    {
                        if (taxi.IsAvailable && taxi.TaxiQueue.Count < minQueueLength)
                        {
                            selectedTaxi = taxi;
                            minQueueLength = taxi.TaxiQueue.Count;
                        }
                        Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has been assigned " + selectedTaxi.GetTaxiName());
                        this.DesiredTaxi = selectedTaxi;
                        if (Logging && FlightDirection == Direction.Outgoing)
                        {
                            if (ElapsedMinutes == 0)
                            {
                                string newMinutes = "00";
                                string logMessage = $"Flight {Number} was assigned {selectedTaxi.GetTaxiName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{newMinutes}.";
                                this.LogHistory.Add(logMessage);
                            }
                            else
                            {
                                string taxiLog = $"Flight {Number} was assigned {selectedTaxi.GetTaxiName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
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
                    FindAvailableGate();
                }

                foreach (Taxi taxi in CurrentAirport.GetAllTaxis())
                {
                    if (taxi.IsAvailable && taxi.TaxiQueue.Count < minQueueLength)
                    {
                        selectedTaxi = taxi;
                        minQueueLength = taxi.TaxiQueue.Count;
                    }
                    Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has been assigned " + selectedTaxi.GetTaxiName());
                    this.DesiredTaxi = selectedTaxi;
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

            if (this.FlightDirection == Direction.Outgoing)
            {

                foreach (Runway runway in DesiredTaxi.GetConnectedRunways())
                {
                    if (runway.RunwayQueue.Count < minQueueLength)
                    {
                        selectedRunway = runway;
                        minQueueLength = runway.RunwayQueue.Count;
                    }
                }
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has been assigned " + selectedRunway.GetRunwayName());
                DesiredRunway = selectedRunway;
                if (Logging)
                {
                    if (ElapsedMinutes == 0)
                    {
                        string newMinutes = "00";
                        string logMessage = $"Flight {Number} was assigned {selectedRunway.GetRunwayName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{newMinutes}.";
                        this.LogHistory.Add(logMessage);
                    }
                    else
                    {
                        string runwayLog = $"Flight {Number} was assigned {selectedRunway.GetRunwayName()} at Day: {ElapsedDays}, Time: {ElapsedHours}:{ElapsedMinutes}.";
                        this.LogHistory.Add(runwayLog);
                    }
                    
                }
                return selectedRunway;
            }
            else if (this.FlightDirection == Direction.Incoming)
            {
                foreach (Runway runway in DesiredTaxi.GetConnectedRunways())
                {
                    if (runway.RunwayQueue.Count < minQueueLength)
                    {
                        selectedRunway = runway;
                        minQueueLength = runway.RunwayQueue.Count;
                    }
                }
                Console.WriteLine("Day: " + ElapsedDays + " - at: " + ElapsedHours + ":" + ElapsedMinutes + " flight " + this.Number + " has been assigned " + selectedRunway.GetRunwayName());
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
        /// Get method for the FlightStatus of a flight
        /// </summary>
        public FlightStatus GetStatus()
        {

            return this.Status;
        }

        /// <summary>
        /// Get method for AssignedGate. This will return a gate object
        /// </summary>
        public Gate GetAssignedGate()
        {
            return AssignedGate;
        }

        public void SetAssignedGate(Gate gate)
        {
            AssignedGate = gate;
        }

        /// <summary>
        /// Get method IsInternational. This will return a bool value
        /// </summary>
        public bool GetIsInternational()
        {
            return this.IsInternational;
        }

        public Direction GetDirection()
        {
            return this.FlightDirection;
        }

        public void SetDesiredTaxi(Taxi taxi)
        {
            DesiredTaxi = taxi;
        }

        public Taxi GetDesiredTaxi()
        {
            return DesiredTaxi;
        }

        public void SetDesiredRunway(Runway runway)
        {
            DesiredRunway = runway;
        }

        public Runway GetDesiredRunway()
        {
            return DesiredRunway;
        }

        public Frequency GetFlightFrequency()
        {
            return this.Frequency;
        }

        public void SetFlightFrequency(Frequency frequency)
        {
            this.Frequency = frequency;
        }

        public void SetCompany(string name)
        {
            this.Company = name;
        }

        public string GetCompany()
        {
            return this.Company;
        }

        public void SetFlightType(FlightType flightType)
        {
            this.FlightType = flightType;
        }

        public FlightType GetFlightType()
        {
            return this.FlightType;
        }

        public void StartDeparturePrep()
        {
            this.SetStatus(FlightStatus.Boarding);
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

        public void StartDeparting()
        {
            this.SetStatus(FlightStatus.Departing);
            this.AssignedGate.SetCurrentHolder(null);
            this.AssignedGate.SetIsAvailable(true);
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

        public bool GetIsTraveling()
        {
            return this.IsTraveling;
        }

        public void SetIsTraveling(bool isTraveling)
        {
            this.IsTraveling = isTraveling;
        }

        public string GetFlightNumber()
        {
            return this.Number;
        }

        public void SetLogging(bool logging)
        {
            this.Logging = logging;
        }

        public List<string> GetLogHistory()
        {
            return this.LogHistory;
        }

        public void SetScheduledDay(DateTime scheduledDay)
        {
            this.ScheduledDay = scheduledDay;
        }

        public void SetHasLogged(bool hasLogged)
        {
            this.HasLogged = hasLogged;
        }

        public void IncomingFlightFromGateToComplete()
        {
            this.AssignedGate.SetIsAvailable(true);
            this.AssignedGate.SetCurrentHolder(null);
            this.SetStatus(FlightStatus.Completed);
            this.DesiredRunway = null;
            this.DesiredTaxi = null;
            this.AssignedGate = null;
            if (Logging)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:00 Flight {Number} has offloaded all passengers and is complete.";
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
                    string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:{newElapsedMinutes} Flight {Number} has offloaded all passengers and has completed.";
                    LogHistory.Add(logMessage2);
                }
            }
        }

        public void NEWIncomingFlightPreperation()
        {
            // Dette finner Gate og setter this.AssignedGate = gate
            // Dette finner Taxi og setter this.DesiredTaxi = selectedTaxi;
            this.FindTaxi();
            // Dette finner Runway og setter this.DesiredRunway = desiredRunway
            this.DesiredRunway = FindRunway();
            this.SetStatus(FlightStatus.Landed);

            // Logging
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


        }

        public void NEWIncomingFlightFromRunwayToTaxi()
        {
            this.DesiredTaxi.AddToTaxiQueue(this);
            
            this.SetStatus(FlightStatus.OnWayToGate);

            // Logging
            if (Logging)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:00 flight {Number} exited the runway and is entering the taxiway {this.DesiredTaxi.TaxiName}";
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
                    string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:{newElapsedMinutes} flight {Number} exited the runway and is entering the taxiway {this.DesiredTaxi.TaxiName}";
                    LogHistory.Add(logMessage2);
                }
            }
        }

        public void NEWIncomingFlightFromTaxiToGate()
        {
            this.SetStatus(FlightStatus.Offloading);

            // Logging
            if (Logging)
            {
                if (ElapsedMinutes == 0)
                {
                    string newMinutes = "00";
                    string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:00 flight {Number} exited the taxiway and is offloading passangers at {this.AssignedGate.GetGateName()}";
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
                    string logMessage2 = $"Day {ElapsedDays} - at {ElapsedHours}:{newElapsedMinutes} flight {Number} exited the taxiway and is offloading passangers at {this.AssignedGate.GetGateName()}";
                    LogHistory.Add(logMessage2);
                }
            }


        }

        public DateTime GetScheduledDay()
        {
            return this.ScheduledDay;
        }

    }//Slutt Flight klassen
}//Slutt namespace


