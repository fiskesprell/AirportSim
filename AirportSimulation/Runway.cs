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
        public double RunwayLength = 3000; // in meters
        public ArrayList ConnectedTaxi = new ArrayList(); // litt usikker på om denne er gjort riktig
        public Queue RunwayQueue;
        public DateTime LastMaintainance;
        // fra fly kommer inn på rullebanen, til det har lettet og rullebanen er ledig igjen
        public double AverageTakeoffTime = 600; // in seconds - here 10 minutes
        // fra et fly blir klarert til å lande til det er landet, bremset, og klart for å sette seg i taxikø
        public double AverageLandingTime = 300; // in seconds (5 minutes)
        public bool IsAvailable = true;


        // Constructor
        public Runway(string RunwayName)
        {
            this.RunwayName = RunwayName;
        }
    }
}
