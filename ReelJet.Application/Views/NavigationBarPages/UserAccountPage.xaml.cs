using System;
using System.Windows;
using System.Windows.Controls;
using Reel_Jet.ViewModels.NavigationBarPageModels;

namespace Reel_Jet.Views.NavigationBarPages {
    public partial class UserAccountPage : Page {
        private Frame MainFrame;

        public UserAccountPage(Frame frame) {
            InitializeComponent();
            MainFrame = frame;
            DataContext = new UserAccountPageModel(MainFrame);
        }
    }
}
