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
            if (TerminalNameTextBox.Text.Length < 2)
            {
                MessageBox.Show("Terminalnames must be at least 2 characters long");
                return;
            }

            string terminalName = TerminalNameTextBox.Text;
            var viewModel = DataContext as CustomizeAirportViewModel;
            if (viewModel == null)
                return;

            if(viewModel.CreateTerminalCommand.CanExecute(terminalName))
                viewModel.CreateTerminalCommand.Execute(terminalName);

            else
            {
                MessageBox.Show("Cannot create the terminal at this time");
            }

            TerminalNameTextBox.Text = "";

        }

        public void CreateTaxiButton_Click(object sender, RoutedEventArgs e)
        {
            if (TerminalNameTextBox.Text.Length < 2)
            {
                MessageBox.Show("Taxinames must be at least 2 characters long");
            }
            string taxiName = TaxiNameTextBox.Text;
            var viewModel = DataContext as CustomizeAirportViewModel;
            if (viewModel == null)
                return;

            if (viewModel.CreateTaxiwayCommand.CanExecute(taxiName))
                viewModel.CreateTaxiwayCommand.Execute(taxiName);

            else
            {
                MessageBox.Show("Cannot create the gate at this time");
            }

            TaxiNameTextBox.Text = "";


        }

        public void CreateRunwayButton_Click(object sender, RoutedEventArgs e)
        {
            if (RunwayNameTextBox.Text.Length < 2)
            {
                MessageBox.Show("Runwaynames must be at least 2 characters long");
            }
            string runwayName = RunwayNameTextBox.Text;
            var viewModel = DataContext as CustomizeAirportViewModel;
            if (viewModel == null)
                return;

            if (viewModel.CreateRunwayCommand.CanExecute(runwayName))
                viewModel.CreateRunwayCommand.Execute(runwayName);

            else
            {
                MessageBox.Show("Cannot create the runway at this time");
            }

            RunwayNameTextBox.Text = "";

        }

        public void CreateGateButton_Click(object sender, RoutedEventArgs e)
        {
            if (GateNameTextBox.Text.Length < 2)
            {
                MessageBox.Show("Gatenames must be at least 2 characters long");
                return;
            }

          
            string gateName = GateNameTextBox.Text;

            var viewModel = DataContext as CustomizeAirportViewModel;
            if (viewModel == null)
            {
                return;
            }

            if (viewModel.CreateGateCommand.CanExecute(gateName))
            {
                viewModel.CreateGateCommand.Execute(gateName);
            }
                
            else
            {
                MessageBox.Show("Cannot create the gate at this time");
            }

            GateNameTextBox.Text = "";

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
                MessageBox.Show("Please select a gate");
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
                MessageBox.Show("Please select a runway");
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
                MessageBox.Show("Please select a taxi");
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

        private void SimulationButton_Click(object sender, RoutedEventArgs e)
        {
            SetUpSimulation simulation = new SetUpSimulation(_airport);
            NavigationManager.Navigate(simulation);
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
