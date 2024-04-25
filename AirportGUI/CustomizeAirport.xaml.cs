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
using AirportSimulation;
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

        public CustomizeAirport(Airport airport)
        {
            InitializeComponent();
            _airport = airport;
            this.DataContext = new CustomizeAirportViewModel(_airport);

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

        //Denne må fikses
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
            {
                viewModel.CreateGateCommand.Execute(parameter);
            }
                
            else
            {
                MessageBox.Show("Cannot create gate at this time");
            }
                

        }

        public void ConfigureTerminalsButton_Click(object sender, RoutedEventArgs e)
        {
            
            var viewModel = DataContext as CustomizeAirportViewModel;

            var selectedItem = viewModel.SelectedItem as Terminal;

            if(selectedItem == null)
            {
                MessageBox.Show("Please select a terminal");
            }

            else
            {
                ConfigureTerminals configureTerminals = new ConfigureTerminals(_airport, selectedItem);
                NavigationManager.Navigate(configureTerminals);
            }

        }

        public void ConfigureGatesButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CustomizeAirportViewModel;
            var selectedItem = viewModel.SelectedItem as Gate;

            if (selectedItem == null)
            {
                MessageBox.Show("Please select a terminal");
            }

            else
            {
                ConfigureGates configureGates = new ConfigureGates(_airport, selectedItem);
                NavigationManager.Navigate(configureGates);
            }
            
        }

        public void ConfigureRunwaysButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CustomizeAirportViewModel;
            var selectedItem = viewModel.SelectedItem as Runway;

            if (selectedItem == null)
            {
                MessageBox.Show("Please select a terminal");
            }

            else
            {
                ConfigureRunways configureRunway = new ConfigureRunways(_airport, selectedItem);
                NavigationManager.Navigate(configureRunway);
            }
            
        }

        public void ConfigureTaxiwaysButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as CustomizeAirportViewModel;
            var selectedItem = viewModel.SelectedItem as Taxi;

            if (selectedItem == null)
            {
                MessageBox.Show("Please select a terminal");
            }

            else
            {
                ConfigureTaxiways configureTaxiways = new ConfigureTaxiways(_airport, selectedItem);
                NavigationManager.Navigate(configureTaxiways);
            }
            
        }

        public void SetUpPlanesFlightsButton_Click(object sender, RoutedEventArgs e)
        {
            MovableParts movableParts = new MovableParts(_airport);
            NavigationManager.Navigate(movableParts);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
    }
}
