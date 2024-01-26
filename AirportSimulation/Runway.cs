using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    internal class Runway
    {
        // Instance Variables
        /// <summary>
        /// The name of your Runway.
        /// </summary>
        public String RunwayName;
        /// <summary>
        /// The length of your runway in meters. <br />
        /// Standard value is 3000(m).
        /// </summary>
        public double RunwayLength { get; set; } = 3000;
        /// <summary>
        /// A List of Taxi objects that are connected to this runway.
        /// </summary>
        public List<Taxi> ConnectedTaxi = new List<Taxi>(); // litt usikker på om denne er gjort riktig
        /// <summary>
        /// Queue of flights looking to use the runway. 
        /// </summary>
        public Queue RunwayQueue;
        /// <summary>
        /// The date and time for the most recent maintenance.
        /// </summary>
        public DateTime LastMaintainance;
        /// <summary>
        /// The time it takes from when a plane enters the runway until it is in the air and the runway is available for use. <br />
        /// In seconds. Standard value is 600 (10 minutes).
        /// </summary>
        public double AverageTakeoffTime { get; set; } = 600;
        /// <summary>
        /// Average time it takes from a plane gets cleared for landing until it has landed and is ready to be put in a taxi queue. <br />
        /// In seconds. Standard value is 300 (5 minutes).
        /// </summary>
        public double AverageLandingTime { get; set; } = 300;
        /// <summary>
        /// Returns True if the runway is empty and ready for a plane. <br />
        /// Returns False if the runway is currently in use.
        /// </summary>
        public bool IsAvailable { get; set; } = true;


        // Constructor
        public Runway(string RunwayName)
        {
            this.RunwayName = RunwayName;
        }

        // Methods
        /// <summary>
        /// Sets IsAvailable to True
        /// </summary>
        public void BecomeAvailable()
        {
            this.IsAvailable = true;
        }
        /// <summary>
        /// Sets IsAvailable to False
        /// </summary>
        public void BecomeUnavailable()
        {
            this.IsAvailable = false;
        }

        public void Clear()
        {
            // Ikke implementert enda
        }

        public void Close()
        {
            // Ikke implementert enda
        }

    }
}
