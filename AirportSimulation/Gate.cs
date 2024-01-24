namespace AirportSimulation
{
    // Ulike typer fly som gates har lov til å ha. Snakke om det er flere eller en bedre måte å kategorisere
    enum gateLicence
    {
        Commercial,
        Transport,
        Personal,
        Military
    }


    public class Gate
    {
        // Vi må diskutere hva som er logiske standardverdier
        private string GateName { get; set; }
        private gateLicence Licence { get; set; } = gateLicence.Commercial;
        // Finne ut hva slags type liste som er best å bruke
        private List<Taxi> ConnectedTaxi = new List<Taxi>();
        // Vi må diskutere om vi skal bruke minutter, sekunder etc for ting som er målt i tid
        private double TurnaroundTime { get; set; } = 10;
        private bool IsAvailable { get; set; } = true;


        public Gate()
        {
            
            GateName = string.Empty;

        }

        // Legge til et taxi object i listen over taxi som er tilkoblet gaten
        // Da kan vi bruke disse listene til å holde styr på hvor fly kan kjøre
        public void addTaxi(Taxi taxi)
        {
            connectedTaxi.Add(taxi);
        }

        public void boarding(Flight flight)
        {
            //Implementere noe for boarding
            //Kanskje bare noe tidgreier?
        }

        public void cleanUp()
        {
            //Her er det bare tenkt tiden fra boadring er ferdig til gaten blir ledig igjen
            //Så clean up er bytte personell, endre info på skjerm osv
            //Blir kanskje bare 
        }

        public void close()
        {
            //Usikker om vi trenger denne da vi heller bare kan bruke set metode på isAvailable

        }


    }
}
