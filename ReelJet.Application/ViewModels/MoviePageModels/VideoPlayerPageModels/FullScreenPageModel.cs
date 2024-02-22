using System;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using ReelJet.Database.Entities;
using Reel_Jet.Views.MoviePages;
using Microsoft.Web.WebView2.Wpf;
using ReelJet.Database.Entities.Abstracts;
using ReelJet.Database.Entities.Concretes;
using Reel_Jet.Views.MoviePages.VideoPlayerPages;


namespace Reel_Jet.ViewModels.MoviePageModels.VideoPlayerPageModels {
    public class FullScreenPageModel {

        // Private Fields

        private Movie Movie;
        private WebView2 Player;
        private Frame MainFrame;
        private BaseMovie BaseMovie;
        private PersonalMovie PersonalMovie;
        private string MovieType;

        // Binding Properties

        public ICommand MinimizeScreenButtonCommand { get; set; }
        public string VideoUrl { get; set; }

        // Constructor

        public FullScreenPageModel(Frame frame, BaseMovie basemovie, WebView2 player, string videourl, string? movietype = "film") {

            Player = player;
            MainFrame = frame;
            VideoUrl = videourl;
            BaseMovie = basemovie;
            MovieType = movietype;

            MinimizeScreenButtonCommand = new RelayCommand(MinimizePage);

        }

        // Functions


        private void MinimizePage(object? sender) {

            Frame videoPlayerFrame = ((MainFrame.Content as VideoPlayerPage)!.DataContext as VideoPlayerPageModel)!.VideoPlayerFrame;
            Uri uri = new Uri("https://www.google.com/");
            Player.Source = uri;

            MinimizeScreenPage minimizeScreen = (((MainFrame.Content as VideoPlayerPage)!.DataContext as VideoPlayerPageModel)!.PrevFrame as MinimizeScreenPage)!;
            videoPlayerFrame.NavigationService.Navigate(minimizeScreen);

            minimizeScreen.PlayerFrame.Content = new FullScreenPage(MainFrame, BaseMovie, VideoUrl, MovieType);
        }

        public WebView2 getPlayer() {
            return Player;
        }
    }
}
