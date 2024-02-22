using System.Windows;
using System.Threading;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using Reel_Jet.Views.MoviePages;
using System.Runtime.CompilerServices;
using ReelJet.Database.Entities.Concretes;
using ReelJet.Application.Models.EntityAdapters;
using ReelJet.Application.Models.DatabaseNamespace;
using static ReelJet.Application.Models.DatabaseNamespace.Database;
using static ReelJet.Application.Models.DatabaseNamespace.JsonHandling;


namespace Reel_Jet.ViewModels.RegistrationPageModels {
    public class LoginPageModel : INotifyPropertyChanged {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public UserAdapter NewUser { get; set; } = new();
        public ICommand? SignInCommand { get; set; }
        public ICommand? LanguageSelectionChangedCommand { get; set; }

        // Constructor

        public LoginPageModel(Frame frame) {
            MainFrame = frame;
            SignInCommand = new RelayCommand(SignIn);
            LanguageSelectionChangedCommand = new RelayCommand(LanguageSelectionChanged);
        }

        // Functions

        private void LanguageSelectionChanged(object? param) {

            string selectedlng="";
            if (param is ComboBoxItem cb)
                if (cb.Content is StackPanel sp)
                    foreach (var item in sp.Children) {
                        if (item is TextBlock tb)
                            selectedlng = tb.Text;
                    }
            if (selectedlng=="AZE")
                CurrentLanguageControl.DeepCopy(ReadData<AzerbaijaniLanguageControl>("aze")!);
            else if (selectedlng=="ENG")
                CurrentLanguageControl.DeepCopy(ReadData<EnglishLanguageControl>("eng")!);
        }

        private void SignIn(object? param) {

            UserAuthentication authentication = new();
            
            if (!string.IsNullOrEmpty(NewUser.Email) && !string.IsNullOrEmpty(NewUser.Password))
                if (authentication.LogIn(NewUser.ConvertToUser())) {
                    userAuthentication.Avatar = UserAuthentication.LoadImage(CurrentUser.Avatar);
                    Thread fileMemoryThread = new Thread(() => {
                        WriteData<int>(CurrentUser.Id, "logs");
                    });
                    fileMemoryThread.Start();
                    MainFrame.Content = new MovieListPage(MainFrame);
                }
                else
                    MessageBox.Show("This account doesn't exist!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("Fill all the required fields", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }


        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
