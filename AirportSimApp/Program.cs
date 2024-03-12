using AirportSimulation;
using AirportSimulationCl;


namespace AirportSimApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gate testGate = new GateBuilder()
                .AddGateName("Gate 4623724273")
                .AddGateLicence(GateLicence.Commercial)
                .Build();


            Terminal testTerminal = new TerminalBuilder()
                .AddTerminalName("Terminal FuckYou")
                .CreateAndAddNewGate("Gate AlleSuger", GateLicence.Commercial)
                .AddGateToTerminal(testGate)
                .Build();

            Runway runway2 = new Runway("Runway Test");
            Taxi taxi2 = new Taxi("Taxi test");

            TimeConfigManager timeConfigManager = new TimeConfigManager();

            Airport testAirport = new Airport("Gardermoen", "Terminal A", "Taxi A", "Runway A", "Gate A");
            testAirport.AddExistingRunway(runway2);
            testAirport.AddExistingTaxi(taxi2);
            runway2.AddConnectedTaxi(taxi2);
            taxi2.AddConnectedRunway(runway2);
            taxi2.AddConnectedGate(testGate);
            timeConfigManager.AddTimeConfig(testGate.Terminal, runway2, 20, 0);


            Airport testAirport2 = new Airport("Gardermoen", "Terminal A", "Taxi A", "Runway A", "Gate A");
            testAirport.AddExistingTerminal(testTerminal);
            Flight testFlight = new Flight("Bra123", testAirport2, new DateTime(2024, 04, 16), 14, 30, FlightDirection.Outgoing, testAirport);
            Flight testFlight2 = new Flight("Bra123", testAirport2, new DateTime(2024, 04, 16), 14, 00, FlightDirection.Incoming, testAirport);
            testAirport.AddExistingFlight(testFlight);
            testAirport.AddExistingFlight(testFlight2);
            TimeSimulation testTimeSimulation = new TimeSimulation();


            testTimeSimulation.SimulateTime(timeConfigManager, testAirport, new DateTime(2024, 04, 15), new DateTime(2024, 04, 17));
        }
    }
}
