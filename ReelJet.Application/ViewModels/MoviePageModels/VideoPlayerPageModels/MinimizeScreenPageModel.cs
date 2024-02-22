using System;
using System.Linq;
using HtmlAgilityPack;
using System.Net.Http;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using ReelJet.Database.Entities;
using Microsoft.Web.WebView2.Wpf;
using Microsoft.Web.WebView2.Core;
using System.Collections.ObjectModel; 
using System.Runtime.CompilerServices;
using Reel_Jet.Views.NavigationBarPages;
using ReelJet.Database.Entities.Abstracts;
using ReelJet.Database.Entities.Concretes;
using ReelJet.Application.Views.MoviePages;
using ReelJet.Application.Models.EntityAdapters;
using Reel_Jet.Views.MoviePages.VideoPlayerPages;
using ReelJet.Application.Models.DatabaseNamespace;
using static ReelJet.Application.Models.DatabaseNamespace.Database;

#nullable disable

namespace Reel_Jet.ViewModels.MoviePageModels.VideoPlayerPageModels {
    public class MinimizeScreenPageModel : INotifyPropertyChanged {
 
         // Private Fields
 
        private Movie Movie;
        private int viewCount;
        private int likeCount;
        private Frame MainFrame;
        private WebView2 Player;
        private string _username;
        private string _videoUrl;
        private string newcomment;
        private string _movietype;
        private string _videoPgUrl;
        private BaseMovie BaseMovie;
        private bool isAdblocker = false;
        private PersonalMovie PersonalMovie;
        private Reel_Jet.Models.MovieNamespace.Option selectedOption;

        // Binding Properties

        public ObservableCollection<User> Users { get; set; } = new();
        public ObservableCollection<CommentsAvatars> Comments { get; set; } = new();
        public Reel_Jet.Models.MovieNamespace.Option SelectedOption { get => selectedOption; 
            set { 
                selectedOption = value;
                OnPropertyChanged();
            }
        }
        public int ViewCount { get => viewCount; 
            set {
                viewCount = value;
                OnPropertyChanged();
            } 
        }
        public int LikeCount { get => likeCount; 
            set {
                likeCount = value;
                OnPropertyChanged();
            } 
        }
        public string NewComment {
            get => newcomment;
            set {
                newcomment = value;
                OnPropertyChanged();
            }
        }
        public string Username {
            get => _username;
            set {
                _username = value;
                OnPropertyChanged();
            }
        }
 
        public Frame PlayerFrame { get; set; }
        public ICommand LikeBtCommand { get; set; }
        public ICommand ForYouPageCommand { get; set; }
        public ICommand SendCommentCommand { get; set; }
        public ICommand MovieListPageCommand { get; set; }
        public ICommand HistoryPgButtonCommand { get; set; }
        public ICommand ProfilePgButtonCommand { get; set; }
        public ICommand SettingsPgButtonCommand { get; set; }
        public ICommand FullScreenButtonCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand WatchListPgButtonCommand { get; set; }
        public ICommand MultipleServerButtonCommand { get; set; }
        public ObservableCollection<Reel_Jet.Models.MovieNamespace.Option> Options { get; set; } = new();
        public string VideoUrl {
            get => _videoUrl;
            set {
                _videoUrl = value;
            } 
        }
 
        // Constructor
 
        public MinimizeScreenPageModel() {
            
            SetCommands();
        }
 
        public MinimizeScreenPageModel(Frame frame, BaseMovie basemovie, WebView2 player, 
            Frame playerframe, ObservableCollection<Reel_Jet.Models.MovieNamespace.Option> options, string videoUrl, string videoPgLink, string? movietype = "film")
            : this() {
 
            MainFrame = frame;
            BaseMovie = basemovie;
            viewCount = basemovie.ViewCount;
            likeCount = basemovie.LikeCount;
            Player = player;
            VideoUrl = videoUrl;
            PlayerFrame = playerframe;
            _videoPgUrl = videoPgLink;
            _movietype = movietype;

            basemovie.ViewCount++;

            if (options != null) { 
                foreach(var option in options)
                    Options.Add(option);
            }

            SetWebView2();
            if (movietype != "film")
                PersonalMovie = (BaseMovie as PersonalMovie);
            else Movie = (BaseMovie as Movie);
            WriteComments();
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

        private void ForYouPage(object? sender) {
            MainFrame.Content = new ForYouPage(MainFrame);
        }

        private void Player_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e) {
            Player.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e) {
            e.Handled = true;
        }

