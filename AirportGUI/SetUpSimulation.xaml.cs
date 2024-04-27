using AirportSimulation;
using NetzachTech.AirportSim.EventArguments;
using NetzachTech.AirportSim.Time;
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

namespace AirportGUI
{
    /// <summary>
    /// Interaction logic for SetUpSimulation.xaml
    /// </summary>
    public partial class SetUpSimulation : Page
    {
        private Airport _airport;
        private bool flightEvents = false;
        private bool gateEvents = false;
        private bool planeEvents = false;
        private bool taxiEvents = false;
        private bool runwayEvents = false;
        private bool terminalEvents = false;

        public SetUpSimulation(Airport airport)
        {
            InitializeComponent();
            _airport = airport;

            this.DataContext = _airport;
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

        private void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            string stringStartDate = StartdateTextBox.Text;
            string stringEndDate = EnddateTextBox.Text;
            DateTime endDate = DateTime.MinValue;

            if (!TryParseStart(stringStartDate, out DateTime startDate) || !TryParseEnd(stringEndDate, out endDate))
            {
                MessageBox.Show("Please write the dates in the correct format.");
                return;
            }

            TimeSimulation ts = new TimeSimulation();

            Simulation simulation = new Simulation(_airport, ts, startDate, endDate, flightEvents);
            NavigationManager.Navigate(simulation);

        }

        private bool TryParseStart(string start, out DateTime startDate)
        {
            startDate = DateTime.MinValue;
            string[] startDates = start.Split('.');


            if (startDates.Length == 3)
            {
                if (int.TryParse(startDates[0], out int day) && int.TryParse(startDates[1], out int month) &&
                int.TryParse(startDates[2], out int year))
                {
                    startDate = new DateTime(year, month, day);
                    return true;
                }
            }
            return false;
        }

        private bool TryParseEnd(string end, out DateTime endDate)
        {
            endDate = DateTime.MinValue;
            string[] endDates = end.Split('.');


            if (endDates.Length == 3)
            {
                if (int.TryParse(endDates[0], out int day) && int.TryParse(endDates[1], out int month) &&
                int.TryParse(endDates[2], out int year))
                {
                   endDate = new DateTime(year, month, day);
                   return true;
                }
            }
            return false;
        }



        private void FlightsEventsChecked(object sender, RoutedEventArgs e)
        {
            flightEvents = true;
        }


        private void FlightsEventsUnchecked(object sender, RoutedEventArgs e)
        {
            flightEvents = false;
        }

        private void PlanesEventsChecked(object sender, RoutedEventArgs e)
        {
            planeEvents = true;
        }

        private void PlanesEventsUnchecked(object sender, RoutedEventArgs e)
        {
            planeEvents = false;
        }

        private void TaxisEventsChecked(object sender, RoutedEventArgs e)
        {
            taxiEvents = true;
        }

        private void TaxisEventsUnchecked(object sender, RoutedEventArgs e)
        {
            taxiEvents = false;
        }

        private void RunwaysEventsChecked(object sender, RoutedEventArgs e)
        {
            runwayEvents = true;
        }

        private void RunwaysEventsUnchecked(object sender, RoutedEventArgs e)
        {
            runwayEvents = false;
        }

        private void TerminalsEventsChecked(object sender, RoutedEventArgs e)
        {
            terminalEvents = true;
        }

        private void TerminalsEventsUnchecked(object sender, RoutedEventArgs e)
        {
            terminalEvents = false;
        }

        private void GatesEventsChecked(object sender, RoutedEventArgs e)
        {
            gateEvents = true;
        }

        private void GatesEventsUnchecked(object sender, RoutedEventArgs e)
        {
            gateEvents = false;
        }
    }
}
