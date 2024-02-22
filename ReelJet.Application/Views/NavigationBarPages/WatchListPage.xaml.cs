using System;
using System.Windows;
using System.Windows.Controls;
using Reel_Jet.ViewModels.NavigationBarPageModels;

namespace Reel_Jet.Views.NavigationBarPages { 
    public partial class WatchListPage : Page {

        // Private Fields

        private Frame MainFrame;

        // Constructor

        public WatchListPage(Frame frame) {
            InitializeComponent();
            MainFrame = frame;
            DataContext = new WatchListPageModel(MainFrame);
        }
    }
}