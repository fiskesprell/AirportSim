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
using AirportSimulation;

namespace AirportGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Stack<Page> navigationStack = new Stack<Page>();

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

        public void Navigate(Page page)
        {
            if (this.Content != null && this.Content is Page)
            {
                navigationStack.Push(this.Content as Page);
            }
            this.Content = page;
        }

        public void NavigateBack()
        {
            if (navigationStack.Any())
            {
                Page previousPage = navigationStack.Pop();
                this.Content = previousPage;
            }

            else
            {
                this.Content = new MainWindow();
            }
        }
    }
}
