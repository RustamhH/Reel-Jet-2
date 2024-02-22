using System;
using System.Windows.Controls;
using Reel_Jet.ViewModels.MoviePageModels;

namespace Reel_Jet.Views.MoviePages {
    public partial class MovieListPage : Page {
        public MovieListPage(Frame frame) {
            InitializeComponent();
            DataContext = new MoviewListPageModel(frame);
        }

    }
}