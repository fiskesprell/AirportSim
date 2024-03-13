using AirportSimulation;
using AirportSimulationCl.NetzachTech.AirportSim.Enums;
using AirportSimulationCl.NetzachTech.AirportSim.Infrastructure;
using NetzachTech.AirportSim.Builders;



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

            testTerminal.PrintTerminalInfo();

            Airport testAirport = new Airport("Gardermoen", "Terminal A", "Taxi A", "Runway A", "Gate A");
            Airport testAirport2 = new Airport("Gardermoen", "Terminal A", "Taxi A", "Runway A", "Gate A");
            testAirport.AddExistingTerminal(testTerminal);
            Flight testFlight = new Flight("Bra123", testAirport2, new DateTime(2024, 04, 16), 14, 30, FlightDirection.Outgoing, testAirport);
            Flight testFlight2 = new Flight("Bra123", testAirport2, new DateTime(2024, 04, 16), 14, 00, FlightDirection.Incoming, testAirport);
            testAirport.AddExistingFlight(testFlight);
            testAirport.AddExistingFlight(testFlight2);
            TimeSimulation testTimeSimulation = new TimeSimulation();
            testTimeSimulation.SimulateTime(testAirport, new DateTime(2024, 04, 15), new DateTime(2024, 04, 17));
        }
    }
}
