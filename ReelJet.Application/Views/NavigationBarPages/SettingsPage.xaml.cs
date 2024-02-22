using System;
using System.Windows;
using System.Windows.Controls;
using Reel_Jet.ViewModels.NavigationBarPageModels;


namespace Reel_Jet.Views.NavigationBarPages {
    public partial class SettingsPage : Page {
        public SettingsPage(Frame frame) {
            InitializeComponent();
            DataContext = new SettingsPageModel(frame, SettingsPageFrame);
        }
    }
}
