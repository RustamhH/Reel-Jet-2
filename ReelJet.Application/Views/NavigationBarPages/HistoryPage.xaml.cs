using System;
using System.Windows.Controls;
using Reel_Jet.ViewModels.NavigationBarPageModels;

namespace Reel_Jet.Views.MoviePages {
    public partial class HistoryPage : Page {
        public HistoryPage(Frame frame) {
            InitializeComponent();
            DataContext = new HistoryPageModel(frame);
        }
    }
}
