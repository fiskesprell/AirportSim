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
        public String RunwayName;
        public double RunwayLength { get; set; } = 3000; // in meters
        public List<Taxi> ConnectedTaxi = new List<Taxi>(); // litt usikker på om denne er gjort riktig
        public Queue RunwayQueue;
        public DateTime LastMaintainance;
        // fra fly kommer inn på rullebanen, til det har lettet og rullebanen er ledig igjen
        public double AverageTakeoffTime { get; set; } = 600; // in seconds - here 10 minutes
        // fra et fly blir klarert til å lande til det er landet, bremset, og klart for å sette seg i taxikø
        public double AverageLandingTime { get; set; } = 300; // in seconds (5 minutes)
        public bool IsAvailable { get; set; } = true;


        // Constructor
        public Runway(string RunwayName)
        {
            this.RunwayName = RunwayName;
        }

        // Methods
        public void BecomeAvailable()
        {
            this.IsAvailable = true;
        }

        public void clear()
        {
            // Ikke implementert enda
        }

        public void close()
        {
            // Ikke implementert enda
        }

    }
}
