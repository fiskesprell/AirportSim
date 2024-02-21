namespace AirportSimulation
{
    
    [Flags]
    public enum GateLicence
    {
        /// <summary>
        /// Defines the types of aircraft licenses a gate can have, allowing for multiple categories simultaneously.
        /// </summary>
        /// <value>
        /// None, 0
        /// Commercial, 1 - Commercial flights allowed.
        /// Transport, 2 - Transport flights allowed.
        /// Personal, 4 - Personal flights allowed.
        /// Military 8 - Military flights allowed.
        /// </value>
        /// <remarks>Flags to allow multiple licences</remarks>
        None = 0,
        Commercial = 1,
        Transport = 2,
        Personal = 4,
        Military = 8
    }


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
        /// Determines the type of planes allowed to use this gate through licensing.
        /// </summary>
        private GateLicence _licence = GateLicence.Commercial;

        /// <summary>
        /// Manages a list of taxiways connected to the gate.
        /// </summary>
        private List<Taxi> _connectedTaxis = new List<Taxi>();

        /// <summary>
        /// Indicates whether the gate is currently available for use.
        /// </summary>
        private bool _isAvailable = true;

        /// <summary>
        /// Holds information about the flight currently using the gate.
        /// </summary>
        private Flight _currentHolder;

        // Vi må diskutere hva som er logiske standardverdier
        /// <summary>
        /// Gets or sets the name of the gate.
        /// </summary>
        public string GateName
        {
            get => _gateName;
            set => _gateName = value;
        }
        // TODO: Fiks denne?
        /// <summary>
        /// Gets or sets the licence(s) of the gate.
        /// </summary>
        private GateLicence Licence
        {
            get => _licence;
            set => _licence = value;
        }
        /// <summary>
        /// Gets or sets the taxiways connected to the gate.
        /// </summary>
        private List<Taxi> ConnectedTaxis
        {
            get => _connectedTaxis;
            set => _connectedTaxis = value;
        }

        /// <summary>
        /// Whether the gate is available or not. <br/>
        /// True = gate is available <br/>
        /// False = gate is unavailable
        /// </summary>
        private bool IsAvailable
        {
            get => _isAvailable;
            set => _isAvailable = value;
        }
        /// <summary>
        /// Gets or sets the  flight currently using the gate.
        /// </summary>
        private Flight CurrentHolder
        {
            get => _currentHolder;
            set => _currentHolder = value;
        }

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
        public void transferFlightToTaxi(Flight flight)
        {
            //Sjekker om lista er tom først
            flight.GetDesiredTaxi().AddToTaxiQueue(flight);
            flight.SetIsTraveling(true);    /// <summary>
    /// Defines the types of aircraft licenses a gate can have, allowing for multiple categories simultaneously.
    /// </summary>
    /// <value>
        }

        /// <summary>
        /// Get method for GateName. This will return a string
        /// </summary>
        public string GetGateName()
        {
            return GateName;
        }

        /// <summary>
        /// Get method for ConnectedTaxi. This will return a list of taxi objects
        /// </summary>
        public List<Taxi> GetConnectedTaxis()
        {
            return ConnectedTaxis;
        }

        /// <summary>
        /// Get method for IsAvailable. This will return a bool
        /// </summary>
        public bool GetIsAvailable()
        {
            return IsAvailable;
        }

        /// <summary>
        /// Set method for IsAvailable. This will change the value of IsAvailable
        /// </summary>
        public void SetIsAvailable(bool status)
        {
            IsAvailable = status;
        }

        /// <summary>
        /// Get method for CurrentHolder. This will return a flight object
        /// </summary>
        public Flight GetCurrentHolder()
        {
            return CurrentHolder;
        }

        public void SetCurrentHolder(Flight flight)
        {
            CurrentHolder = flight;
        }

        /// <summary>
        /// Get method GateLicence. This will return an enumvalue
        /// </summary>
        public GateLicence GetGateLicence()
        {
            return Licence;
        }

        /// <summary>
        /// Checks the gate licence compared to the flight type.
        /// </summary>
        public bool CheckGateLicence(Flight flight)
        {
            FlightType flighttype = flight.GetFlightType();
            if ((this.Licence & (GateLicence)flighttype) == this.Licence)
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
