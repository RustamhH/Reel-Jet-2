using System;
using System.Windows.Controls;
using Reel_Jet.ViewModels.MoviePageModels;
using ReelJet.Database.Entities.Abstracts;

namespace Reel_Jet.Views.MoviePages {

    public partial class VideoPlayerPage : Page {
        public VideoPlayerPage(Frame frame, BaseMovie movie, string? movitetype = "film") {
            InitializeComponent();
            DataContext = new VideoPlayerPageModel(frame, movie, Player, movitetype);
        }
    }
}
