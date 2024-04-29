using AirportGUI.NetzachTech.AirportSim.DataContext;
using AirportSimulation;
using AirportSimulationCl.NetzachTech.AirportSim.EventArguments;
using NetzachTech.AirportSim.EventArguments;
using NetzachTech.AirportSim.Time;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AirportGUI
{
    /// <summary>
    /// Interaction logic for Simulation.xaml
    /// </summary>
    public partial class Simulation : Page, INotifyPropertyChanged
    {
        private ObservableCollection<string> _eventList = new ObservableCollection<string>();
        public ObservableCollection<string> EventList
        { get { return _eventList; } }

        private Airport _airport;
        private TimeSimulation _timeSimulation;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _elapsedDays = 0;
        public int ElapsedDays
        {
            get => _elapsedDays;
            set
            {
                if (_elapsedDays != value)
                {
                    _elapsedDays = value;
                    OnPropertyChanged(nameof(ElapsedDays));
                }
            }
        }
        private int _elapsedHours = 0;
        public int ElapsedHours
        {
            get => _elapsedHours;
            set
            {
                if (_elapsedHours != value)
                {
                    _elapsedHours = value;
                    OnPropertyChanged(nameof(ElapsedHours));
                }
            }
        }
        private int _elapsedMinutes = 0;
        public int ElapsedMinutes
        {
            get => _elapsedMinutes;
            set
            {
                if (_elapsedMinutes != value)
                {
                    _elapsedMinutes = value;
                    OnPropertyChanged(nameof(ElapsedMinutes));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Simulation(Airport airport, TimeSimulation timeSimulation, DateTime startDate, DateTime endDate, bool flightEvents)
        {
            InitializeComponent();
            _airport = airport;
            _timeSimulation = timeSimulation;
            _startDate = startDate;
            _endDate = endDate;

            SetDataContext(airport, timeSimulation, startDate, endDate);

            if (flightEvents)
            {
                FlightsEventsChecked();
            }

            _timeSimulation.TimeUpdated += TimeSimulation_TimeUpdated;

        }

        private async void StartButton_Click(object sender, RoutedEventArgs e) 
        {

            if (CheckSimulation(_airport))
            {
                _timeSimulation.SimulateTime(_airport, _startDate, _endDate);
            }

            else
            {
                MessageBox.Show(Application.Current.MainWindow, "Cant simulate a airport without proper infrastructure setup.", "Error", MessageBoxButton.OK);
            }
        }

        private void SetDataContext(Airport airport, TimeSimulation timeSimulation, DateTime startDate, DateTime endDate)
        {
            var myDataContext = new TimeAirportDataContext
            {
                Airport = airport,
                TimeSimulation = timeSimulation,
                StartDate = startDate,
                EndDate = endDate,
                EventList = _eventList
            };

            DataContext = myDataContext;
            
        }

        private bool CheckSimulation(Airport airport) 
        {
            if (!(airport.AllGates.Count > 0))
            {
                return false;
            }

            if (!(airport.AllRunways.Count > 0))
            {
                return false;
            }

            if (!(airport.AllTaxis.Count > 0))
            {
                return false;
            }

            if (!(airport.AllTerminals.Count > 0))
            {
                return false;
            }

            //if (airport.AllFlights.Count >= 1 && airport.ListOfPlanes.Count < 1)
            //{
            //    return false;
            //}


            return true;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == textBox.Tag.ToString())
            {
                textBox.Text = string.Empty;
                textBox.Foreground = new SolidColorBrush(Colors.Black);
                textBox.FontStyle = FontStyles.Normal;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = textBox.Tag.ToString();
                textBox.Foreground = new SolidColorBrush(Colors.Gray);
                textBox.FontStyle = FontStyles.Italic;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateBack();

        }

        private void TimeSimulation_TimeUpdated(object sender, EventArgs e)
        {
            if (e is TimeUpdatedEventArgs timeArgs)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ElapsedDays = timeArgs.ElapsedDays;
                    ElapsedHours = timeArgs.ElapsedHours;
                    ElapsedMinutes = timeArgs.ElapsedMinutes;
                });
            }
 
        }

        private void AddEventToList(string eventString)
        {
            EventList.Add(eventString);
        }

        private void FlightsEventsChecked()
        {
            foreach (var flight in _airport.AllFlights)
            {
                flight.FlightIsAssignedGate += FlightIsAssignedGateHandler;
                flight.FlightIsAssignedPlane += FlightIsAssignedPlaneHandler;
                flight.FlightIsAssignedTaxi += FlightIsAssignedTaxiHandler;
                flight.FlightIsAssignedRunway += FlightIsAssignedRunwayHandler;
                flight.FlightHasTakenOff += FlightHasTakenOffHandler;
                flight.FlightHasLanded += FlightHasLandedHandler;
                flight.FlightHasBegunOnboarding += FlightHasBegunOnboardingHandler;
                flight.FlightHasFinishedOnboarding += FlightHasFinishedOnboardingHandler;
                flight.FlightHasBegunOffloading += FlightHasBegunOffloadingHandler;
                flight.FlightHasFinishedOffloading += FlightHasFinishedOffloadingHandler;
                flight.FlightHasChangedStatus += FlightHasChangedStatusHandler;
                flight.FlightHasStartedTraveling += FlightHasStartedTravelingHandler;
                flight.FlightHasParkedAtGate += FlightHasParkedAtGateHandler;
            }
        }

        private void FlightHasParkedAtGateHandler(object? sender, FlightPlaneGateArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has parked at gate: {e.gate.GateName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightHasStartedTravelingHandler(object? sender, FlightPlaneTaxiArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has left the gate: {e.flight.AssignedGate.GateName} and started taxiing on taxiway: {e.taxi.TaxiName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightHasChangedStatusHandler(object? sender, FlightPlaneArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has changed its status to: {e.flight.Status} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightHasFinishedOffloadingHandler(object? sender, FlightPlaneGateArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has finished offloading at gate {e.gate.GateName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightHasBegunOffloadingHandler(object? sender, FlightPlaneGateArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has begun offloading at gate {e.gate.GateName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightHasFinishedOnboardingHandler(object? sender, FlightPlaneGateArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has finished onboarding at gate {e.gate.GateName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightHasBegunOnboardingHandler(object? sender, FlightPlaneGateArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has begun onboarding at gate {e.gate.GateName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightHasLandedHandler(object? sender, FlightPlaneRunwayArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has landed at runway {e.runway.RunwayName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightHasTakenOffHandler(object? sender, FlightPlaneRunwayArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has taken off from runway {e.runway.RunwayName} towards {e.flight.DestinationAirport.AirportName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightIsAssignedRunwayHandler(object? sender, FlightPlaneRunwayArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has been assigned runway {e.runway.RunwayName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightIsAssignedTaxiHandler(object? sender, FlightPlaneTaxiArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has been assigned taxi {e.taxi.TaxiName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightIsAssignedPlaneHandler(object? sender, FlightPlaneArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has been assigned plane {e.flight.AssignedPlane.TailNumber} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void FlightIsAssignedGateHandler(object? sender, FlightPlaneGateArgs e)
        {
            string eventString = $"Flight {e.flight.Number} has been assigned gate {e.gate.GateName} at day: {ElapsedDays}, time: {ElapsedHours.ToString("D2")}:{ElapsedMinutes.ToString("D2")}";
            AddEventToList(eventString);
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
