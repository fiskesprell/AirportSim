using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportSimulation
{
    //Dette er en draft på hvordan en bruker skal kunne kjøre en simulering på 7 dager for flyplassen gardemoen
    internal class Program
    {
        public static void Main(string[] args)
        {
            //Ved opprettelsen av en flyplass vil også en terminal, en taxi, en runway, og en gate opprettes
            Airport gardemoen = new Airport("Gardemoen", "A", "T25", "R13", "G01";
            //Lager en instans av TimeSimulation
            TimeSimulation timeSimulation = new TimeSimulation();
            //Kjører simuleringsmetoden med flyplassen, antall dager, antall timer, antall minutter som argumenter
            timeSimulation.SimulateTime(gardemoen, 7, 0, 0);

            //Vet forresten ikke hvordan man lager en kjørbar "main" så dette funker garantert ikke
        }

    }
}
