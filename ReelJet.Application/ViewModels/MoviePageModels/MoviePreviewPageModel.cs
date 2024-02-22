using System;
using System.Linq;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.Models.MovieNamespace;
using System.Runtime.CompilerServices;
using Reel_Jet.Views.NavigationBarPages;
using ReelJet.Application.Views.MoviePages;
using static ReelJet.Application.Models.DatabaseNamespace.Database;



namespace Reel_Jet.ViewModels.MoviePageModels {
    public class MoviePreviewPageModel : INotifyPropertyChanged {

        // Private Fields

        private Movie? _movie;
        private Frame MainFrame;
        private bool isContain = false;
        private ReelJet.Database.Entities.Movie MovieAdapter = new();

        // Binding Properties

        public Movie Movie {
            get => _movie!;
            set {
                _movie = value;
            }
        }
        public string trailerLink { get; set; }
        public ICommand? ForYouPageCommand { get; set; }
        public ICommand? MovieListPageCommand { get; set; }
        public ICommand? VideoPlayerPageCommand { get; set; }
        public ICommand? ProfilePgButtonCommand { get; set; }
        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? SettingsPgButtonCommand { get; set; }
        public ICommand? WatchListPgButtonCommand { get; set; }

        // Constructor

        public MoviePreviewPageModel(Frame frame, Movie movie) { 
            
            MainFrame = frame;
            Movie = movie;
            trailerLink = "https://www.youtube.com/results?search_query=" + movie.Title + " trailer";

            SetCommands();
        }

        // Functions

        private void MovieListPage(object? param) {
            MainFrame.Content = new MovieListPage(MainFrame);
        }

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private void SettingsPage(object? sender) {
            MainFrame.Content = new SettingsPage(MainFrame);
        }

        private void ProfilePage(object? sender) {
            MainFrame.Content = new UserAccountPage(MainFrame);
        }

        private void ForYouPage(object? sender) {
            MainFrame.Content = new ForYouPage(MainFrame);
        }

        private void SetCommands() {

            ForYouPageCommand = new RelayCommand(ForYouPage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);
            MovieListPageCommand = new RelayCommand(MovieListPage);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            VideoPlayerPageCommand = new RelayCommand(VideoPlayerPage);
        }

       private void VideoPlayerPage(object? param) {

            IfMovieExistDontAddHistoryList();
            IfMovieExistDontAddMovies();
            IfMovieDontExistAddMoviesHistoryLists();

            MainFrame.Content = new VideoPlayerPage(MainFrame, MovieAdapter);
        }

        private void IfMovieExistDontAddHistoryList() {

            if (CurrentUser.HistoryList != null)
                foreach (var movie in CurrentUser.HistoryList)
                    if (movie.Movie.Title == Movie.Title && movie.Movie.imdbID == Movie.imdbID) {
 
                        MovieAdapter = movie.Movie;
                        isContain = true;
                        return;
                    }
        }

        private void IfMovieExistDontAddMovies() {

            if (DbContext.Movies != null && !isContain) {
                var movies = DbContext.Movies.ToList();

                if (movies != null) {
                    foreach (var movie in movies) {
                        if (movie.Title == Movie.Title && movie.imdbID == Movie.imdbID) {
 
                            ReelJet.Database.Entities.Concretes.UserHistoryList historyList = new() {
                                UserId = CurrentUser.Id,
                                MovieId = movie.Id,
                            };

                            HistoryLists.Add(historyList.Movie);
                            DbContext.HistoryLists.Add(historyList);
                            DbContext.SaveChanges();
 
                            MovieAdapter = movie;
                            isContain = true;
                            return;
                        }
                    }
                }
            }
        }

        private void IfMovieDontExistAddMoviesHistoryLists() {

            if (!isContain) {

                ReelJet.Database.Entities.Movie movie = AddMovies();
 
                ReelJet.Database.Entities.Concretes.UserHistoryList historyList = new() {
                    UserId = CurrentUser.Id,
                    MovieId = movie.Id,
                };
                HistoryLists.Add(historyList.Movie);
 
                foreach (var rating in Movie.Ratings)
                    DbContext.Ratings.Add(new() {
                        Source = rating.Source,
                        Value = rating.Value,
                        MovieId = movie.Id
                    });
 
                DbContext.HistoryLists.Add(historyList); 
                DbContext.SaveChanges();

                MovieAdapter = movie;
            }
        }

        private ReelJet.Database.Entities.Movie AddMovies() {

            ReelJet.Database.Entities.Movie movie = new() {
                Actors = Movie.Actors,
                Awards = Movie.Awards,
                BoxOffice = Movie.BoxOffice,
                Country = Movie.Country,
                Director = Movie.Director,
                DVD = Movie.DVD,
                Genre = Movie.Genre,
                imdbID = Movie.imdbID,
                imdbVotes = Movie.imdbVotes,
                imdbRating = Movie.imdbRating,
                Language = Movie.Language,
                LikeCount = 0,
                ViewCount = 1,
                Metascore = Movie.Metascore,
                Plot = Movie.Plot,
                Poster = Movie.Poster,
                Production = Movie.Production,
                Rated = Movie.Rated,
                Released = Movie.Released,
                Response = Movie.Response,
                Runtime = Movie.Runtime,
                Year = Movie.Year,
                Writer = Movie.Writer,
                Website = Movie.Website,
                Type = Movie.Type,
                Title = Movie.Title
            };
            DbContext.Movies!.Add(movie);
            DbContext.SaveChanges();

            return movie;
        }


        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}