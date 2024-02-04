using AirportSimulation;


namespace AirportSimApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Airport gardermoen = new Airport("Gardermoen", "Terminal A", "Taxi A", "Runway A", "Gate 01");
            TimeSimulation simulering = new TimeSimulation();

            Flight newFlight = new Flight("BRA123", "Gatwick", new DateTime(2022, 2, 15), 15, 30, Direction.Outgoing, gardermoen);
            newFlight.setFlightFrequency(Frequency.Daily);
            gardermoen.addFlight(newFlight);
            simulering.simulateTime(simulering, gardermoen, new DateTime(2022, 2, 14), new DateTime(2022, 3, 14));

        }
    }
}
