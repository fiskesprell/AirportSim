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
        private int DepartureTimeHour { get; set; } = 0;
        private int DepartureTimeMinute { get; set; } = 0;
        private int ArrivalTimeHour { get; set; } = 0;
        private int ArrivalTimeMinute { get; set; } = 0;
        private string Destination { get; set; }
        private DateTime LastMaintanace { get; set; }
        private FlightStatus Status { get; set;} = FlightStatus.OnTime;
        private Frequency Frequency { get; set; } = Frequency.OneTime;
        private Direction FlightDirection;
        public int ElapsedDays = 0;
        public int ElapsedHours = 0;
        public int ElapsedMinutes = 0;


    }

    public Flight(string number, string destination, int hour, int minute, Direction direction, Airport airport)
    {
        this.Number = number;
        this.Destination = destination;

        //Hvis de sender inn noe som ikke er en av kategoriene i Direction enumen så vil en exception kastes
        if (Enum.TryParse(directionString, out Direction direction))
        {
            this.FlightDirection = direction;

            if (this.FlightDirection == Outgoing)
            {
                this.DepartureTimeHour = hour;
                this.DepartureTimeMinute = minute;
            }
            else
            {
                this.ArrivalTimeHour = hour;
                this.ArrivalTimeMinute = minute;
            }
        }
        else
        {
            throw new ArgumentException($"Invalid direction: {directionString}. Expected values are {string.Join(", ", Enum.GetNames(typeof(Direction)))}.", nameof(directionString));
        }

        this.flightsim(airport);
    }

    public void updateElapsedTime(Airport airport)
    {
        ElapsedDays = airport.ElapsedDays;
        ElapsedHours = airport.ElapsedHours;
        ElapsedMinutes = airport.ElapsedMinutes;
    }

    private void flightSim(Airport airport)
    {
        if (this.Status == Outgoing)
        {
            //Jeg vet at disse tidssammenligningene ikke vil funke, men bare en kjapp draft så jeg ikke glemmer
            //TODO: Fikse tidssammenligning så den faktisk fungerer
            if (elapsedHours == DepartureTimeHour - 1 && elapsedMinutes == DepartureTimeMinutes - 45)
            {
                //Logg flight BRA123 har fått gate {this.AssignedGate} tildelt. F.eks
                Gate availableGate = findAvailableGate();
                ParkGate(availableGate);
            }

            if (elapsedHours == DepartureTimeHour - 1 && elapsedMinutes == DepartureTimeMinutes)
            {
                gate.DepartingPreperations(this);
            }

            if (elapsedHours == DepartureTimeHour && elapsedMinutes == DepartureTimeMinutes - 30)
            {
                gate.DepartFlightFromGate(this);
            }
        }
        else (this.Status == Incoming)
        {
            if (elapsedHours == ArrivalTimeHour && elapsedMinutes == ArrivalTimeMinutes -20 )
            {
                Gate availableGate = this.findAvailableGate();
                Runway bestRunway = this.findOptimalRunway();
                    
            }
        }
    }

        public void takeoff()
        {
            //Simuler f.eks 2 min
            //Sett staus til Departed
            //Sett rullebane til ledig
        }

        public void Land()
        {
            //Sett rullebane til opptatt
            //Simuler f.eks. 2 min
            //Sett status til Arrived
            //Finn den taxi som er connected til denne rullebanen som også er connected til den gaten flighten har fått assigned
        }

        public void ParkGate()
        {
            //Parkere ved gate
        }

        //Denne tror jeg vi ikke trenger, fordi det er ikke en flight som har maintanance, men et fly
        public void performMaintanance()
        {
            //Dette flyet 
        }

        public void changeStatus(FlightStatus status)
        {
            Status = status;
        }

    public Gate findAvailableGate()
    {
        //Loope gjennom alle connected gates til alle terminaler som har samme bool verdi på innland utland
        //Finne en ledig gate
        //Endre instansvariablen til den gaten slik at den nå er opptatt
        return availableGate;
    }

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






        }


    }


}
