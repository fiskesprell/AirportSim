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
    /// Interaction logic for ConfigureRunways.xaml
    /// </summary>
    public partial class ConfigureRunways : Page
    {
        private Airport _airport;
        private Runway _runway;
        public ConfigureRunways(Airport airport, Runway runway)
        {
            InitializeComponent();
            _airport = airport;
            _runway = runway;

            SetDataContext(airport, runway);

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

        private void ConnectionButton_Click(Object sender, RoutedEventArgs e) 
        {
            string taxiName = TaxiNameTextBox.Text;
            Taxi selectedTaxi = _airport.FindTaxi(taxiName);

            _airport.ConnectTaxiAndRunway(selectedTaxi, _runway);

            TaxiNameTextBox.Text = "";
        }

        private void SetDataContext(Airport airport, Runway runway)
        {
            var myDataContext = new RunwayAirportDataContext
            {
                Airport = airport,
                Runway = runway
            };

            DataContext = myDataContext;
        }
    }
}
