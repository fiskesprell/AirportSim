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

namespace AirportGUI
{
    /// <summary>
    /// Interaction logic for ConfigureTaxiways.xaml
    /// </summary>
    public partial class ConfigureTaxiways : Page
    {
        private Airport _airport;
        public ConfigureTaxiways(Airport airport)
        {
            InitializeComponent();
            _airport = airport;
            InitializeViewModel(airport);
        }

        public void InitializeViewModel(Airport airport)
        {
            this.DataContext = new CustomizeAirportViewModel(airport);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateBack();

        }
    }
}
