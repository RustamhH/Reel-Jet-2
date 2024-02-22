using System;
using System.Windows.Controls;
using Reel_Jet.Views.RegistrationPages;
using ReelJet.Application.ViewModels.RegistrationPageModels;

namespace ReelJet.Application.Views.RegistrationPages;

public partial class ForgotPasswordPage : Page {

    // Private Fields

    private Frame MainFrame;
    
    // Constructor

    public ForgotPasswordPage(Frame frame) {
        InitializeComponent();
        MainFrame = frame;
        DataContext = new ForgotPasswordPageModel(frame);

    }

    // Functions

    private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
        MainFrame.Content = new LoginPage(MainFrame);
    }
}