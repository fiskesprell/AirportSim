using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AirportSimulation
{
    enum FlightType
    {
        Commercial,
        Transport,
        Personal,
        Military
    }

    enum Direction
    {
        Incoming,
        Outgoing
    }

    enum FlightStatus
    {
        OnTime,
        ArrivingDelayed,
        DepartingDelayed,
        Boarding,
        Departing,
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
        private int ScheduledHour { get; set; } = 0;
        private int ScheduledMinutes { get; set; } = 0;
        private string Destination { get; set; }
        private DateTime LastMaintanace { get; set; }
        private FlightStatus Status { get; set; } = FlightStatus.OnTime;
        private Frequency Frequency { get; set; } = Frequency.OneTime;
        private Direction FlightDirection;
        public int ElapsedDays = 0;
        public int ElapsedHours = 0;
        public int ElapsedMinutes = 0;
        private Airport currentAirport;
        private bool IsParked = false;



        public Flight(string number, string destination, int hour, int minute, Direction direction, Airport airport)
        {
            this.Number = number;
            this.Destination = destination;
            this.currentAirport = airport;

            //Hvis de sender inn noe som ikke er en av kategoriene i Direction enumen så vil en exception kastes
            if (Enum.TryParse(direction.ToString(), out Direction flightDirection))
            {
                this.FlightDirection = direction;

                if (this.FlightDirection == Direction.Outgoing)
                {
                    this.ScheduledHour = hour;
                    this.ScheduledMinutes = minute;
                }
                else
                {
                    this.ScheduledHour = hour;
                    this.ScheduledMinutes = minute;
                }
            }
            else
            {
                throw new ArgumentException($"Invalid direction: {direction}. Expected values are {string.Join(", ", Enum.GetNames(typeof(Direction)))}.", nameof(directionString));
            }

            this.flightSim(airport);
        }//Slutt konstruktør

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
                //Jeg vet at disse tidssammenligningene ikke vil funke, men bare en kjapp draft så jeg ikke glemmer
                //TODO: Fikse tidssammenligning så den faktisk fungerer
                if (ElapsedHours == ScheduledHour - 1 && ElapsedMinutes == ScheduledMinutes - 45)
                {
                    //Logg flight BRA123 har fått gate {this.AssignedGate} tildelt. F.eks
                    Gate availableGate = findAvailableGate();
                    parkGate(availableGate);
                }

                if (ElapsedHours == ScheduledHour - 1 && ElapsedMinutes == ScheduledMinutes)
                {
                    this.AssignedGate.DepartingPreperations(this);
                }

                if (ElapsedHours == ScheduledHour && ElapsedMinutes == ScheduledMinutes - 30)
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

                }
            }
        }//Slutt flightSim

        public void takeoff()
        {
            //Simuler f.eks 2 min
            //Sett staus til Departed
            //Sett rullebane til ledig
        }//Slutt takeoff

        public void land()
        {
            //Sett rullebane til opptatt
            //Simuler f.eks. 2 min
            //Sett status til Arrived
            //Finn den taxi som er connected til denne rullebanen som også er connected til den gaten flighten har fått assigned
        }//Slutt land

        public void parkGate(Gate gate)
        {
            this.IsParked = true;
            Console.WriteLine("Nå har flight " + this.Number + " parkert");
        }//Slutt parkGate

        /// <summary>
        /// This method will change the status of the flight. 
        /// </summary>
        public void changeStatus(FlightStatus status)
        {
            Status = status;
        }//Slutt changeStatus

        /// <summary>
        /// This method will loop through all terminals, then all the gates in that terminal, and find a gate that is available and have the correct licence
        /// </summary>
        public Gate findAvailableGate()
        {
            //Loope gjennom alle connected gates til alle terminaler som har samme bool verdi på innland utland
            foreach (var terminal in currentAirport.getAllTerminals())
            {
                if (terminal.IsInternational == this.IsInternational)
                {
                    foreach(var gate in terminal.getConnectedGates())
                    {
                        if (gate.IsAvailable == true && this.FlightType in gate.getGateLicence)
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

                foreach (Taxi taxi in currentAirport.getAllTaxis())
                {
                    if (taxi.IsAvailable && taxi.TaxiQueue.Count < minQueueLength)
                    {
                        selectedTaxi = taxi;
                        minQueueLength = taxi.TaxiQueue.Count;
                    }
                }
            }
            Console.WriteLine("Nå har flight " + this.Number + " fått en taxi");
            return selectedTaxi;
        }//Slutt findTaxi

        /// <summary>
        /// This method will find a runway based on your gate. This will return a runway object
        /// </summary>
        public Runway findRunway()
        {
            Runway selectedRunway = null;
            int minQueueLength = int.MaxValue;

            if (this.FlightDirection == Direction.Outgoing)
            {
                // For outgoing flights, consider runways suitable for take-off
                foreach (Runway runway in currentAirport.getAllRunways())
                {
                    //Hva er IsAvailableForTakeoff? Hva gjør den?
                    if (runway.IsAvailableForTakeoff && runway.RunwayQueue.Count < minQueueLength)
                    {
                        selectedRunway = runway;
                        minQueueLength = runway.RunwayQueue.Count;
                    }
                }
            }
            else if (this.FlightDirection == Direction.Incoming)
            {
                // For incoming flights, consider runways suitable for landing
                foreach (Runway runway in currentAirport.getAllRunways())
                {
                    //Hva er IsAvailableForLanding?
                    if (runway.IsAvailableForLanding && runway.RunwayQueue.Count < minQueueLength)
                    {
                        selectedRunway = runway;
                        minQueueLength = runway.RunwayQueue.Count;
                    }
                }
            }
            Console.WriteLine("Nå har flight " + this.Number + " fått en rullebane");
            return selectedRunway;
        }//Slutt findRunway


        public void LandingPreperation()
        {
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
            DateTime WhenToBeginLookingForGate = this.ArrivalTime;



            // Steg 2: Når det er 20 minutter til landing
            // 2.1 - Se om flight er innlands eller utlands
            // 2.2 - Finn alle terminaler som er innlands/utlands (samme som flight).
            // 2.3 - Gå gjennom alle terminaler, finn en som er ledig.
            // 2.4 - gå gjennom denne ledige terminalen, og finn en ledig gate med minst kø.

            DateTime whenToPrepare = ArrivalTime;
            whenToPrepare = whenToPrepare.AddMinutes(-20);
        }//Slutt LandingPreperation


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

        /// <summary>
        /// Get method IsInternational. This will return a bool value
        /// </summary>
        public bool getIsInternational()
        {
            return IsInternational;
        }

    }//Slutt Flight klassen
}//Slutt namespace


