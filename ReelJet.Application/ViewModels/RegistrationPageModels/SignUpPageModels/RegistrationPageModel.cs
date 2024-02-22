using System;
using System.Windows;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using System.Runtime.CompilerServices;
using ReelJet.Application.Models.EntityAdapters;
using Reel_Jet.Views.RegistrationPages.SignUpPages;


namespace Reel_Jet.ViewModels.RegistrationPageModels.SignUpPageModels {
    public class RegistrationPageModel {

        // Private Fields

        private Frame MainFrame;
        private Frame RegistrationFrame;
        private string? confirmPassword;

        // Binding Properties

        public UserAdapter newUser { get; set; } = new();
        public ICommand SignUpCommand { get; set; }
        public string? ConfirmPassword {
            get => confirmPassword;
            set {
                confirmPassword = value; OnProperty();
            }
        }

        // Constructor

        public RegistrationPageModel(Frame frame, Frame frame2) {

            MainFrame = frame;
            RegistrationFrame = frame2;
            SignUpCommand = new RelayCommand(SignUp);

        }

        // Functions

        public void SignUp(object? param) {

            if (!string.IsNullOrEmpty(newUser.Name) && !string.IsNullOrEmpty(newUser.Surname) && newUser.Age != null &&
                !string.IsNullOrEmpty(newUser.Username) && !string.IsNullOrEmpty(newUser.PhoneNumber) && !string.IsNullOrEmpty(newUser.Password) &&
                !string.IsNullOrEmpty(ConfirmPassword)) {

                if (ConfirmPassword == newUser.Password) 
                    RegistrationFrame.Content = new ValidationPage(MainFrame, newUser.ConvertToUser(), "Registration");
                else
                    MessageBox.Show("Wrong Password Confirmation,Try Again", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Fill all the required fields,Try Again", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        // Property Changed

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnProperty([CallerMemberName] string name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}