using System;
using System.Windows;
using Reel_Jet.ViewModels;
using System.Windows.Input;
using Reel_Jet.Views.MoviePages;

namespace Reel_Jet.Views {
    public partial class MainView : Window {

        private WindowState prevstate;

        public MainView() {
            InitializeComponent();
            DataContext = new MainViewModel(MainFrame);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.F11) {
                if (WindowStyle == WindowStyle.SingleBorderWindow) {
                    WindowStyle = WindowStyle.None;
                    prevstate = WindowState;
                    WindowState = WindowState.Maximized;

                    Visibility = Visibility.Collapsed;
                    Topmost = true;
                    Visibility = Visibility.Visible;
                }
                else {
                    WindowState = prevstate;
                    WindowStyle = WindowStyle.SingleBorderWindow;
                    Topmost = false;
                }
            }
        }
    }
}
