using System;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ReelJet.Database.Entities.Concretes {
    public class BaseLanguageControl : INotifyPropertyChanged {

        // Private Fields

        private string welcomeBackText;
        private string signInToYourAccountText;
        private string passwordText;
        private string forgotPasswordText;
        private string signInText;
        private string dontHaveAnAccountText;
        private string signUpText;
        private string registrationText;
        private string nameText;
        private string surnameText;
        private string ageText;
        private string usernameText;
        private string phoneNumberText;
        private string confirmYourPasswordText;
        private string alreadyHaveAnAccountText;
        private string authenticateYourAccountText;
        private string enterAuthorizationCodeText;
        private string confirmText;
        private string forgotYourPasswordText;
        private string newPasswordText;
        private string requestPasswordResetText;
        private string backToText;
        private string searchText;
        private string filterMoviesText;
        private string popularText;
        private string nowPlayingText;
        private string accounttext;
        private string upcomingText;
        private string topRatedText;
        private string trailerText;
        private string watchListText;
        private string minuteText;
        private string watchMovieText;
        private string imdbRatingText;
        private string directorText;
        private string starsText;
        private string writersText;
        private string commentText;
        private string addcommentText;
        private string cancelText;
        private string sendText;
        private string settingsText;
        private string clearCacheText;
        private string logOutText;
        private string editProfileText;
        private string confirmChangesText;
        private string timeRemainingText;
        private string secondtext;
        private string movielinktext;
        private string detailstext;
        private string titletext;
        private string descriptiontext;
        private string hourtext;
        private string minutetext;

        // Login Page

        public string WelcomeBackText { get => welcomeBackText; set { welcomeBackText = value; OnPropertyChanged(); } }
        public string SignInToYourAccountText { get => signInToYourAccountText; set { signInToYourAccountText = value; OnPropertyChanged(); } }
        public string PasswordText { get => passwordText; set { passwordText = value; OnPropertyChanged(); } }
        public string ForgotPasswordText { get => forgotPasswordText; set { forgotPasswordText = value; OnPropertyChanged(); } }
        public string SignInText { get => signInText; set { signInText = value; OnPropertyChanged(); } }
        public string DontHaveAnAccountText { get => dontHaveAnAccountText; set { dontHaveAnAccountText = value; OnPropertyChanged(); } }
        public string SignUpText { get => signUpText; set { signUpText = value; OnPropertyChanged(); } }

        // SignUp Page

        public string RegistrationText { get => registrationText; set { registrationText = value; OnPropertyChanged(); } }
        public string NameText { get => nameText; set { nameText = value; OnPropertyChanged(); } }
        public string SurnameText { get => surnameText; set { surnameText = value; OnPropertyChanged(); } }
        public string AgeText { get => ageText; set { ageText = value; OnPropertyChanged(); } }
        public string UsernameText { get => usernameText; set { usernameText = value; OnPropertyChanged(); } }
        public string PhoneNumberText { get => phoneNumberText; set { phoneNumberText = value; OnPropertyChanged(); } }
        public string ConfirmYourPasswordText { get => confirmYourPasswordText; set { confirmYourPasswordText = value; OnPropertyChanged(); } }
        public string AlreadyHaveAnAccountText { get => alreadyHaveAnAccountText; set { alreadyHaveAnAccountText = value; OnPropertyChanged(); } }


        // Validation Page
        public string AuthenticateYourAccountText { get => authenticateYourAccountText; set { authenticateYourAccountText = value; OnPropertyChanged(); } }
        public string EnterAuthorizationCodeText { get => enterAuthorizationCodeText; set { enterAuthorizationCodeText = value; OnPropertyChanged(); } }
        public string ConfirmText { get => confirmText; set { confirmText = value; OnPropertyChanged(); } }
        public string TimeRemainingText { get => timeRemainingText; set { timeRemainingText = value; OnPropertyChanged(); } }
        public string SecondText { get => secondtext; set { secondtext = value; OnPropertyChanged(); } }


        // Forgot Password Page

        public string ForgotYourPasswordText { get => forgotYourPasswordText; set { forgotYourPasswordText = value; OnPropertyChanged(); } }
        public string NewPasswordText { get => newPasswordText; set { newPasswordText = value; OnPropertyChanged(); } }
        public string RequestPasswordResetText { get => requestPasswordResetText; set { requestPasswordResetText = value; OnPropertyChanged(); } }
        public string BackToText { get => backToText; set { backToText = value; OnPropertyChanged(); } }


        // Movie List Page

        public string SearchText { get => searchText; set { searchText = value; OnPropertyChanged(); } }
        public string FilterMoviesText { get => filterMoviesText; set { filterMoviesText = value; OnPropertyChanged(); } }
        public string PopularText { get => popularText; set { popularText = value; OnPropertyChanged(); } }
        public string NowPlayingText { get => nowPlayingText; set { nowPlayingText = value; OnPropertyChanged(); } }
        public string UpcomingText { get => upcomingText; set { upcomingText = value; OnPropertyChanged(); } }
        public string TopRatedText { get => topRatedText; set { topRatedText = value; OnPropertyChanged(); } }
        public string TrailerText { get => trailerText; set { trailerText = value; OnPropertyChanged(); } }
        public string WatchListText { get => watchListText; set { watchListText = value; OnPropertyChanged(); } }

        // Movie Preview Page

        public string WatchMovieText { get => watchMovieText; set { watchMovieText = value; OnPropertyChanged(); } }
        public string ImdbRatingText { get => imdbRatingText; set { imdbRatingText = value; OnPropertyChanged(); } }
        public string DirectorText { get => directorText; set { directorText = value; OnPropertyChanged(); } }
        public string StarsText { get => starsText; set { starsText = value; OnPropertyChanged(); } }
        public string WritersText { get => writersText; set { writersText = value; OnPropertyChanged(); } }


        // Video Player Page

        public string CommentText { get => commentText; set { commentText = value; OnPropertyChanged(); } }
        public string AddCommentText { get => addcommentText; set { addcommentText = value; OnPropertyChanged(); } }
        public string CancelText { get => cancelText; set { cancelText = value; OnPropertyChanged(); } }
        public string SendText { get => sendText; set { sendText = value; OnPropertyChanged(); } }

        // Settings Page

        public string AccountText { get => accounttext; set { accounttext = value; OnPropertyChanged(); } }
        public string ClearCacheText { get => clearCacheText; set { clearCacheText = value; OnPropertyChanged(); } }
        public string SettingsText { get => settingsText; set { settingsText = value; OnPropertyChanged(); } }
        public string LogOutText { get => logOutText; set { logOutText = value; OnPropertyChanged(); } }
        public string UploadVideoText { get => settingsText; set { settingsText = value; OnPropertyChanged(); } }


        // Account Page

        public string EditProfileText { get => editProfileText; set { editProfileText = value;OnPropertyChanged(); } }
        public string ConfirmChangesText { get => confirmChangesText; set { confirmChangesText = value; OnPropertyChanged(); } }

        public void DeepCopy(BaseLanguageControl clonedObject) { 
            PropertyInfo[] properties = typeof(BaseLanguageControl).GetProperties();

            foreach (var property in properties) {
                if (property.CanWrite) {
                    var value = property.GetValue(clonedObject);
                    property.SetValue(this, value);
                }
            }
        }

        // ForYou Page

        public string MovieLinkText { get => movielinktext; set { movielinktext = value; OnPropertyChanged(); } }
        public string DetailsText { get => detailstext; set { detailstext = value; OnPropertyChanged(); } }
        public string TitleText { get => titletext; set { titletext = value; OnPropertyChanged(); } }
        public string DescriptionText { get => descriptiontext; set { descriptiontext = value; OnPropertyChanged(); } }
        public string HourText { get => hourtext; set { hourtext = value; OnPropertyChanged(); } }
        public string MinuteText { get => minutetext; set { minutetext = value; OnPropertyChanged(); } }


        // INotify

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
