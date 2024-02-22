using System;
using System.Linq;
using System.Windows.Controls;
using ReelJet.Application.ViewModels.NavigationBarPageModels.SettingsPageModels;

namespace ReelJet.Application.Views.NavigationBarPages.SettingsPages;

public partial class UploadVideoPage : Page {
    public UploadVideoPage() {
        InitializeComponent();
        DataContext = new UploadVideoPageModel();
    }
}