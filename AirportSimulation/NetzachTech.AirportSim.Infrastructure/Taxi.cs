using AirportSimulation;
using NetzachTech.AirportSim.FlightOperations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NetzachTech.AirportSim.Infrastructure
{

    /// <summary>
    /// Represents a taxiway, managing connected gates, runways, and the queue of flights.
    /// </summary>
    public class Taxi
    {
        /// <summary>
        /// The name of the taxiway.
        /// </summary>
        private string _taxiName;
        public string TaxiName
        { get => _taxiName; set => _taxiName = value; }

        /// <summary>
        /// Gets or sets the type of the taxiway, distinguishing between main and crossing taxiways.
        /// </summary>
        public TaxiwayType TaxiwayType { get; set; }

        /// <summary>
        /// List of gates connected to this taxiway.
        /// </summary>
        private ObservableCollection<Gate> _connectedGates = new ObservableCollection<Gate>();
        public ObservableCollection<Gate> ConnectedGates
        { get => _connectedGates; }

        /// <summary>
        /// List of runways connected to this taxiway.
        /// </summary>
        private ObservableCollection<Runway> _connectedRunways = new ObservableCollection<Runway>();
        public ObservableCollection<Runway> ConnectedRunways
        { get => _connectedRunways; }

        /// <summary>
        /// Queue of flights that wish to use the taxiway.
        /// </summary>
        private Queue<Flight> _taxiQueue = new Queue<Flight>();
        public Queue<Flight> TaxiQueue
        { get => _taxiQueue; }

        /// <summary>
        /// Tells you whether the taxiway is available or not. <br/>
        /// True = Taxiway is available. <br/>
        /// False = Taxiway is unavailable.
        /// </summary>
        private bool _isAvailable = true;
        public bool IsAvailable
        { get => _isAvailable; set => _isAvailable = value; }

        public Taxi() { }

        /// <summary>
        /// Initializes a new instance of the Taxi class.
        /// </summary>
        /// <param name="taxiName">The name of the taxiway, typically A-Z.</param>
        /// <param name="taxiwayType">The type of the taxiway.</param> 
        public Taxi(string taxiName, TaxiwayType taxiwayType)
        {
            TaxiName = taxiName;
            TaxiwayType = taxiwayType;
        }

        /// <summary>
        /// This adds a flight to the taxi queue.
        /// </summary>
        /// <param name="flight">Flight</param>
        public void AddToTaxiQueue(Flight flight)
        {
            TaxiQueue.Enqueue(flight);
        }

        /// <summary>
        /// Removes the flight from the start of the queue. Based on the status of said flight, it either gets access to a runway queue, or arrives at their gate
        /// </summary>
        public void RemoveFromTaxiQueue()
        {

            Flight flight = TaxiQueue.Dequeue();
            
            if (flight.FlightDirection == FlightDirection.Incoming)
            {
                flight.ParkFlightAtGate(flight.AssignedGate);
            }
            else
            {
                if (flight.Status != FlightStatus.Departing)
                {
                    //Hvis statusen er departing så er den ferdig med å boarde så da skal den finne en taxi for å finne en runway for å ta av
                    if (!flight.IsTraveling)
                    {
                        flight.ParkFlightAtGate(flight.AssignedGate);
                    }
                }

                else
                {
                    if (flight.AssignedRunway == null)
                    {
                        Runway correctRunway = flight.FindRunway();
                        correctRunway.AddToRunwayQueue(flight);
                    }//hvis statusen ikke er "departing" så vil det si at den ikke har boardet enda og skal til gate. Dvs, den kommer fra hangar
                    else
                    {
                        flight.AssignedRunway.AddToRunwayQueue(flight);
                    }
                }

            }
        }

        /// <summary>
        /// Adds a gate to the list of connected gates
        /// </summary>
        /// <param name="gate">Gate to connect</param>
        public void AddConnectedGate(Gate gate)
        {
            ConnectedGates.Add(gate);
        }

        /// <summary>
        /// Removes a certain gate from the list of connected gates
        /// </summary>
        /// <param name="gate"></param>
        public void RemoveConnectedGate(Gate gate)
        {
            ConnectedGates.Remove(gate);
        }

        /// <summary>
        /// Adds a runway to the list of connected runways
        /// </summary>
        /// <param name="runway"></param>
        public void AddConnectedRunway(Runway runway)
        {
            ConnectedRunways.Add(runway);
        }

        /// <summary>
        /// Removes a runway from the list of connected runways
        /// </summary>
        /// <param name="runway"></param>
        public void RemoveConnectedRunway(Runway runway)
        {
            ConnectedRunways.Remove(runway);
        }

        /// <summary>
        /// Finds and connects to a gate if the given name matches with the name of any of the gates in the given airport.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="airport"></param>
        public void ConnectToGateWithName(string name, Airport airport)
        {
            foreach (var terminal in airport.AllTaxis)
            {
                foreach(var gate in terminal.ConnectedGates)
                    if (gate.GateName.Equals(name))
                    {
                        ConnectedGates.Add(gate);
                    }
            }
        }

        /// <summary>
        /// Finds and connects to a runway if the given name matches with the name of any of the runways in the given airport.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="airport"></param>
        public void ConnectToRunwayWithName(string name, Airport airport)
        {
            foreach (var runway in airport.AllRunways)
            {
                if (runway.RunwayName.Equals(name))
                {
                    ConnectedRunways.Add(runway);
                }
            }
        }
    }

}
