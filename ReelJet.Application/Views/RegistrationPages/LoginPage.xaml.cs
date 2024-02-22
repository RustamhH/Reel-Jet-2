using System;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.ViewModels.RegistrationPageModels;
using ReelJet.Application.Views.RegistrationPages;
using Reel_Jet.Views.RegistrationPages.SignUpPages;

namespace Reel_Jet.Views.RegistrationPages {
    public partial class LoginPage : Page {

        // Private Fields

        private Frame MainFrame;

        // Constructor

        public LoginPage(Frame frame) {
            InitializeComponent();
            MainFrame = frame;
            DataContext = new LoginPageModel(frame);
        }

        // Functions

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
            MainFrame.Content = new MainSignUpPage(MainFrame);
        }

        private void ForgotPassword_MouseDown(object sender, MouseButtonEventArgs e) {
            MainFrame.Content = new ForgotPasswordPage(MainFrame);
        }
    }
}
