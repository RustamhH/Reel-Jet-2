using System;
using System.Windows.Controls;
using ReelJet.Database.Entities.Abstracts;
using Reel_Jet.ViewModels.MoviePageModels.VideoPlayerPageModels;


namespace Reel_Jet.Views.MoviePages.VideoPlayerPages {
    public partial class FullScreenPage : Page {

        private Frame Frame;
        private BaseMovie BaseMovie;
        private string VideoUrl;

        public FullScreenPage(Frame frame, BaseMovie movie, string videourl, string? movietype = "film") {
            InitializeComponent();
            DataContext = new FullScreenPageModel(frame, movie, Player, videourl, movietype);
            Frame = frame;
            VideoUrl = videourl;
        }
    }
}
