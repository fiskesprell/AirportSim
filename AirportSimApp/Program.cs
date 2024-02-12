using AirportSimulation;


namespace AirportSimApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Airport gardermoen = new Airport("Gardermoen", "Terminal A", "Taxi A", "Runway A", "Gate 01");
            TimeSimulation simulering = new TimeSimulation();

            // adding FLIGHTS
            Flight newFlight1 = new Flight("DAILY_1", "Gatwick", new DateTime(2022, 2, 15), 15, 30, Direction.Incoming, gardermoen);
            Flight newFlight2 = new Flight("DAILY_2", "Gatwick", new DateTime(2022, 2, 16), 15, 30, Direction.Outgoing, gardermoen);
            Flight newFlight3 = new Flight("DAILY_3", "Gatwick", new DateTime(2022, 2, 17), 19, 15, Direction.Incoming, gardermoen);
            Flight newFlight4 = new Flight("DAILY_4", "Gatwick", new DateTime(2022, 2, 18), 09, 30, Direction.Outgoing, gardermoen);
            Flight newFlight5 = new Flight("DAILY_6", "Gatwick", new DateTime(2022, 2, 19), 07, 15, Direction.Incoming, gardermoen);
            Flight newFlight6 = new Flight("DAILY_7", "Gatwick", new DateTime(2022, 2, 20), 04, 30, Direction.Outgoing, gardermoen);
            Flight newFlight7 = new Flight("DAILY_8", "Gatwick", new DateTime(2022, 2, 21), 19, 15, Direction.Incoming, gardermoen);
            Flight newFlight8 = new Flight("DAILY_9", "Gatwick", new DateTime(2022, 2, 22), 22, 30, Direction.Outgoing, gardermoen);
            Flight newFlight9 = new Flight("DAILY_10", "Gatwick", new DateTime(2022, 2, 23), 13, 30, Direction.Incoming, gardermoen);
            Flight newFlight10 = new Flight("WEEKLY_1", "Gatwick", new DateTime(2022, 2, 14), 15, 15, Direction.Outgoing, gardermoen);
            Flight newFlight11 = new Flight("WEEKLY_2", "Gatwick", new DateTime(2022, 2, 15), 13, 30, Direction.Incoming, gardermoen);
            Flight newFlight12 = new Flight("WEEKLY_3", "Gatwick", new DateTime(2022, 2, 16), 14, 30, Direction.Outgoing, gardermoen);
            Flight newFlight13 = new Flight("WEEKLY_4", "Gatwick", new DateTime(2022, 2, 17), 15, 20, Direction.Incoming, gardermoen);
            Flight newFlight14 = new Flight("WEEKLY_5", "Gatwick", new DateTime(2022, 2, 18), 15, 30, Direction.Outgoing, gardermoen);
            Flight newFlight15 = new Flight("WEEKLY_6", "Gatwick", new DateTime(2022, 2, 19), 16, 30, Direction.Incoming, gardermoen);
            Flight newFlight16 = new Flight("WEEKLY_7", "Gatwick", new DateTime(2022, 2, 20), 17, 18, Direction.Outgoing, gardermoen);
            Flight newFlight17 = new Flight("WEEKLY_8", "Gatwick", new DateTime(2022, 2, 21), 18, 30, Direction.Incoming, gardermoen);
            Flight newFlight18 = new Flight("WEEKLY_9", "Gatwick", new DateTime(2022, 2, 22), 19, 13, Direction.Outgoing, gardermoen);
            Flight newFlight19 = new Flight("WEEKLY_10", "Gatwick", new DateTime(2022, 2, 23), 10, 47, Direction.Incoming, gardermoen);

            //daily 1 - 9
            newFlight1.SetFlightFrequency(Frequency.Daily);
            newFlight1.SetFlightType(FlightType.Commercial);
            newFlight2.SetFlightFrequency(Frequency.Daily);
            newFlight2.SetFlightType(FlightType.Commercial);
            newFlight3.SetFlightFrequency(Frequency.Daily);
            newFlight3.SetFlightType(FlightType.Commercial);
            newFlight4.SetFlightFrequency(Frequency.Daily);
            newFlight4.SetFlightType(FlightType.Commercial);
            newFlight5.SetFlightFrequency(Frequency.Daily);
            newFlight5.SetFlightType(FlightType.Commercial);
            newFlight6.SetFlightFrequency(Frequency.Daily);
            newFlight6.SetFlightType(FlightType.Commercial);
            newFlight7.SetFlightFrequency(Frequency.Daily);
            newFlight7.SetFlightType(FlightType.Commercial);
            newFlight8.SetFlightFrequency(Frequency.Daily);
            newFlight8.SetFlightType(FlightType.Commercial);
            newFlight9.SetFlightFrequency(Frequency.Daily);
            newFlight9.SetFlightType(FlightType.Commercial);
            // weekly
            newFlight10.SetFlightFrequency(Frequency.Weekly);
            newFlight10.SetFlightType(FlightType.Commercial);
            newFlight11.SetFlightFrequency(Frequency.Weekly);
            newFlight11.SetFlightType(FlightType.Commercial);
            newFlight12.SetFlightFrequency(Frequency.Weekly);
            newFlight12.SetFlightType(FlightType.Commercial);
            newFlight13.SetFlightFrequency(Frequency.Weekly);
            newFlight13.SetFlightType(FlightType.Commercial);
            newFlight14.SetFlightFrequency(Frequency.Weekly);
            newFlight14.SetFlightType(FlightType.Commercial);
            newFlight15.SetFlightFrequency(Frequency.Weekly);
            newFlight15.SetFlightType(FlightType.Commercial);
            newFlight16.SetFlightFrequency(Frequency.Weekly);
            newFlight16.SetFlightType(FlightType.Commercial);
            newFlight17.SetFlightFrequency(Frequency.Weekly);
            newFlight17.SetFlightType(FlightType.Commercial);
            newFlight18.SetFlightFrequency(Frequency.Weekly);
            newFlight18.SetFlightType(FlightType.Commercial);
            newFlight19.SetFlightFrequency(Frequency.Weekly);
            newFlight19.SetFlightType(FlightType.Commercial);

            // add flights
            gardermoen.AddNewFlight(newFlight1);
            gardermoen.AddNewFlight(newFlight2);
            gardermoen.AddNewFlight(newFlight3);
            gardermoen.AddNewFlight(newFlight4);
            gardermoen.AddNewFlight(newFlight5);
            gardermoen.AddNewFlight(newFlight6);
            gardermoen.AddNewFlight(newFlight7);
            gardermoen.AddNewFlight(newFlight8);
            gardermoen.AddNewFlight(newFlight9);
            gardermoen.AddNewFlight(newFlight10);
            gardermoen.AddNewFlight(newFlight11);
            gardermoen.AddNewFlight(newFlight12);
            gardermoen.AddNewFlight(newFlight13);
            gardermoen.AddNewFlight(newFlight14);
            gardermoen.AddNewFlight(newFlight15);
            gardermoen.AddNewFlight(newFlight16);
            gardermoen.AddNewFlight(newFlight17);
            gardermoen.AddNewFlight(newFlight18);
            gardermoen.AddNewFlight(newFlight19);


            // Add Terminal
            gardermoen.AddNewTerminal("SillyTerminal");
            gardermoen.AddNewTaxi("sillygame");
            gardermoen.AddNewRunway("sillyRunway");
            gardermoen.AddNewConnectedGateAndTaxi("Silly", "Goblin");
            
            // Add Taxi


            // add Runway


            // add Gate




            simulering.SimulateTime(simulering, gardermoen, new DateTime(2022, 2, 14), new DateTime(2022, 2, 25));

        }
    }
}
