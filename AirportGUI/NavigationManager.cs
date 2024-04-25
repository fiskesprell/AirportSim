using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Runtime.CompilerServices;

namespace AirportGUI
{
    public static class NavigationManager
    {
        private static Stack<Page> navigationStack = new Stack<Page>();
        private static Window mainWindow = Application.Current.MainWindow;

        public static void Navigate(Page page)
        {
            if (mainWindow.Content != null && mainWindow.Content is Page)
            {
                navigationStack.Push(mainWindow.Content as Page);
            }
            mainWindow.Content = page;
        }

        public static void NavigateBack()
        {
            if (navigationStack.Any())
            {
                mainWindow.Content = navigationStack.Pop();
            }
            else
            {
                Application.Current.MainWindow.Close();

                MainWindow newMainWindow = new MainWindow();
                Application.Current.MainWindow = newMainWindow;
                newMainWindow.Show();
            }
        }
    }
}
