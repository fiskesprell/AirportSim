using AirportSimulation;
using AirportSimulationCl.NetzachTech.AirportSim.Enums;

namespace NetzachTech.AirportSim.Infrastructure
{
    /// <summary>
    /// Manages an airport gate's operations, including the types of aircraft it can accommodate, its availability, and its connection to taxiways. Is also able to be "held" by a Flight object.
    /// </summary>
    public class Gate
    {

        /// <summary>
        /// Name of the gate, numerical format 0-99.
        /// </summary>
        // Format is not implemented with regex or otherwwise. We will discuss whether a new terminal should be preferred, or if devs should be allowed to create infinite gates.
        private string _gateName;
        /// <summary>
        /// Gets or sets the name of the gate.
        /// </summary>
        public string GateName
        { get => _gateName; set => _gateName = value; }

        /// <summary>
        /// Determines the type of planes allowed to use this gate through licensing.
        /// </summary>
        private GateLicence _licence = GateLicence.Commercial;
        /// <summary>
        /// Gets or sets the licence(s) of the gate.
        /// </summary>
        public GateLicence Licence
        { get => _licence; set => _licence |= value; }

        /// <summary>
        /// Manages a list of taxiways connected to the gate.
        /// </summary>
        private List<Taxi> _connectedTaxis = new List<Taxi>();
        /// <summary>
        /// Gets or sets the taxiways connected to the gate.
        /// </summary>
        public List<Taxi> ConnectedTaxis
        { get => _connectedTaxis; }

        /// <summary>
        /// Indicates whether the gate is currently available for use.
        /// </summary>
        private bool _isAvailable = true;
        /// <summary>
        /// Whether the gate is available or not. <br/>
        /// True = gate is available <br/>
        /// False = gate is unavailable
        /// </summary>
        public bool IsAvailable
        { get => _isAvailable; set => _isAvailable = value; }

        /// <summary>
        /// Holds information about the flight currently using the gate.
        /// </summary>
        private Flight _currentHolder;
        /// <summary>
        /// Gets or sets the  flight currently using the gate.
        /// </summary>
        public Flight CurrentHolder
        { get => _currentHolder; set => _currentHolder = value; }

        /// <summary>
        /// Constructor for making a gate
        /// </summary>
        public Gate() { }

        /// <summary>
        /// Constructor for making a gate
        /// </summary>
        public Gate(string gateName)
        {
            GateName = gateName;
            Console.WriteLine("Gate " + gateName + " har blitt opprettet");
        }

        // Legge til et taxi object i listen over taxi som er tilkoblet gaten
        // Da kan vi bruke disse listene til å holde styr på hvor fly kan kjøre

        public void AddTaxi(Taxi taxi)
        {
            _connectedTaxis.Add(taxi);
        }


        // Legger til en spesifikk lisens til gaten
        /// <summary>
        /// Adds a licence to a specific gate
        /// </summary>
        /// <param name="licence"></param>
        public void AddLicence(GateLicence licence)
        {
            Licence |= licence;
        }

        // Fjerner en spesifikk lisens fra gaten
        /// <summary>
        /// Removes a specific licence from a specific gate
        /// </summary>
        /// <param name="licence"></param>
        public void RemoveLicence(GateLicence licence)
        {
            Licence &= ~licence;
        }

        // Fjerner alle licencer fra gaten slik at den ikke har lov å ha noen fly
        /// <summary>
        /// Removes all licences from a specific gate
        /// </summary>
        public void RemoveAllLicences()
        {
            Licence = GateLicence.None;
        }

        //Denne metoden går gjennom alle taxi som er connected til gaten og sjekker hvilken 
        //taxi som har minst kø

        //Denne tror jeg overlapper veldig med findTaxi så vi kan slanke den eller bare fjerne den helt
        public void TransferFlightToTaxi(Flight flight)
        {
            flight.AssignedTaxi.AddToTaxiQueue(flight);
            flight.IsTraveling = true;
        }

        /// <summary>
        /// Checks the gate licence compared to the flight type.
        /// </summary>
        public bool CheckGateLicence(Flight flight)
        {
            FlightType flighttype = flight.FlightType;
            if ((Licence & (GateLicence)flighttype) == Licence)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Adds the gateobject to the list of gates for the terminal passed as an argument
        /// </summary>
        /// <param name="terminal"></param>
        public void AddGateToTerminal(Terminal terminal)
        {
            terminal.AddExistingGate(this);
        }


    }
}
