﻿namespace AirportSimulation
{
    // Ulike typer fly som gates har lov til å ha
    // Å bruke Flags gjør at det er mulig å ha flere kategorier i stedet for bare 1
    [Flags]
    enum GateLicence
    {
        None = 0,
        Commercial = 1,
        Transport = 2,
        Personal = 4,
        Military = 8
    }


    public class Gate
    {
        // Vi må diskutere hva som er logiske standardverdier
        /// <summary>
        /// The name of your Gate.
        /// </summary>
        private string GateName { get; set; }
        // TODO: Fiks denne?
        /// <summary>
        /// This gate's licence. Decides the type of planes allowed to use this gate. <br/>
        /// Valid values are: None, Commercial, Transport, Personal, Military.
        /// </summary>
        private GateLicence Licence { get; set; } = GateLicence.Commercial;
        /// <summary>
        /// List of taxiways connected to this gate.
        /// </summary>
        private List<Taxi> ConnectedTaxi = new List<Taxi>();
        // Vi må diskutere om vi skal bruke minutter, sekunder etc for ting som er målt i tid
        // (fiskesprell) Jeg bruker sekunder som default. Kanskje endre etterpå?
        /// <summary>
        /// TODO: write this
        /// </summary>
        private double TurnaroundTime { get; set; } = 10;
        /// <summary>
        /// Whether the gate is available or not. <br/>
        /// True = gate is available <br/>
        /// False = gate is unavailable
        /// </summary>
        private bool IsAvailable { get; set; } = true;
        /// <summary>
        /// The flight currently using the gate.
        /// </summary>
        private Flight CurrentHolder { get; set; }


        public Gate()
        {
            GateName = string.Empty;
        }

        // Legge til et taxi object i listen over taxi som er tilkoblet gaten
        // Da kan vi bruke disse listene til å holde styr på hvor fly kan kjøre
        public void AddTaxi(Taxi taxi)
        {
            ConnectedTaxi.Add(taxi);
        }

        public void DepartingPreperation(Flight flight)
        {
            //45 min før scheduled departure start boarding
            //Implementere noe for boarding
            //Kanskje bare noe tidgreier?
        }

        public void ArrivalPreperation()
        {
            //Her er det bare tenkt tiden fra boadring er ferdig til gaten blir ledig igjen
            //Så clean up er bytte personell, endre info på skjerm osv
            //Blir kanskje bare 
        }

        // Legger til en taxi til listen med connectedTaxi
        public void AddConnectedTaxi(Taxi taxi)
        {
            ConnectedTaxi.Add(taxi);
        }

        // Legger til en spesifikk lisens til gaten
        public void AddLicence(GateLicence licence) 
        {
            Licence |= GateLicence.licence;
        }

        // Fjerner en spesifikk lisens fra gaten
        public void RemoveLicence(GateLicence licence)
        {
            Licence &= ~GateLicence.licence;
        }

        // Fjerner alle licencer fra gaten slik at den ikke har lov å ha noen fly
        public void removeAllLicences()
        {
            Licence = GateLicence.None;
        }

        //Denne metoden går gjennom alle taxi som er connected til gaten og sjekker hvilken 
        //taxi som har minst kø
        public void transferFlightToTaxi(Flight flight)
        {
            //Sjekker om lista er tom først
            if (ConnectedTaxi.Count != 0)
            {
                Taxi correctTaxi = null;
                int minQueueLength = int.MaxValue;

                //Går gjennom alle taxi og sjekker lengden på køen
                foreach (Taxi taxi in ConnectedTaxi)
                {
                    //Hvis køen er mindre enn tidligere iterasjoner så velg den taxi
                    if (taxi.lengthQueue() < minQueueLength)
                    {
                        correctTaxi = taxi;
                        minQueueLength = taxi.lengthQueue();
                    }
                }

                //En liten sjekk på at den faktisk har valgt ut en taxi
                if (correctTaxi != null)
                {
                    //Legger til flight i den taxi sin kø
                    correctTaxi.AddToQueue(flight);
                }
            }
        }


    }
}
