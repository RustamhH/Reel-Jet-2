using System;
using System.Linq;
using System.Text.Json;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.Services.WebServices;
using System.Collections.ObjectModel;
using Reel_Jet.Models.MovieNamespace;
using System.Runtime.CompilerServices;
using Reel_Jet.Views.NavigationBarPages;
using ReelJet.Application.Views.MoviePages;
using ReelJet.Application.Models.DatabaseNamespace;
using static ReelJet.Application.Models.DatabaseNamespace.Database;


namespace Reel_Jet.ViewModels.MoviePageModels {
    public class MoviewListPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;
        private MovieCollection _movie;
        private string _selectedfilter;

        // Binding Properties

        public MovieCollection Movie {
            get => _movie;
            set {
                _movie = value;
                OnPropertyChanged();
            }
        }
        public string SelectedFilter {
            get => _selectedfilter;
            set {
                _selectedfilter = value;
                OnPropertyChanged();
            }
        }
        public ICommand? SearchCommand { get; set; }
        public ICommand? AddToWatchListCommand { get; set; }
        public ICommand? ForYouPgButtonCommand { get; set; }
        public ICommand? HistoryPgButtonCommand { get; set; }
        public ICommand? ProfilePgButtonCommand { get; set; }
        public ICommand? SettingsPgButtonCommand { get; set; }
        public ICommand? SelectionChangedCommand { get; set; }
        public ICommand? WatchListPgButtonCommand { get; set; }
        public ObservableCollection<Movie> Movies { get; set; }
        public ICommand? WatchTrailerFromListCommand { get; set; }
        public ICommand? FilterSelectionChangedCommand { get; set; }

        // Constructor

        public MoviewListPageModel(Frame frame) { 

            MainFrame = frame;
            SetCommands();

            this.Movies = Database.Movies;

            if (this.Movies.Count == 0) GetPopularMoviesFromDatabase("popular");
        }

        // Functions

        private void HistoryPage(object? sender) {
            MainFrame.Content = new HistoryPage(MainFrame);
        }

        private void WatchListPage(object? sender) {
            MainFrame.Content = new WatchListPage(MainFrame);
        }

        private void ForYouPage(object? sender) {
            MainFrame.Content = new ForYouPage(MainFrame);
        }

        private void ProfilePage(object? sender) {
            MainFrame.Content = new UserAccountPage(MainFrame);
        }

        private void SettingsPage(object? sender) {
            MainFrame.Content = new SettingsPage(MainFrame);
        }

        private void SelectionChanged(object? param) {

            Movie movie = (param as Movie)!;
            if (movie != null)
                MainFrame.Content = new MoviePreviewPage(MainFrame, movie);
        }

        private void Search(object? param) {

            string text = (param as string)!;
            TaskToJson(text);
        }

        private void FilterSelectionChanged(object? param) {

            Movies.Clear();
            string selectedfilter = (param as ComboBoxItem)!.Content.ToString()!.ToLower();
            selectedfilter = selectedfilter.Replace(" ", "_");
            GetPopularMoviesFromDatabase(selectedfilter);
        }

        private async Task AddAsyncWatchListDatabase (ReelJet.Database.Entities.Movie MovieEntity) {

            ReelJet.Database.Entities.Concretes.UserWatchList WatchList = new() {
                UserId = CurrentUser.Id,
                MovieId = MovieEntity.Id
            };
 
            await DbContext.WatchLists.AddAsync(WatchList);
            await DbContext.SaveChangesAsync();
            WatchLists.Add(MovieEntity);
        }

        private void SetCommands() {

            SearchCommand = new RelayCommand(Search);
            ForYouPgButtonCommand = new RelayCommand(ForYouPage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);
            AddToWatchListCommand = new RelayCommand(AddToWatchList);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            WatchTrailerFromListCommand = new RelayCommand(SelectionChanged);
            FilterSelectionChangedCommand = new RelayCommand(FilterSelectionChanged);
        }

        private async void TaskToJson(string title) {

            var jsonStr = await OmdbService.GetAllMoviesByTitle(title);
            Movie = JsonSerializer.Deserialize<MovieCollection>(jsonStr)!;

            if (Movie.Search is not null) {
                if (Movies != null) Movies.Clear();

                foreach (var result in Movie.Search) {

                    var movieCollectionJson = await OmdbService.GetConcreteMovieById(result.imdbID);
                    var movieFromCollection = JsonSerializer.Deserialize<Movie>(movieCollectionJson);

                    if (movieFromCollection!.Poster == "N/A")
                        movieFromCollection.Poster = "\\Static Files\\Images\\no-poster.png";

                    if (movieFromCollection is not null)
                        Movies.Add(movieFromCollection);
                }
                return;
            }
        }

        private void GetPopularMoviesFromDatabase(string filterchoice) {

            if (filterchoice == "popular" || filterchoice == "populyar")
                foreach (var item in Database.PopularMovies)
                    Movies.Add(item);
            else if (filterchoice == "top_rated" || filterchoice == "yüksək_reytinqli")
                foreach (var item in Database.TopRatedMovies)
                    Movies.Add(item);
            else if (filterchoice == "upcoming" || filterchoice=="tezliklə")
                foreach (var item in Database.UpcomingMovies)
                    Movies.Add(item);
            else if (filterchoice == "now_playing" || filterchoice=="yeni")
                foreach (var item in Database.NowPlayingMovies)
                    Movies.Add(item);

        }

        private async void AddToWatchList(object? sender) {

            Movie Movie = (sender as Movie)!;
            var movie = await GetAddedMovie(Movie);
        }

        private async Task<ReelJet.Database.Entities.Movie> GetAddedMovie(Movie Movie) {

            bool isContain = false;

            ReelJet.Database.Entities.Movie MovieEntity = new() {
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
                Title = Movie.Title,
            };
 
            if (WatchLists != null)
                foreach (var movie in WatchLists) {
                    if (movie.imdbID == MovieEntity.imdbID) {
                        isContain = true;
                        break;
                    }
                }
            
            if(!isContain) {
                foreach (var movie in DbContext.Movies.ToList()) {
                    if (MovieEntity.imdbID == movie.imdbID) {
                        await AddAsyncWatchListDatabase(movie);
                        isContain = true; 
                        break;
                    }
                }
            }

            if (!isContain) {
 
                await DbContext.Movies.AddAsync(MovieEntity);
                await DbContext.SaveChangesAsync();
 
                foreach (var rating in Movie.Ratings)
                    await DbContext.Ratings.AddAsync(new() {
                        Source = rating.Source,
                        Value = rating.Value,
                        MovieId = MovieEntity.Id
                    });

                await AddAsyncWatchListDatabase(MovieEntity);
            }
            return MovieEntity;
        }

        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}