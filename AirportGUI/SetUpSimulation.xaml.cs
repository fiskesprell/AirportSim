using AirportSimulation;
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

        private void PrepareSimulation_Click(object sender, RoutedEventArgs e)
        {
            string stringStartDate = StartdateTextBox.Text;
            string stringEndDate = EnddateTextBox.Text;

            if (!TryParseStart(stringStartDate, out DateTime startDate) || !TryParseEnd(stringEndDate, out DateTime endDate))
            {
                throw new ArgumentException("Couldnt convert dates");
            }

            TimeConfigManager tgm = new TimeConfigManager();
            TimeSimulation ts = new TimeSimulation();


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

        private bool TryParseEnd(string end, out DateTime startDate)
        {
            startDate = DateTime.MinValue;
            string[] endDates = end.Split('.');


            if (endDates.Length == 3)
            {
                if (int.TryParse(endDates[0], out int day) && int.TryParse(endDates[1], out int month) &&
                int.TryParse(endDates[2], out int year))
                {
                    startDate = new DateTime(year, month, day);
                    return true;
                }
            }
            return false;
        }

        private void FlightsEventsChecked(object sender, RoutedEventArgs e)
        {
            //Få til å subscribe på events her
            //Få til en smart måte å enten returnere hele objektet
            //eller printe ut på skjermen i "real-time"
        }

        private void FlightsEventsUnchecked(object sender, RoutedEventArgs e)
        {
            //Ikke subscribe på events
        }

        private void PlanesEventsChecked(object sender, RoutedEventArgs e)
        {

        }

        private void PlanesEventsUnchecked(object sender, RoutedEventArgs e)
        {

        }

        private void TaxisEventsChecked(object sender, RoutedEventArgs e)
        {

        }

        private void TaxisEventsUnchecked(object sender, RoutedEventArgs e)
        {

        }

        private void RunwaysEventsChecked(object sender, RoutedEventArgs e)
        {

        }

        private void RunwaysEventsUnchecked(object sender, RoutedEventArgs e)
        {

        }

        private void TerminalsEventsChecked(object sender, RoutedEventArgs e)
        {

        }

        private void TerminalsEventsUnchecked(object sender, RoutedEventArgs e)
        {

        }

        private void GatesEventsChecked(object sender, RoutedEventArgs e)
        {

        }

        private void GatesEventsUnchecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
