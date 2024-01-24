namespace AirportSimulation
{
    // Ulike typer fly som gates har lov til å ha. Snakke om det er flere eller en bedre måte å kategorisere
    enum gateLicence
    {
        commercial,
        transport,
        personal,
        military
    }


    public class Gate
    {
        private string gateName {  get; set; }
        private enum gateLicence { get; set; }
        // Finne ut hva slags type liste som er best å bruke
        private List<Taxi> connectedTaxi;
        // Vi må diskutere om vi skal bruke minutter, sekunder etc for ting som er målt i tid
        private double turnaroundTime { get; set; }
        private bool isAvailable { get; set; } = true;


        public Gate()
        {
            // Vi må diskutere hva som er logiske standardverdier
            gateName = string.Empty;
            // Å tilegne enum på denne måten funker ikke
            gateLicence = gateLicence;
            connectedTaxi = new List<Taxi>();
            turnaroundTime = 10;

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
