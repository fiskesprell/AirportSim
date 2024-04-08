using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using NetzachTech.AirportSim.Infrastructure;

namespace AirportGUI
{
    
    /// <summary>
    /// Interaction logic for CustomizeAirport.xaml
    /// </summary>
    public partial class CustomizeAirport : Page
    {
        private Airport _airport;
        public Airport Airport
        {
            get { return _airport; }
            set { _airport = value; NotifyPropertyChanged(); }
        }
        public string AirportName => _airport?.AirportName;

        public CustomizeAirport()
        {
            InitializeComponent();
            System.Diagnostics.Debug.WriteLine("CustomizeAirport page constructed.");

        }

        public void InitializeAirport(Airport airport)
        {
            _airport = airport;
            this.DataContext = this;
        }

        public void CreateTerminalButton_Click(object sender, RoutedEventArgs e)
        {
            string terminalName = TerminalNameTextBox.Text;
            Terminal terminal = new Terminal(terminalName);
            _airport.AddExistingTerminal(terminal);
            NotifyPropertyChanged(nameof(Airport));

        }

        public void CreateTaxiButton_Click(object sender, RoutedEventArgs e)
        {
            string taxiName = TaxiNameTextBox.Text;
            Taxi taxi = new Taxi(taxiName, TaxiwayType.Main);
            _airport.AddExistingTaxi(taxi);
            NotifyPropertyChanged(nameof(Airport));


        }

        public void CreateRunwayButton_Click(object sender, RoutedEventArgs e)
        {
            string runwayName = RunwayNameTextBox.Text;
            Runway runway = new Runway(runwayName);
            _airport.AddExistingRunway(runway);
            NotifyPropertyChanged(nameof(Airport));

        }

        public void CreateGateButton_Click(object sender, RoutedEventArgs e)
        {
            string gateName = GateNameTextBox.Text;
            Gate gate = new Gate(gateName);

            string terminalName = Terminal2NameTextBox.Text;

            if (_airport.AllTerminals.Count == 0)
                MessageBox.Show("No terminals in this airport");

            foreach (var terminal in _airport.AllTerminals)
            {
                
                if (terminal.TerminalName == terminalName)
                    terminal.AddExistingGate(gate);
            }
            NotifyPropertyChanged(nameof(Airport));

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


}
}
