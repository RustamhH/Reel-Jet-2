using System;
using System.Windows;
using System.Windows.Controls;
using Reel_Jet.ViewModels.NavigationBarPageModels.SettingsPageModels;


namespace Reel_Jet.Views.NavigationBarPages.SettingsPages {
    public partial class AccountPage : Page {
        public AccountPage() {
            InitializeComponent();
            DataContext = new AccountPageModel();
        }
    }
}
