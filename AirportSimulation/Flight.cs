using System;
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

            this.flightSim(airport);
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
        public void updateElapsedTime(Airport airport)
        {
            this.ElapsedDays = airport.ElapsedDays;
            this.ElapsedHours = airport.ElapsedHours;
            this.ElapsedMinutes = airport.ElapsedMinutes;
        }//Slutt updateElapsedTime


        /// <summary>
        /// This method keeps track of the chain of events during the simulation
        /// </summary>
        private void flightSim(Airport airport)
        {
            if (this.FlightDirection == Direction.Outgoing)
            {
                //Kalle på convertTime for å få riktig klokkeslett 1 time og 45 min "tilbake" i tid
                //Dessverre kan man ikke overskrive variabler så må lage nye variabler hver gang
                (int newHours1, int newMinutes1) = convertTime(ScheduledHour, ScheduledMinutes, 1, 45);
                if (ElapsedHours == newHours1 && ElapsedMinutes == newMinutes1)
                {
                    //Logg flight BRA123 har fått gate {this.AssignedGate} tildelt. F.eks
                    Gate availableGate = findAvailableGate();
                    parkGate(availableGate);
                }

                //Derfor blir det newHours1, newHours2, osv
                (int newHours2, int newMinutes2) = convertTime(ScheduledHour, ScheduledMinutes, 1, 0);
                if (ElapsedHours == newHours2 && ElapsedMinutes == newMinutes2)
                {
                    //this.AssignedGate.DepartingPreperations(this);
                }

                (int newHours3, int newMinutes3) = convertTime(ScheduledHour, ScheduledMinutes, 0, 30);
                if (ElapsedHours == newHours3 && ElapsedMinutes == newMinutes3)
                {
                    Taxi correctTaxi = this.findTaxi();
                    this.AssignedGate.transferFlightToTaxi(this);
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
            if (this.Frequency == Frequency.Daily)
            {
                DateTime newDate = this.ScheduledDay.AddDays(1);
                Flight dailyFlight = new Flight(this.Number, this.Destination, newDate, this.ScheduledHour, this.ScheduledMinutes, this.FlightDirection, this.CurrentAirport, this.IsInternational, this.FlightType, this.Frequency, this.Company);
                dailyFlight.setDesiredRunway(null);
                dailyFlight.setDesiredTaxi(null);
                dailyFlight.setAssignedGate(null);
            }

            if (this.Frequency == Frequency.Weekly)
            {
                DateTime newDate = this.ScheduledDay.AddDays(7);
                Flight weeklyFlight = new Flight(this.Number, this.Destination, newDate, this.ScheduledHour, this.ScheduledMinutes, this.FlightDirection, this.CurrentAirport, this.IsInternational, this.FlightType, this.Frequency, this.Company);
                weeklyFlight.setDesiredRunway(null);
                weeklyFlight.setDesiredTaxi(null);
                weeklyFlight.setAssignedGate(null);
            }
        }//Slutt flightSim

        public void takeoff(Runway runway)
        {
            runway.setFlightOnRunway(this);
            //broom broom
            this.Status = FlightStatus.Departed;
            Console.WriteLine("Nå har flight " + this.Number + " tatt av");
            runway.setFlightOnRunway(null);
            runway.setIsAvailable(true);
        }//Slutt takeoff

        public void land(Runway runway)
        {
            runway.setFlightOnRunway(this);
            this.Status = FlightStatus.Arrived;
            Console.WriteLine("Nå har flight " + this.Number + " landet");
            DesiredTaxi.addToQueue(this);
            runway.setFlightOnRunway(null);
            runway.setIsAvailable(true);
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

                
            Console.WriteLine("Nå har flight " + this.Number + " parkert");
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
                            Console.WriteLine("Nå har flight " + this.Number + "fått en gate");
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
                }
            }
            Console.WriteLine("Nå har flight " + this.Number + " fått en taxi");
            this.DesiredTaxi = selectedTaxi;
            return selectedTaxi;
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
            }
            Console.WriteLine("Nå har flight " + this.Number + " fått en rullebane");
            DesiredRunway = selectedRunway;
            return selectedRunway;
        }//Slutt findRunway




        // public void LandingPreperation()
        //{
            //Loope gjennom alle terminaler får å finne en med samme bool verdi
            //Loope gjennom alle gates i riktig terminale for å finne en ledig
            // AssignedGate = gate;
            //loope gjennom alle taxi som er connected med den gaten
            //loope gjennom alle rullebanene som er connected med de taxiene 
            //finne optimal løsning mtp kø
            //Sette flighten i rullebanekø

            // Tildele gate 20 min før landing
            // Må fortsatt lande selv om ingen gates er ledige.
            // Da står den heller og venter på gate når den er på bakken.

            // Steg 1: Finn 20 minutter før Arrival til å begynne prosessen
            // DateTime WhenToBeginLookingForGate = this.ArrivalTime;



            // Steg 2: Når det er 20 minutter til landing
            // 2.1 - Se om flight er innlands eller utlands
            // 2.2 - Finn alle terminaler som er innlands/utlands (samme som flight).
            // 2.3 - Gå gjennom alle terminaler, finn en som er ledig.
            // 2.4 - gå gjennom denne ledige terminalen, og finn en ledig gate med minst kø.

            // DateTime whenToPrepare = ArrivalTime;
            // whenToPrepare = whenToPrepare.AddMinutes(-20);
        // }//Slutt LandingPreperation

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
    }//Slutt Flight klassen
}//Slutt namespace


