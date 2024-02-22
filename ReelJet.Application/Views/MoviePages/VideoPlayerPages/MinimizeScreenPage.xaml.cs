using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using ReelJet.Database.Entities.Abstracts;
using Reel_Jet.ViewModels.MoviePageModels.VideoPlayerPageModels;

namespace Reel_Jet.Views.MoviePages.VideoPlayerPages {
    public partial class MinimizeScreenPage : Page {
        public MinimizeScreenPage(Frame frame, BaseMovie movie, ObservableCollection<Reel_Jet.Models.MovieNamespace.Option> options, string videoUrl, string videoPgUrl, string? movietype = "film") {

            InitializeComponent();

            FullScreenPage fullScreenPage = new FullScreenPage(frame, movie, videoUrl, movietype);
            PlayerFrame.Content = fullScreenPage;

            DataContext = new MinimizeScreenPageModel(frame, movie, (fullScreenPage.DataContext as FullScreenPageModel)!.getPlayer(), 
                PlayerFrame, options, videoUrl, videoPgUrl, movietype);

            if (movietype != "film") ServersSection.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
