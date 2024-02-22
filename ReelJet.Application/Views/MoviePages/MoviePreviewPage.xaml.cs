using System;
using System.Windows.Controls;
using Reel_Jet.Models.MovieNamespace;
using Reel_Jet.ViewModels.MoviePageModels;

namespace Reel_Jet.Views.MoviePages {
    public partial class MoviePreviewPage : Page {
        public MoviePreviewPage(Frame frame, Movie movie) {
            InitializeComponent();
            DataContext = new MoviePreviewPageModel(frame, movie);
        }
    }
}
