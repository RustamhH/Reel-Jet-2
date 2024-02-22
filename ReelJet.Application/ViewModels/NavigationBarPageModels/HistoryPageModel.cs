using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using ReelJet.Database.Entities;
using System.Collections.ObjectModel;
using Reel_Jet.Views.NavigationBarPages;
using ReelJet.Application.Views.MoviePages;
using static ReelJet.Application.Models.DatabaseNamespace.Database;


namespace Reel_Jet.ViewModels.NavigationBarPageModels {
    public class HistoryPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ICommand ForYouPageCommand { get; set; }
        public ICommand? MoviePgButtonCommand { get; set; }
        public ICommand? ProfilePgButtonCommand { get; set; }
        public ICommand? SelectionChangedCommand { get; set; }
        public ICommand? SettingsPgButtonCommand { get; set; }
        public ICommand? WatchListPgButtonCommand { get; set; }
        public ObservableCollection<Movie> HistoryList { get; set; }

        // Constructor

        public HistoryPageModel(Frame frame) {

            MainFrame = frame;

            WriteHistoryList();
            SetCommands();
        }

        // Functions

        private void SelectionChanged(object? param) {
            Movie movie = (Movie)param!;
            MainFrame.Content = new VideoPlayerPage(MainFrame, movie);
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

        private void WriteHistoryList() {

            HistoryList = HistoryLists;
        }

        private void SetCommands()  {

            ForYouPageCommand = new RelayCommand(ForYouPage);
            ProfilePgButtonCommand = new RelayCommand(ProfilePage);
            MoviePgButtonCommand = new RelayCommand(MovieListPage);
            SettingsPgButtonCommand = new RelayCommand(SettingsPage);
            WatchListPgButtonCommand = new RelayCommand(WatchListPage);
            SelectionChangedCommand = new RelayCommand(SelectionChanged);
        }
    }
}
