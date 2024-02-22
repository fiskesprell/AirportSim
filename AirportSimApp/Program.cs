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
        }
    }
}
