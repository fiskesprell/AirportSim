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
using NetzachTech.AirportSim.Infrastructure;

namespace AirportGUI
{
    /// <summary>
    /// Interaction logic for ConfigureTaxiways.xaml
    /// </summary>
    public partial class ConfigureTaxiways : Page
    {
        public ConfigureTaxiways(Airport airport)
        {
            InitializeComponent();
            InitializeViewModel(airport);
        }

        public void InitializeViewModel(Airport airport)
        {
            this.DataContext = new CustomizeAirportViewModel(airport);
        }
    }
}
