using NetzachTech.AirportSim.Infrastructure;
using System.Windows.Navigation;
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
using System.Windows.Shapes;

namespace AirportGUI
{
    /// <summary>
    /// Interaction logic for MainWindo.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        public void CreateAirportButton_Click(object sender, RoutedEventArgs e) 
        {
            string airportName = AirportNameTextBox.Text;
            try
            {
                Airport airport = new Airport(airportName);

                CustomizeAirport customizeAirportPage = new CustomizeAirport(airport);
                this.Content = customizeAirportPage;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Creating Airport");
            }
            
        }

        public void Navigate(Page page)
        {
            this.Content = page;
        }
    }
}
