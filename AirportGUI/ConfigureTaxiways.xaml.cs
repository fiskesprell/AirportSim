using System;
using System.Collections.Generic;
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
using AirportGUI.NetzachTech.AirportSim.DataContext;
using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;

namespace AirportGUI
{
    /// <summary>
    /// Interaction logic for ConfigureTaxiways.xaml
    /// </summary>
    public partial class ConfigureTaxiways : Page
    {
        private Airport _airport;
        private Taxi _taxi;
        public ConfigureTaxiways(Airport airport,Taxi taxi)
        {
            InitializeComponent();
            _airport = airport;
            _taxi = taxi;
            SetDataContext(airport, taxi);

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

        private void SetDataContext(Airport airport, Taxi taxi)
        {
            var myDataContext = new TaxiAirportDataContext
            {
                Airport = airport,
                Taxi = taxi
            };

            DataContext = myDataContext;
        }

        private void ConnectionRunwayButton_Click(object sender, RoutedEventArgs e)
        {
            string runwayName = RunwayNameTextBox.Text;
            Runway selectedRunway = _airport.FindRunway(runwayName);

            if (selectedRunway != null)
            {
                _airport.ConnectTaxiAndRunway(_taxi, selectedRunway);
            }
            else
            {
                MessageBox.Show(Application.Current.MainWindow, "There are no runways with that name in this airport", "Error", MessageBoxButton.OK);
            }

            RunwayNameTextBox.Text = "";
        }

        private void ConnectionGateButton_Click(object sender, RoutedEventArgs e)
        {
            string gateName = GateNameTextBox.Text;
            Gate selectedGate = _airport.FindGate(gateName);

            if (selectedGate != null)
            {
                _airport.ConnectGateAndTaxi(selectedGate, _taxi);
            }
            else
            {
                MessageBox.Show(Application.Current.MainWindow, "There are no gates with that name in this airport", "Error", MessageBoxButton.OK);
            }

            GateNameTextBox.Text = "";
        }
    }
}
