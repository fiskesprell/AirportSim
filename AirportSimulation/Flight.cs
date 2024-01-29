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
        private Runway AssignedRunway { get; set; }
        private bool IsInternational { get; set; } = false;
        private int ScheduledHour { get; set; } = 0;
        private int ScheduledMinute { get; set; } = 0;
        private string Destination { get; set; }
        private DateTime LastMaintanace { get; set; }
        private FlightStatus Status { get; set; } = FlightStatus.OnTime;
        private Frequency Frequency { get; set; } = Frequency.OneTime;
        private Direction FlightDirection;
        public int ElapsedDays = 0;
        public int ElapsedHours = 0;
        public int ElapsedMinutes = 0;
        private Airport CurrentAirport = null;


        public Flight(string number, string destination, int hour, int minute, Direction direction, Airport airport)
        {
            this.Number = number;
            this.Destination = destination;
            this.CurrentAirport = airport;

            //Hvis de sender inn noe som ikke er en av kategoriene i Direction enumen så vil en exception kastes
            if (Enum.TryParse(directionString, out Direction direction))
            {
                this.FlightDirection = direction;

                if (this.FlightDirection == Outgoing)
                {
                    this.ScheduledHour = hour;
                    this.ScheduledMinute = minute;
                }
                else
                {
                    this.ScheduledHour = hour;
                    this.ScheduledMinute = minute;
                }
            }
            else
            {
                throw new ArgumentException($"Invalid direction: {directionString}. Expected values are {string.Join(", ", Enum.GetNames(typeof(Direction)))}.", nameof(directionString));
            }
            Console.WriteLine("Nå er en flight opprettet");

            this.flightsim(airport);
        }//Slutt konstruktør

        public void updateElapsedTime(Airport airport)
        {
            this.ElapsedDays = airport.ElapsedDays;
            this.ElapsedHours = airport.ElapsedHours;
            this.ElapsedMinutes = airport.ElapsedMinutes;
        }//Slutt updateElapsedTime

        private void flightSim(Airport airport)
        {
            if (this.Status == Outgoing)
            {
                //Jeg vet at disse tidssammenligningene ikke vil funke, men bare en kjapp draft så jeg ikke glemmer
                //TODO: Fikse tidssammenligning så den faktisk fungerer
                if (elapsedHours == ScheduledHour - 1 && elapsedMinutes == ScheduledMinutes - 45)
                {
                    //Logg flight BRA123 har fått gate {this.AssignedGate} tildelt. F.eks
                    Gate availableGate = findAvailableGate();
                    ParkGate(availableGate);
                }

                if (elapsedHours == ScheduledHour - 1 && elapsedMinutes == ScheduledMinutes)
                {
                    gate.DepartingPreperations(this);
                }

                if (elapsedHours == ScheduledHour && elapsedMinutes == ScheduledMinutes - 30)
                {
                    gate.DepartFlightFromGate(this);
                }
            }
            else (this.Status == Incoming)
            {
                if (elapsedHours == ScheduledHour && elapsedMinutes == ScheduledMinutes - 20)
                {
                    Gate availableGate = this.findAvailableGate();
                    Runway bestRunway = this.findOptimalRunway();

                }
            }
        }//Slutt flightSim

        public void takeoff()
        {
            //Simuler f.eks 2 min
            //Sett staus til Departed
            //Sett rullebane til ledig
            this.Status = FlightStatus.Departed;
            Console.WriteLine("Nå har flight " + this.Number + "tatt av");
        }//Slutt takeoff

        public void land()
        {
            this.Status = FlightStatus.Arrived;
            Console.WriteLine("Nå har flight " + this.Number + "landet");
            //Sett rullebane til opptatt
            //Simuler f.eks. 2 min
            //Sett status til Arrived
            //Finn den taxi som er connected til denne rullebanen som også er connected til den gaten flighten har fått assigned
        }//Slutt Land

        public void parkGate()
        {
            //Parkere ved gate
        }//Slutt ParkGate

        public void changeStatus(FlightStatus status)
        {
            Status = status;
        }//Slutt changeStatus

        public Gate findAvailableGate()
        {
            //Gpr gjennom alle terminalene på flyplassen
            foreach (var terminal in CurrentAirport.allTerminals)
            {
                //Finner en som er samme bool verdi som flighten
                if (terminal.IsInternational == this.IsInternational)
                {
                    //Går gjennom alle gatene på den terminalen
                    foreach (var gate in terminal.connectedGates)
                    {
                        //Finner en som er ledig
                        if (gate.IsAvailable == true)
                        {
                            //Returnerer den første den finner
                            this.AssignedGate = gate;
                            Console.WriteLine("Nå har flight " + this.Number + "funnet gate " + this.AssignedGate);
                            return gate;
                        }
                    }
                }
            }
            //Hvis den ikke finner en ledig gate før den lander så returneres null og den må lete igjen når den står i kø
            return null;
        }//Slutt findAvailableGate

        private Runway findRunway()
        {
            Gate gate = this.AssignedGate;
            foreach(var taxi in gate.connectedTaxi)
            {

            }
        }

        public void landingPreperation()
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

        private void arrivalProcessing()
        {
            //Denne metoden tar 45 min
            //Det er at passasjerer går av, baggasje blir tatt ut
        }

    }//Slutt Flight klasse
}//Slutt namespace

