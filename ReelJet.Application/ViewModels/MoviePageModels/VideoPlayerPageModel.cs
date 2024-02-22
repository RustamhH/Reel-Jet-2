using System;
using System.Net.Http;
using HtmlAgilityPack;
using System.ComponentModel;
using System.Windows.Controls;
using ReelJet.Database.Entities;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using ReelJet.Database.Entities.Concretes;
using ReelJet.Database.Entities.Abstracts;
using Reel_Jet.Views.MoviePages.VideoPlayerPages;
using ReelJet.Application.Models.ServerNamespace.Abstracts;
using static ReelJet.Application.Models.DatabaseNamespace.Database;

#nullable disable

namespace Reel_Jet.ViewModels.MoviePageModels {
    class VideoPlayerPageModel : INotifyPropertyChanged {

        // Private Fields

        private Movie Movie;
        private string _search;
        private Frame MainFrame;
        private string _videoUrl;
        private string _videoPgUrl;
        private BaseMovie BaseMovie;
        private string serverName = null;
        private PersonalMovie PersonalMovie;

        // Public Fields

        public object PrevFrame;
        public Frame VideoPlayerFrame;
        public FullScreenPage fullScreenPage;
        public ObservableCollection<Reel_Jet.Models.MovieNamespace.Option> Options { get; set; } 
        public string VideoUrl {
            get => _videoUrl;
            set {
                _videoUrl = value;
            } 
        }

        // Constructor

        public VideoPlayerPageModel(Frame frame, BaseMovie basemovie, Frame videoplayerframe, string? movietype = "film") {

            ScrapingServers.Clear();
            VideoPlayerFrame = videoplayerframe;
            MainFrame = frame;
            BaseMovie = basemovie;

            if (movietype == "film") {

                Movie = (BaseMovie as Movie);

                SearchAlgorithm(Movie.Title);
                if (!CheckMovieExist()) {
                    VideoPlayerFrame.Content = new MinimizeScreenPage(frame, Movie, Options, $"https://multiembed.mov/?video_id={Movie.imdbID}", _videoPgUrl);
                }
                else VideoPlayerFrame.Content = new MinimizeScreenPage(frame, Movie, Options, VideoUrl, _videoPgUrl);
            }
            else {
                PersonalMovie = basemovie as PersonalMovie;
                VideoPlayerFrame.Content = new MinimizeScreenPage(frame, PersonalMovie, Options, PersonalMovie.MovieLink, _videoPgUrl, movietype);
            }

        }

        // Functions

        private bool CheckMovieExist() {

            if (ScrapeFullHdIzle()) serverName = "Multiple Server 3";
            if (ScrapeFullFilmIzleNet()) serverName = "Multiple Server 2";
            if (ScrapeDiziBox()) serverName = "Multiple Server 1";

            if (serverName == null) return false;
            else {
                foreach(var server in ScrapingServers)
                    if (server.ServerName == serverName) {
                        Options = server.Options;
                        VideoUrl = server.VideoFrameUrl;
                        _videoPgUrl = server.VideoPageUrl;
                        break;
                    }
                return true;
            }
        }

        private void SearchAlgorithm(string title) {
            _search = title.ToLower();
            _search = _search.Replace(" ", "+");
            _search = _search.Replace("ı", "i");
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
                Reel_Jet.Models.MovieNamespace.Option option = new Reel_Jet.Models.MovieNamespace.Option();
                option.option = optionNode.InnerText;
                Options.Add(option);
            }

            HtmlAttribute scrapingLink = linkContainer.Attributes["src"];

            if (scrapingLink.Value.Substring(0, 5) == "https") VideoUrl = scrapingLink.Value;
            else VideoUrl = "https:" + scrapingLink.Value;

            if (VideoUrl.Contains("youtube.com"))
                throw new Exception("Trailer Link");
        }

        private string? FindVideoLink(string searchlink) {

            string url = searchlink + _search;
            var httpClient = new HttpClient();
            var html = httpClient.GetStringAsync(url).Result;
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            HtmlAttribute? scr = null;
            HtmlNodeCollection movieDetailNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='movie-details existing-details']");

            if (movieDetailNodes == null) return null;
            foreach (var node in movieDetailNodes) {
                try {
                    if (node.InnerText.Contains(Movie.Year)) {
                        var doc = new HtmlDocument();
                        doc.LoadHtml(node.InnerHtml);

                        var nameDiv = doc.DocumentNode.SelectSingleNode("//div[@class='name']");
                        var anchorElement = nameDiv.SelectSingleNode("a");
                        scr = anchorElement.Attributes["href"];
                        break;
                    }   
                }
                catch { }
            }
            return scr!.Value;
        }


        // Scraping


        // DiziBox

        private bool ScrapeDiziBox() {
            try {
                ScrapingServer server = new();
                ScrapingServers.Add(server);
                server.ServerName = "Multiple Server 1";
                string? VideoPageLink = FindVideoLink("https://www.dizifilmbox.pw/?s=");
                server.VideoPageUrl = VideoPageLink;
                FindEmbedVideoLink(VideoPageLink);
                server.VideoFrameUrl = VideoUrl;
                server.Options = new();
                foreach(var option in Options) {
                    server.Options.Add(option);
                }
                _videoPgUrl = VideoPageLink;
                return true;
            }
            catch {
                return false;
            }
        }

        // FullFilmIzle.net

        private bool ScrapeFullFilmIzleNet() {
            try {
                ScrapingServer server = new();
                ScrapingServers.Add(server);
                server.ServerName = "Multiple Server 2";
                string? VideoPageLink = FindVideoLink("https://fullfilmizle.net/?s=");
                server.VideoPageUrl = VideoPageLink;
                FindEmbedVideoLink(VideoPageLink);
                _videoPgUrl = VideoPageLink;
                server.VideoFrameUrl = VideoUrl;
                server.Options = new();
                foreach(var option in Options) {
                    server.Options.Add(option);
                }
                return true;
            }
            catch {
                return false;
            }
        }

        // FullHdIzle.me

        private bool ScrapeFullHdIzle() {
            try {
                ScrapingServer server = new();
                ScrapingServers.Add(server);
                server.ServerName = "Multiple Server 3";
                string? VideoPageLink = FindVideoLink("https://www.fullhdizle.me/?s=");
                server.VideoPageUrl = VideoPageLink;
                FindEmbedVideoLink(VideoPageLink);
                _videoPgUrl = VideoPageLink;
                server.VideoFrameUrl = VideoUrl;
                server.Options = new();
                foreach(var option in Options) {
                    server.Options.Add(option);
                }
                return true;
            }
            catch {
                return false;
            }
        }


        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) { 
            PropertyChanged!.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}