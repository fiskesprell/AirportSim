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
    }
}
