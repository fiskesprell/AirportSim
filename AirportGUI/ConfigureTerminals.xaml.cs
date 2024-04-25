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
    /// Interaction logic for ConfigureTerminals.xaml
    /// </summary>
    public partial class ConfigureTerminals : Page
    {
        private Airport _airport;
        private Terminal _terminal;
        public ConfigureTerminals(Airport airport, Terminal terminal)
        {
            InitializeComponent();
            _airport = airport;
            _terminal = terminal;

            SetDataContext(airport, terminal);
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

        private void UpdateTerminalButton_Click(object sender, RoutedEventArgs e)
        {
            bool isDomestic = DomesticButton.IsChecked == true;
            bool isInternational = InternationalButton.IsChecked == true;
            bool isBoth = BothButton.IsChecked == true;
            if (isInternational)
            {
                _terminal.StrictlyInternational = true;
            }

            if (isBoth) 
            {
                _terminal.ReadyForInternational = true;
            }

            if (isDomestic)
            {
                _terminal.ReadyForInternational = false;
            }

        }

        private void AddGateToTerminal_Click(object sender, RoutedEventArgs e)
        {
            string gateName = GateNameTextBox.Text;

            foreach(var gate in _airport.AllGates)
            {
                if (gate.GateName.Equals(gateName))
                {
                    _terminal.AddExistingGate(gate);
                }
            } 

        }

        private void SetDataContext(Airport airport, Terminal terminal)
        {
            var myDataContext = new TerminalAirportDataContext
            {
                Airport = airport,
                Terminal = terminal
            };

            DataContext = myDataContext;
        }
    }
}
