using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportSimulation;

namespace AirportSimulationCl
{
    public class Plane
    {
        //Beskrivelse
        private string PlaneName;
        private string PlaneModel;
        private int TailNumber;

        //Funksjonalitet
        private int PlaneLength;
        private int PlaneBodyWidth;
        private int PlaneWingspan;
        private int PlaneTopVelocity;
        private int PlaneWeight;
        private double PlaneFuelBurnRate;

        //Tilgjengelighet
        private bool PlaneIsInUse;
        private bool PlaneIsAvailable;
        private DateTime PlaneLastMaintanance;
        private DateTime ScheduledMaintanance;
        private Airport CurrentAirport;

        //Kapasitet
        private int PlanePassangerCapacity;
        private int PlaneCargoCapacity;
        private int PlaneFuelCapacity;

        //Historikk
        private List<Flight> PlaneHistory = new List<Flight>();
        

        public Plane() { }

        private void LandPlane() { }

        private void TakeoffPlane() { }

        private void ParkPlaneAtGate() { }

        private void SchedulePlaneMaintanance() { }

        private void PerformPlaneMaintanace() { }

        private void AssignPlaneToAirport() { }

        private void TakePlaneOutOfUse() { }

        private void SetPlaneInUse() { }

        private void PrintPlaneHistory() { }




    }
}
