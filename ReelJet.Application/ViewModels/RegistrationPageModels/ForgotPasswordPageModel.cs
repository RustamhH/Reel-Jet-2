using System;
using System.Windows;
using Reel_Jet.Commands;
using System.Windows.Controls;
using ReelJet.Database.Entities.Concretes;
using ReelJet.Application.Models.EntityAdapters;
using Reel_Jet.Views.RegistrationPages.SignUpPages;
using static ReelJet.Application.Models.DatabaseNamespace.Database;
using static ReelJet.Application.Models.DatabaseNamespace.JsonHandling;


namespace ReelJet.Application.ViewModels.RegistrationPageModels;

public class ForgotPasswordPageModel {

    // Private Fields

    private Frame MainFrame;

    // Binding Properties

    public UserAdapter CurrentUser { get; set; } = new();
    public RelayCommand RequestPasswordCommand { get; set; }
    public RelayCommand LanguageSelectionChangedCommand { get; set; }
    
    // Constructor

    public ForgotPasswordPageModel(Frame frame) {

        MainFrame = frame;

        RequestPasswordCommand = new RelayCommand(RequestPassword);
        LanguageSelectionChangedCommand = new RelayCommand(LanguageSelectionChanged);
    }

    // Functions

    private void LanguageSelectionChanged(object? param) {

        string selectedlng = "";
        if (param is ComboBoxItem cb)
            if (cb.Content is StackPanel sp)
                foreach (var item in sp.Children) {
                    if (item is TextBlock tb)
                        selectedlng = tb.Text;
                }
        if (selectedlng == "AZE")
            CurrentLanguageControl.DeepCopy(ReadData<AzerbaijaniLanguageControl>("aze")!);
        else if (selectedlng == "ENG")
            CurrentLanguageControl.DeepCopy(ReadData<EnglishLanguageControl>("eng")!);
    }

    public void RequestPassword(object? param) {

        if (!string.IsNullOrEmpty(CurrentUser.Email) && !string.IsNullOrEmpty(CurrentUser.Password)) {
            bool isContain = false;

            foreach (var user in DbContext.Users)
                if (user.Email == CurrentUser.Email)
                    isContain = true;
            if (!isContain) MessageBox.Show($"In database there is no user with email {CurrentUser.Email}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else MainFrame.Content = new MainSignUpPage(MainFrame, CurrentUser.ConvertToUser(), "Password Recover");
        }
        else
            MessageBox.Show("Please Fill All Blanks!", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}
