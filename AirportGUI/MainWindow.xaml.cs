﻿using NetzachTech.AirportSim.Infrastructure;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateAirportButton_Click(object sender, RoutedEventArgs e)
        {

            string airportName = AirportNameTextBox.Text;
            Airport airport = new Airport(airportName);

            CustomizeAirport customizeAirportPage = new CustomizeAirport();
            customizeAirportPage.InitializeAirport(airport);
            MainFrame.Navigate(customizeAirportPage);


        }
    }
}