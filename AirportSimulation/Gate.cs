namespace AirportSimulation
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
        private string GateName { get; set; }
        private GateLicence Licence { get; set; } = GateLicence.Commercial;
        private List<Taxi> ConnectedTaxi = new List<Taxi>();
        // Vi må diskutere om vi skal bruke minutter, sekunder etc for ting som er målt i tid
        private double TurnaroundTime { get; set; } = 10;
        private bool IsAvailable { get; set; } = true;
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


    }
}
