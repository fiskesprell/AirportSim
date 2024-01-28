﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    enum FlightType
    {
        Commercial,
        Transport,
        Personal,
        Military
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
        private DateTime DepartureTime { get; set; }
        private DateTime ArrivalTime { get; set; }
        private string Destination { get; set; }
        private DateTime LastMaintanace { get; set; }
        private FlightStatus Status = FlightStatus.OnTime;
        private Frequency Frequency { get; set; } = Frequency.OneTime;

    }

    public Flight(string Number, string Destination, DateTime DepartureTime, DateTime Arrival)
    {
        this.Number = Number;
        this.Destination = Destination;
        this.DepartureTime = DepartureTime;
        this.ArrivalTime = Arrival;

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

    public void findAvailableGate()
    {
        //Loope gjennom alle connected gates til alle terminaler som har samme bool verdi på innland utland
        //Finne en ledig gate
        //Endre instansvariablen til den gaten slik at den nå er opptatt
     
    }

    public void landingPreperation()
    {
        //Loope gjennom alle terminaler får å finne en med samme bool verdi
        //Loope gjennom alle gates i riktig terminale for å finne en ledig
        AssignedGate = gate;
        //loope gjennom alle taxi som er connected med den gaten
        //loope gjennom alle rullebanene som er connected med de taxiene 
        //finne optimal løsning mtp kø
        //Sette flighten i rullebanekø

        // Tildele gate 20 min før landing
        // Må fortsatt lande selv om ingen gates er ledige.
        // Da står den heller og venter på gate når den er på bakken.

        // Steg 1: Finn 20 minutter før Arrival til å begynne prosessen



        // Steg 2: Når det er 20 minutter til landing
        // 2.1 - Se om flight er innlands eller utlands
        // 2.2 - Finn alle terminaler som er innlands/utlands (samme som flight).
        // 2.3 - Gå gjennom alle terminaler, finn en som er ledig.
        // 2.4 - gå gjennom denne ledige terminalen, og finn en ledig gate med minst kø.



    }


}
