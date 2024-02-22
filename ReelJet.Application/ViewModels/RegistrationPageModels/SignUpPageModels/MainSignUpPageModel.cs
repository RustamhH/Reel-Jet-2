using System;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.Windows.Controls;
using ReelJet.Database.Entities.Concretes;
using Reel_Jet.Views.RegistrationPages.SignUpPages;
using static ReelJet.Application.Models.DatabaseNamespace.Database;
using static ReelJet.Application.Models.DatabaseNamespace.JsonHandling;

#nullable disable

namespace Reel_Jet.ViewModels.RegistrationPageModels.SignUpPageModels {
    public class MainSignupPageModel {

        // Private Fields

        private Frame MainFrame;

        // Binding Properties

        public ICommand LanguageSelectionChangedCommand { get; set; }

        // Constructor

        public MainSignupPageModel(Frame frame, Frame frame2, User? currentUser, string process) {

            MainFrame = frame;
            LanguageSelectionChangedCommand = new RelayCommand(LanguageSelectionChanged);
            if (process == "Registration") frame2.Content = new RegistrationPage(MainFrame, frame2);
            else if (process == "Password Recover") frame2.Content = new ValidationPage(MainFrame,currentUser, "Password Recover");
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
                CurrentLanguageControl.DeepCopy(ReadData<AzerbaijaniLanguageControl>("aze"));
            else if (selectedlng == "ENG")
                CurrentLanguageControl.DeepCopy(ReadData<EnglishLanguageControl>("eng"));
        }
    }
}