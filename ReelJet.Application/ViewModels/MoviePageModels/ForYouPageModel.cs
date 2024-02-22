using System;
using System.Linq;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Collections.ObjectModel;
using Reel_Jet.Views.NavigationBarPages;
using ReelJet.Database.Entities.Concretes;
using ReelJet.Application.Models.EntityAdapters;
using ReelJet.Application.Models.DatabaseNamespace;
using static ReelJet.Application.Models.DatabaseNamespace.Database;


namespace ReelJet.Application.ViewModels.MoviePageModels {
    public class ForYouPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ObservableCollection<PersonalMovieAdapter> PersonalMovies { get; set; } = new();
        public ICommand WatchListPgButtonCommand { get; set; }
        public ICommand MovieListPgButtonCommand { get; set; }
        public ICommand SettingsPgButtonCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand HistoryPgButtonCommand { get; set; }
        public ICommand ProfilePgButtonCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        // Constructor

        public ForYouPageModel(Frame frame) { 
            MainFrame = frame;

            SetCommands();
            var personalMovies = DbContext.PersonalMovies.ToList();
            foreach (var movie in personalMovies) {

                PersonalMovieAdapter movieAdapter = new() {
                    Id = movie.Id,
                    Avatar = UserAuthentication.LoadImage(movie.User.Avatar!),
                    Poster = UserAuthentication.LoadImage(movie.Poster),
                    Description = movie.Description,
                    UploadDate = movie.UploadDate,
                    MovieLink = movie.MovieLink,
                    ViewCount = movie.ViewCount,
                    LikeCount = movie.LikeCount,
                    Minute = movie.Minute,
                    Title = movie.Title,
                    Hour = movie.Hour,
                    User = movie.User,
                };
                PersonalMovies.Add(movieAdapter);
            }
        }

        // Functions

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private void MovieListPage(object? sender) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }

        private void ProfilePage(object? sender) {
            MainFrame.Content = new UserAccountPage(MainFrame);
        }

        private void SettingsPage(object? sender) { 
            MainFrame.Content = new SettingsPage(MainFrame);
        }

        private void SelectionChanged(object? param) {

            PersonalMovieAdapter personalMovieAdapter = (PersonalMovieAdapter)param;
            PersonalMovie personalMovie = DbContext.PersonalMovies.Where(movie => movie.Id == personalMovieAdapter.Id).First();
            MainFrame.Content = new VideoPlayerPage(MainFrame, personalMovie, "personalmovie");
        }

        private void Search(object? param) {

        }

        private void SetCommands() {

            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            MovieListPgButtonCommand = new RelayCommand(MovieListPage);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);
            SearchCommand = new RelayCommand(Search);
        }

    }
}
