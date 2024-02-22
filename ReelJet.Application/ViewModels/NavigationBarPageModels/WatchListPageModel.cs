using System;
using System.Linq;
using System.Windows;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using ReelJet.Database.Entities;
using System.Collections.ObjectModel;
using Reel_Jet.Views.NavigationBarPages;
using ReelJet.Database.Entities.Concretes;
using ReelJet.Application.Views.MoviePages;
using static ReelJet.Application.Models.DatabaseNamespace.Database;


namespace Reel_Jet.ViewModels.NavigationBarPageModels {
    public class WatchListPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ICommand? ForYouPageCommand { get; set; }
        public ICommand? ProfilePgButtonCommand { get; set; }
        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? SettingsPgButtonCommand { get; set; }
        public ICommand? MovieListPgButtonCommand { get; set; }
        public ICommand RemoveFromWatchListCommand { get; set; }
        public ICommand WatchMovieFromWatchListCommand { get; set; }
        public ObservableCollection<Movie> MyWatchList { get; set; }
        public Reel_Jet.Models.MovieNamespace.ShortMovieInfo? MovieInfo { get; set; }

        // Constructor

        public WatchListPageModel(Frame frame) {

            MainFrame = frame;

            WriteWatchlist();
            SetCommands();
        }

        // Functions

        private void MovieListPage(object? sender) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void ProfilePage(object? sender) {
            MainFrame.Content = new UserAccountPage(MainFrame);
        }

        private void SettingsPage(object? sender) {
            MainFrame.Content = new SettingsPage(MainFrame);
        }

        private void ForYouPage(object? sender) {
            MainFrame.Content = new ForYouPage(MainFrame);
        }

        private void WriteWatchlist() {

            MyWatchList = WatchLists;
        }

        private void SetCommands() {

            ForYouPageCommand = new RelayCommand(ForYouPage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            MovieListPgButtonCommand = new RelayCommand(MovieListPage);
            RemoveFromWatchListCommand = new RelayCommand(RemoveFromWatchList);
            WatchMovieFromWatchListCommand = new RelayCommand(WatchMovieFromWatchList);
        }

        private void WatchMovieFromWatchList(object? sender) {
 
            try {
                var Movie = (sender as ReelJet.Database.Entities.Movie)!;
                ReelJet.Database.Entities.Concretes.UserHistoryList historyList = new() {
                    UserId = CurrentUser.Id,
                    Movie = Movie,
                    MovieId = Movie.Id,
                    User = CurrentUser,
                };
 
                if(!DbContext.HistoryLists.Any(entity => entity.UserId == historyList.UserId && entity.Movie.imdbID==historyList.Movie.imdbID)) {
                    DbContext.HistoryLists.Add(historyList);
                    DbContext.SaveChanges();
                }
                
                MainFrame.Content = new VideoPlayerPage(MainFrame, Movie);
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void RemoveFromWatchList(object? sender) {

            ReelJet.Database.Entities.Movie a = (sender as ReelJet.Database.Entities.Movie)!;
            MyWatchList.Remove(a);
            UserWatchList? deleteditem = null;

            foreach (var item in CurrentUser.WatchList.ToList())
                if (item.MovieId == a.Id) deleteditem = item;

            DbContext.WatchLists.Remove(deleteditem!);
            DbContext.SaveChanges();
        }
    }
}