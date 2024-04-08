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
            this.DataContext = new CustomizeAirportViewModel(_airport);

        }

        public void InitializeAirport(Airport airport)
        {
            _airport = airport;
        }

        public void CreateTerminalButton_Click(object sender, RoutedEventArgs e)
        {
            string terminalName = TerminalNameTextBox.Text;
            var viewModel = DataContext as CustomizeAirportViewModel;
            if (viewModel == null)
                return;

            if(viewModel.CreateTerminalCommand.CanExecute(terminalName))
                viewModel.CreateTerminalCommand.Execute(terminalName);

        }

        public void CreateTaxiButton_Click(object sender, RoutedEventArgs e)
        {
            string taxiName = TaxiNameTextBox.Text;
            var viewModel = DataContext as CustomizeAirportViewModel;
            if (viewModel == null)
                return;

            if (viewModel.CreateTaxiwayCommand.CanExecute(taxiName))
                viewModel.CreateTaxiwayCommand.Execute(taxiName);


        }

        public void CreateRunwayButton_Click(object sender, RoutedEventArgs e)
        {
            string runwayName = RunwayNameTextBox.Text;
            var viewModel = DataContext as CustomizeAirportViewModel;
            if (viewModel == null)
                return;

            if (viewModel.CreateRunwayCommand.CanExecute(runwayName))
                viewModel.CreateRunwayCommand.Execute(runwayName);

        }

        public void CreateGateButton_Click(object sender, RoutedEventArgs e)
        {
            string gateName = GateNameTextBox.Text;
            string terminalName = Terminal2NameTextBox.Text;


            Gate gate = new Gate(gateName);

            var viewModel = DataContext as CustomizeAirportViewModel;
            if (viewModel == null)
            {
                MessageBox.Show("Viewmodel not found");
                return;
            }

            Terminal selectedTerminal = viewModel.Airport.AllTerminals.FirstOrDefault(t => t.TerminalName == terminalName);

            
            if (selectedTerminal != null) 
            {
                MessageBox.Show("Terminal not found");
                return;
            }

            var parameter = new GateCreationInfo(gateName, selectedTerminal);

            if (viewModel.CreateGateCommand.CanExecute(parameter))
                viewModel.CreateGateCommand.Execute(parameter);
            else
                MessageBox.Show("Cannot create gate at this time");

        }

        public void ConfigureTerminalsButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigureTerminals configureTerminals = new ConfigureTerminals(_airport);
            configureTerminals.InitializeViewModel(_airport);
            this.Content = configureTerminals;
        }

        public void ConfigureGatesButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigureGates configureGates = new ConfigureGates(_airport);
            configureGates.InitializeViewModel(_airport);
            this.Content = configureGates;
        }

        public void ConfigureRunwaysButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigureRunways configureRunways = new ConfigureRunways(_airport);
            configureRunways.InitializeViewModel(_airport);
            this.Content= configureRunways;
        }

        public void ConfigureTaxiwaysButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigureTaxiways configureTaxiways = new ConfigureTaxiways(_airport);
            configureTaxiways.InitializeViewModel(_airport);
            this.Content = ConfigureTaxiways;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


}
}
