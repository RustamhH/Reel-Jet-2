using System;
using System.Windows.Input;
using System.Windows.Controls;
using Reel_Jet.ViewModels.RegistrationPageModels.SignUpPageModels;


namespace Reel_Jet.Views.RegistrationPages.SignUpPages {

    public partial class RegistrationPage : Page {
        private Frame MainFrame;
        public RegistrationPage(Frame frame, Frame frame2) {
            InitializeComponent();
            MainFrame = frame;
            DataContext = new RegistrationPageModel(MainFrame, frame2);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) {
            MainFrame.Content = new LoginPage(MainFrame);
        }
    }
}