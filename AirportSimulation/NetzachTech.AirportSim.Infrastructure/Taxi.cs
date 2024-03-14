﻿using AirportSimulation;
using NetzachTech.AirportSim.FlightOperations;
using System;
using System.Collections.Generic;
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
        /// List of gates connected to this taxiway.
        /// </summary>
        private List<Gate> _connectedGates = new List<Gate>();
        public List<Gate> ConnectedGates
        { get => _connectedGates; }

        /// <summary>
        /// List of runways connected to this taxiway.
        /// </summary>
        private List<Runway> _connectedRunways = new List<Runway>();
        public List<Runway> ConnectedRunways
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
        public Taxi(string taxiName)
        {
            TaxiName = taxiName;
        }

        /// <summary>
        /// This adds a flight to the taxi queue.
        /// </summary>
        /// <param name="flight">Flight</param>
        public void AddToTaxiQueue(Flight flight)
        {
            if (flight.FlightDirection == FlightDirection.Outgoing)
            {
                if (flight.Status != FlightStatus.Departing)
                {
                    if (flight.ElapsedMinutes == 0)
                    {
                        string elapsedMinutes = "00";
                        Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + elapsedMinutes + " flight " + flight.Number + " started traveling on " + this.TaxiName + " towards " + flight.AssignedGate.GateName);
                    }
                    else
                    {
                        Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.Number + " started traveling on " + this.TaxiName + " towards " + flight.AssignedGate.GateName);
                    }
                    TaxiQueue.Enqueue(flight);
                }

                else
                {
                    if (flight.ElapsedMinutes == 0)
                    {
                        string elapsedMinutes = "00";
                        Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + elapsedMinutes + " flight " + flight.Number + " started traveling on " + this.TaxiName + " towards " + flight.AssignedRunway.RunwayName);
                    }
                    else
                    {
                        Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.Number + " started traveling on " + this.TaxiName + " towards " + flight.AssignedRunway.RunwayName);
                    }
                    TaxiQueue.Enqueue(flight);
                }
            }


            else if (flight.FlightDirection == FlightDirection.Incoming)
            {
                if (flight.AssignedGate != null)
                {
                    Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.Number + " started traveling on " + TaxiName + " towards " + flight.AssignedGate.GateName);
                }
                else
                {
                    Console.WriteLine("Day: " + flight.ElapsedDays + " - at: " + flight.ElapsedHours + ":" + flight.ElapsedMinutes + " flight " + flight.Number + " started traveling on " + TaxiName + " towards a Gate");
                }

                TaxiQueue.Enqueue(flight);

            }


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
    }

}
