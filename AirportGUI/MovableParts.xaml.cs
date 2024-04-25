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
using AirportSimulation;
using NetzachTech.AirportSim.Infrastructure;

namespace AirportGUI
{
    
    /// <summary>
    /// Interaction logic for MovableParts.xaml
    /// </summary>
    public partial class MovableParts : Page
    {
        private Airport _airport;
        public MovableParts(Airport airport)
        {
            _airport = airport;
            InitializeComponent();
            this.DataContext = new MovablePartsViewModel(_airport);
        }

        private void CreateFlightButton_Click(object sender, RoutedEventArgs e)
        {
            bool isIncoming = IncomingRadioButton.IsChecked == true;
            bool isOutgoing = OutgoingRadioButton.IsChecked == true;
            string flightNumber = FlightNumberTextBox.Text;
            string flightDate = FlightDateTextBox.Text;
            string destination = FlightDestinationTextBox.Text;
            string flightTime = FlightTimeTextBox.Text;

            

            FlightCreationParameters parameters = new FlightCreationParameters
            {
                FlightNumber = flightNumber,
                FlightDate = flightDate,
                FlightTime = flightTime,
                Destination = destination,
                IsIncoming = isIncoming
            };

            var viewModel = DataContext as MovablePartsViewModel;
            if (viewModel == null)
                return;

            if (viewModel.CreateFlightCommand.CanExecute(parameters))
                viewModel.CreateFlightCommand.Execute(parameters);


        }

        private void CreatePlaneButton_Click(Object sender, RoutedEventArgs e) 
        {
            string planeName = PlaneNameTextBox.Text;
            string planeModel = PlaneModelTextBox.Text;
            string tailnumber = PlaneTailnumberTextBox.Text;

            PlaneCreationParameters parameters = new PlaneCreationParameters
            {
                PlaneName = planeName,
                PlaneModel = planeModel,
                Tailnumber = tailnumber
            };

            var viewModel = DataContext as MovablePartsViewModel;
            if (viewModel == null)
                return;

            if (viewModel.CreatePlaneCommand.CanExecute(parameters))
            {
                viewModel.CreatePlaneCommand.Execute(parameters);
            }
                
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