        private void FullScreenPage(object? sender) {

            VideoPlayerPageModel videoPlayerPageModel = ((MainFrame.Content as VideoPlayerPage).DataContext as VideoPlayerPageModel);
 
            Frame videoPlayerFrame = videoPlayerPageModel.VideoPlayerFrame;
            videoPlayerPageModel.PrevFrame = videoPlayerFrame.Content;
            videoPlayerPageModel.fullScreenPage = PlayerFrame.Content as FullScreenPage;
 
            videoPlayerFrame.Navigate(PlayerFrame.Content);
        }

        private void LikeButton(object? sender) { 

            if (_movietype == "film") {
                DbContext.Movies.Where(m => m.Id == Movie.Id).First().LikeCount++;
                DbContext.SaveChanges();
            }
            else {
                DbContext.PersonalMovies.Where(m => m.Id == PersonalMovie.Id).First().LikeCount++;
                DbContext.SaveChanges();
            }
            LikeCount++;
        }

        private void SetWebView2() {

            Uri uri = new Uri(VideoUrl!);
            Player.Source = uri;

            if (!isAdblocker) {

                isAdblocker = true;
                Player.EnsureCoreWebView2Async();
                Player.CoreWebView2InitializationCompleted += Player_CoreWebView2InitializationCompleted;
            }
        }

        private void SetCommands() {

            LikeBtCommand = new RelayCommand(LikeButton);
            ForYouPageCommand = new RelayCommand(ForYouPage);
            SendCommentCommand = new RelayCommand(SendComment);
            MovieListPageCommand = new RelayCommand(MovieListPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);
            HistoryPgButtonCommand = new RelayCommand(HistoryPage);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            FullScreenButtonCommand = new RelayCommand(FullScreenPage);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
            MultipleServerButtonCommand = new RelayCommand(ChangeServer);
        }

        private void ChangeServer(object? param) {

            string serverName = param as string;
            if (serverName == null || _movietype != "film") return;

            Options.Clear();

            if (serverName == "Multiple Server 4")
                VideoUrl = $"https://multiembed.mov/?video_id={Movie.imdbID}";
            else if (serverName == "Multiple Server 5")
                VideoUrl = $"https://vidload.eu/embed/{Movie.imdbID}";
            else {

                foreach(var server in ScrapingServers) 
                    if (serverName == server.ServerName && server.VideoFrameUrl != null) {
                        VideoUrl = server.VideoFrameUrl;
                        _videoPgUrl = server.VideoPageUrl;

                        foreach (var option in server.Options)
                            Options.Add(option);
                    }
            }

            SetWebView2();
        }

        private void SelectionChanged(object? sender) {

            string option = SelectedOption!.option;
            if (option != null) {
                int count = 0;
                foreach(var op in Options!) {
                    count++;
                    if (op.option == option) break;
                }
 
                try {
                    FindEmbedVideoLink(_videoPgUrl + count.ToString() + "/");
                    Uri uri = new Uri(VideoUrl!);
                    Player.Source = uri;
                }
                catch (Exception e) {
                    if (e.Message == "Trailer Link" && option.ToLower() == "fragman") {
                        Uri uri = new Uri(VideoUrl!);
                        Player.Source = uri;
                    }
                }
            }
        }
 
        private void FindEmbedVideoLink(string? VideoPageLink) {
 
            var httpClient = new HttpClient();
            var htmlDocument = new HtmlDocument();
            var html = httpClient.GetStringAsync(VideoPageLink).Result;
            htmlDocument.LoadHtml(html);
 
            Options = new();
            var linkContainer = htmlDocument.DocumentNode.SelectSingleNode("//iframe[@src]");
            var optionNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='keremiya_part']//span");
 
