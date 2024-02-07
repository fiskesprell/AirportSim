using AirportSimulation;


namespace AirportSimApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Airport gardermoen = new Airport("Gardermoen", "Terminal A", "Taxi A", "Runway A", "Gate 01");
            TimeSimulation simulering = new TimeSimulation();

            Flight newFlight = new Flight("Daily123", "Gatwick", new DateTime(2022, 2, 15), 15, 30, Direction.Incoming, gardermoen);
            //Flight newFlight2 = new Flight("Weekly321", "Gatwick", new DateTime(2022, 2, 15), 17, 00, Direction.Outgoing, gardermoen);
            newFlight.SetFlightFrequency(Frequency.Daily);
            //newFlight2.setFlightFrequency(Frequency.Weekly);
            gardermoen.AddFlight(newFlight);
            //gardermoen.addFlight(newFlight2);
            simulering.SimulateTime(simulering, gardermoen, new DateTime(2022, 2, 14), new DateTime(2022, 2, 16));

        }
    }
}
