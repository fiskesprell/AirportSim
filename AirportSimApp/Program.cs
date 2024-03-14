using AirportSimulation;
using NetzachTech.AirportSim.Enums;
using NetzachTech.AirportSim.Infrastructure;
using NetzachTech.AirportSim.Builders;
using NetzachTech.AirportSim.Infrastructure;



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
            timeConfigManager.AddTimeConfig(testGate.Terminal, runway2, 20);


            Airport testAirport2 = new Airport("Gardermoen", "Terminal A", "Taxi A", "Runway A", "Gate A");
            testAirport.AddExistingTerminal(testTerminal);
            Flight testFlight = new("Bra123", testAirport2, new DateTime(2024, 04, 16), 14, 30, FlightDirection.Outgoing, testAirport);
            Flight testFlight2 = new("Bra123", testAirport2, new DateTime(2024, 04, 16), 14, 00, FlightDirection.Incoming, testAirport);
            testAirport.AddExistingFlight(testFlight);
            testAirport.AddExistingFlight(testFlight2);
            TimeSimulation testTimeSimulation = new TimeSimulation();


            

            Airport test3 = new Airport();
            Terminal testTerminal3 = new Terminal("Terminal A");
            Gate gate = new Gate("Gate4");
            Taxi taxi = new Taxi("Taxi B");
            Runway runway = new Runway("Runway C");
            testTerminal3.AddExistingGate(gate);
            gate.AddTaxi(taxi);
            taxi.AddConnectedGate(gate);
            taxi.AddConnectedRunway(runway);
            runway.AddConnectedTaxi(taxi);
            test3.AddExistingTaxi(taxi);
            test3.AddExistingTerminal(testTerminal3);
            test3.AddExistingRunway(runway);
            Flight testFlight3 = new Flight("Bra123", testAirport2, new DateTime(2024, 04, 15), 14, 30, FlightDirection.Outgoing, test3);
            test3.AddExistingFlight(testFlight3);

            TimeConfigManager timeConfigManager1 = new TimeConfigManager();
            //timeConfigManager1.AddTimeConfig(testTerminal3, runway, 15);
            
            testTimeSimulation.SimulateTime(timeConfigManager1, test3, new DateTime(2024, 04, 15), new DateTime(2024, 04, 16));


        }
    }
}
