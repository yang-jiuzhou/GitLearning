using HBBio.WindowSize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HBBio
{
    public partial class DicEvent : ResourceDictionary
    {
        int i = 0;

        private void Title_MouseDown(object sender, MouseButtonEventArgs e)
        {
            i += 1;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            timer.Tick += (s, e1) => { timer.IsEnabled = false; i = 0; };
            timer.IsEnabled = true;
            if (i % 2 == 0)
            {
                timer.IsEnabled = false;
                i = 0;
                Window targetWindow = Window.GetWindow((Border)sender);
                targetWindow.WindowState = targetWindow.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            }
        }

        public void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Window targetWindow = Window.GetWindow((Border)sender);
                targetWindow.DragMove();
            }
        }

        public void ButtonMin_Click(object sender, RoutedEventArgs e)
        {
            Window targetWindow = Window.GetWindow((Button)sender);
            targetWindow.WindowState = WindowState.Minimized;
        }

        public void ButtonMax_Click(object sender, RoutedEventArgs e)
        {
            Window targetWindow = Window.GetWindow((Button)sender);
            if (WindowState.Normal == targetWindow.WindowState)
            {
                targetWindow.WindowState = WindowState.Maximized;
                targetWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                targetWindow.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            }
            else
            {
                targetWindow.WindowState = WindowState.Normal;
            }
        }

        public void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window targetWindow = Window.GetWindow((Button)sender);
            targetWindow.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window targetWindow = Window.GetWindow((Border)sender);

            WindowSizeManager manager = new WindowSizeManager();
            double x = 0;
            double y = 0;
            double height = 0;
            double width = 0;
            if (manager.GetWindowSize(targetWindow.Title.Split('-')[0], out x, out y, out height, out width))
            {
                if (WindowSizeManager.s_RememberSize)
                {
                    if (SizeToContent.Height != targetWindow.SizeToContent && !targetWindow.Title.Equals(Share.ReadXaml.GetResources("labTips")))
                    {
                        targetWindow.SizeToContent = SizeToContent.Manual;
                    }
                    targetWindow.WindowStartupLocation = WindowStartupLocation.Manual;
                    targetWindow.Left = x;
                    targetWindow.Top = y;
                    targetWindow.Height = height;
                    targetWindow.Width = width;
                }
            }
            else
            {
                manager.AddWindowSize(targetWindow.Title.Split('-')[0], targetWindow.Left, targetWindow.Top, targetWindow.Height, targetWindow.Width);
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            Window targetWindow = Window.GetWindow((Border)sender);

            WindowSizeManager manager = new WindowSizeManager();
            manager.UpdateWindowSize(targetWindow.Title.Split('-')[0], targetWindow.Left, targetWindow.Top, targetWindow.Height, targetWindow.Width);
        }
    }
}
