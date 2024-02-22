using System;
using RestSharp;
using System.Linq;
using System.Windows;
using System.Threading;
using System.Text.Json;
using Castle.Core.Internal;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;
using ReelJet.Database.Contexts;
using Reel_Jet.Views.MoviePages;
using Reel_Jet.Models.MovieNamespace;
using Reel_Jet.Views.RegistrationPages;
using ReelJet.Application.Models.DatabaseNamespace;
using static Reel_Jet.Services.WebServices.OmdbService;
using static ReelJet.Application.Models.DatabaseNamespace.Database;
using static ReelJet.Application.Models.DatabaseNamespace.JsonHandling;


namespace Reel_Jet.ViewModels.RegistrationPageModels {
    public class LoadingPageModel {

        // Private Fields

        private int? UserId;
        private Frame MainFrame;

        // Constructor

        public LoadingPageModel(Frame frame) { 

            MainFrame = frame;

            Thread loadCurrentUser = new Thread(() => {

                DbContext = new ReelJetDbContext();
                var users = DbContext.Users.ToList();
                Users = users;
            });
            loadCurrentUser.Start();

            Thread loadDatabaseResources = new Thread(() => {

                loadCurrentUser.Join();

                // Loading resources

                foreach (var watchlist in CurrentUser.WatchList)
                    WatchLists.Add(watchlist.Movie);

                foreach (var historylist in CurrentUser.HistoryList)
                    HistoryLists.Add(historylist.Movie);
            });

            Thread fileMemoryThread = new Thread(() => {
                try {
                    UserId = ReadData<int?>("logs");

                    loadCurrentUser.Join();
                    CurrentUser = Users.Where(u => u.Id == UserId).First();
                    userAuthentication.Avatar = UserAuthentication.LoadImage(CurrentUser.Avatar!);
                    loadDatabaseResources.Start();
                }
                catch(Exception ex) {
                    UserId = null;
                }
            });
            fileMemoryThread.Start();

            try {
                Thread popularThread = new Thread(()=>LoadFilteredMoviesToDatabase("popular"));
                popularThread.Start();

                Thread topRatedThread = new Thread(() => LoadFilteredMoviesToDatabase("top_rated"));
                topRatedThread.Start();

                Thread upcomingThread = new Thread(() => LoadFilteredMoviesToDatabase("upcoming"));
                upcomingThread.Start();

                Thread nowPlayingThread = new Thread(() => LoadFilteredMoviesToDatabase("now_playing"));
                nowPlayingThread.Start();
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message, "Network Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(6.25);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Functions

        private void Timer_Tick(object? sender, EventArgs e) {
            // Stop the timer
            
            ((DispatcherTimer)sender!).Stop();
            if (UserId != null) 
                MainFrame.Content = new MovieListPage(MainFrame);
            else MainFrame.Content = new LoginPage(MainFrame);
        }

        public async void LoadFilteredMoviesToDatabase(string filterchoice) {

            var options = new RestClientOptions($"https://api.themoviedb.org/3/movie/{filterchoice}?language=en-US&page=1");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwN2I3OWYxNmU2NWFmMGY1YTBjNGY4ZGFkZDdkMDhjNCIsInN1YiI6IjY0YjA0MzFjMjBlY2FmMDBjNmY2MWQ1ZSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.VErhjbegJJ2tyZVP-GiDRN_gTcH_MYVhQ1wThi0Ytb0");
            var response = await client.GetAsync(request);
            FilteredMovies popularMovies = JsonSerializer.Deserialize<FilteredMovies>(response.Content!)!;
            for (int i = 0; i < popularMovies.results.Count; i++) {

                var jsonStr = await GetConcreteMovieByTitle(popularMovies.results[i].title);
                Movie movie = JsonSerializer.Deserialize<Movie>(jsonStr)!;
                movie.Year = popularMovies.results[i].release_date.Substring(0, 4);

                if (!movie.Title.IsNullOrEmpty()) {
                    if (filterchoice == "popular") Database.PopularMovies.Add(movie);
                    else if (filterchoice == "top_rated") Database.TopRatedMovies.Add(movie);
                    else if (filterchoice == "upcoming") Database.UpcomingMovies.Add(movie);
                    else if (filterchoice == "now_playing") Database.NowPlayingMovies.Add(movie);
                } 
            }   
        }
    }
}