            foreach(var optionNode in optionNodes) {
                Reel_Jet.Models.MovieNamespace.Option option = new();
                option.option = optionNode.InnerText;
                Options.Add(option);
            }
 
            HtmlAttribute scrapingLink = linkContainer.Attributes["src"];
 
            if (scrapingLink.Value.Substring(0, 5) == "https") VideoUrl = scrapingLink.Value;
            else VideoUrl = "https:" + scrapingLink.Value;
 
            if (VideoUrl.Contains("youtube.com"))
                throw new Exception("Trailer Link");
        }
 
        private void SendComment(object? param) {

            if (!string.IsNullOrEmpty(NewComment)) {
                Comment comment = new Comment { Content = NewComment, PostedTime = DateTime.Now, LikeCount = 0, UserId = CurrentUser.Id };

                DbContext.Comments.Add(comment);
                DbContext.SaveChanges();

                CommentsAvatars commentsavatars;
                if (_movietype == "film") {

                    CommentsMovies commentsMovies = new CommentsMovies() { CommentId = comment.Id, MovieId = Movie.Id };
                    DbContext.CommentsMovies.Add(commentsMovies);
                    DbContext.SaveChanges();

                    commentsavatars = new CommentsAvatars { Id = comment.Id, CommentsMovies = comment.CommentsMovies, User = comment.User, Content = comment.Content, PostedTime = comment.PostedTime, LikeCount = 0, Avatar = UserAuthentication.LoadImage(comment.User.Avatar)};
                }
                else {
                    CommentsPersonalMovies commentsPersonalMovies = new CommentsPersonalMovies() { CommentId = comment.Id, PersonalMovieId = PersonalMovie.Id };
                    DbContext.CommentsPersonalMovies.Add(commentsPersonalMovies);
                    DbContext.SaveChanges();

                    commentsavatars = new CommentsAvatars { Id = comment.Id, CommentsPersonalMovies = comment.CommentsPersonalMovies, User = comment.User, Content = comment.Content, PostedTime = comment.PostedTime, LikeCount = 0, Avatar = UserAuthentication.LoadImage(comment.User.Avatar) };
                }

                Comments.Add(commentsavatars);
                NewComment = string.Empty;
 
            }
        }
 
        private void WriteComments() {
 
            CommentsAvatars commentsavatars = new();
            if (Movie != null) {

                if (_movietype == "film") {
                    var commentsMovies = DbContext.CommentsMovies
                        .Where(p => p.MovieId == Movie.Id)
                        .Select(p => p.Comment)
                        .ToList();
                    foreach(var comment in commentsMovies) {
                        commentsavatars = new CommentsAvatars { Id = comment.Id, CommentsMovies = comment.CommentsMovies, User = comment.User, Content = comment.Content, PostedTime = comment.PostedTime, LikeCount = 0, Avatar = UserAuthentication.LoadImage(comment.User.Avatar) };
                        Comments.Add(commentsavatars);
                    }
                }
            }
            else if (PersonalMovie != null) {
                var commentsPersonalMovies = DbContext.CommentsPersonalMovies
                    .Where(p => p.PersonalMovieId == PersonalMovie.Id)
                    .Select(p => p.Comment)
                    .ToList();
                foreach (var comment in commentsPersonalMovies) {
                    commentsavatars = new CommentsAvatars { Id = comment.Id, CommentsPersonalMovies = comment.CommentsPersonalMovies, User = comment.User, Content = comment.Content, PostedTime = comment.PostedTime, LikeCount = 0, Avatar = UserAuthentication.LoadImage(comment.User.Avatar) };
                    Comments.Add(commentsavatars);
                }
            }
        }

        // INotifyPropertyChanged
 
        public event PropertyChangedEventHandler? PropertyChanged;
 
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged!.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
 
    }
}