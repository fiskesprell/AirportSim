using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;

namespace AirportGUI
{
    class CustomizeAirportViewModel : INotifyPropertyChanged
    {
        private Airport _airport;
        public Airport Airport
        {
            get { return _airport; }
            set { _airport = value; OnPropertyChanged(); }
        }
        public string AirportName => _airport?.AirportName;
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand CreateTerminalCommand { get; private set; }
        public ICommand CreateGateCommand { get; private set; }
        public ICommand CreateRunwayCommand { get; private set; }
        public ICommand CreateTaxiwayCommand { get; private set; }

        private object _selectedItem;
        public object SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public CustomizeAirportViewModel(Airport airport)
        {
            _airport = airport;

            CreateTerminalCommand = new RelayCommand(CreateTerminalAction);
            CreateGateCommand = new RelayCommand(CreateGateAction);
            CreateRunwayCommand = new RelayCommand(CreateRunwayAction);
            CreateTaxiwayCommand = new RelayCommand(CreateTaxiwayAction);
        }

        public void CreateTerminalAction(object parameter)
        {
            if (parameter is string name)
            {
                CreateTerminal(name);
            }
        }

        public void CreateTerminal(string name)
        {
            Terminal terminal = new Terminal(name);
            _airport.AddExistingTerminal(terminal);
            OnPropertyChanged(nameof(Airport));

        }

        public void CreateGateAction(object parameter)
        {
            if (parameter is string name)
            {
                CreateGate(name);
            }
        }

        public void CreateGate(string name)
        {
            Gate gate = new Gate(name);
            _airport.AddExistingGate(gate);
            OnPropertyChanged(nameof(Airport));

        }

        public void CreateRunwayAction(object parameter)
        {
            if (parameter is string name)
            {
                CreateRunway(name);
            }
        }

        public void CreateRunway(string name)
        {
            Runway runway = new Runway(name);
            _airport.AddExistingRunway(runway);
            OnPropertyChanged(nameof(Airport));

        }

        public void CreateTaxiwayAction(object parameter)
        {
            if (parameter is string name)
            {
                CreateTaxiway(name);
            }
        }

        public void CreateTaxiway(string name)
        {
            Taxi taxi = new Taxi(name, TaxiwayType.Main);
            _airport.AddExistingTaxi(taxi);
            OnPropertyChanged(nameof(Airport));

        }

        

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
