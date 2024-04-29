
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MyPlane = NetzachTech.AirportSim.Infrastructure;
using FlightOperations = NetzachTech.AirportSim.FlightOperations;
using AirportSimulation;

namespace AirportGUI
{
    class MovablePartsViewModel : INotifyPropertyChanged
    {

        private Airport _airport;
        public Airport Airport
        {
            get { return _airport; }
            set { _airport = value; OnPropertyChanged(); }
        }
        public string AirportName => _airport?.AirportName;
        public event PropertyChangedEventHandler PropertyChanged;


        public ICommand CreateFlightCommand { get; private set; }
        public ICommand CreatePlaneCommand { get; private set; }

        public MovablePartsViewModel(Airport airport)
        {
            _airport = airport;

            CreateFlightCommand = new RelayCommand(CreateFlightAction);
            CreatePlaneCommand = new RelayCommand(CreatePlaneAction);
        }

        public void CreateFlightAction(object parameter)
        {
            if (parameter is FlightCreationParameters flightParams)
            {
                CreateFlight(flightParams);
            }
            else
            {
                throw new ArgumentException("Invalid parameter for flight");
            }
        }

        public void CreateFlight(FlightCreationParameters parameter)
        {
            try
            {
                if (!TryParseFlightDateTime(parameter.FlightDate, parameter.FlightTime, out DateTime dateTime))
                {
                    throw new ArgumentException("Invalid time or date format");
                }


                Flight flight = new Flight();
                flight.Number = parameter.FlightNumber;
                flight.DestinationAirport = new Airport(parameter.Destination);
                flight.ScheduledDay = dateTime;
                flight.ScheduledHour = dateTime.Hour;
                flight.ScheduledMinutes = dateTime.Minute;


                if (parameter.IsIncoming)
                {
                    flight.FlightDirection = FlightOperations.FlightDirection.Incoming;
                    MyPlane.Plane incomingPlane = new MyPlane.Plane();
                    flight.AssignedPlane = incomingPlane;
                }

                else
                {
                    flight.FlightDirection = FlightOperations.FlightDirection.Outgoing;
                    
                }

                _airport.AddExistingFlight(flight);
                OnPropertyChanged(nameof(Airport));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to create flight: {ex.Message}");
            }

        }

        private bool TryParseFlightDateTime(string flightDate, string flightTime, out DateTime dateTime)
        {
            dateTime = DateTime.MinValue;
            string[] dates = flightDate.Split('.');
            string[] departureTime = flightTime.Split('.');

            if (dates.Length == 3 && departureTime.Length == 2)
            {
                if (int.TryParse(dates[0], out int day) && int.TryParse(dates[1], out int month) &&
                int.TryParse(dates[2], out int year) && int.TryParse(departureTime[0], out int hour) &&
                int.TryParse(departureTime[1], out int minute))
                {
                    dateTime = new DateTime(year, month, day, hour, minute, 0);
                    return true;
                }
            }
            return false;

        }

        public void CreatePlaneAction(object parameter)
        {
            if (parameter is PlaneCreationParameters planeParams)
            {
                CreatePlane(planeParams);
            }
        }

        public void CreatePlane(PlaneCreationParameters parameter)
        {
            string name = parameter.PlaneName;
            string model = parameter.PlaneModel;
            string tail = parameter.Tailnumber;

            MyPlane.Plane plane = new MyPlane.Plane(name, model, tail);
            _airport.AddPlaneToListOfAvailablePlanes(plane);
            OnPropertyChanged(nameof(Airport));

        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
