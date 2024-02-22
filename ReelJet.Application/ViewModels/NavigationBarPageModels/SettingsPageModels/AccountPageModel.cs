using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;
using ReelJet.Database.Entities.Concretes;
using ReelJet.Application.Models.DatabaseNamespace;
using static ReelJet.Application.Models.DatabaseNamespace.Database;

namespace Reel_Jet.ViewModels.NavigationBarPageModels.SettingsPageModels {
    public class AccountPageModel : INotifyPropertyChanged {

        // Private Fields

        private BitmapImage _avatar;

        // Binding Properties

        public BitmapImage Avatar { get => _avatar;
            set { 
                _avatar = value; OnProperty();
            } 
        }
        public User EditedUser { get; set; } = CurrentUser;
        public ICommand ConfirmChangeCommand { get; set; }
        public ICommand EditPfpCommand { get; set; }

        // Constructor

        public AccountPageModel() {

            EditPfpCommand = new RelayCommand(EditPfp);
            ConfirmChangeCommand = new RelayCommand(ConfirmChange);

            Avatar = userAuthentication.Avatar;
        }

        // Functions

        private void EditPfp(object? obj) {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == true) {
                using (FileStream fs = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read)) {
                    using (BinaryReader br = new BinaryReader(fs)) {
                        EditedUser.Avatar = br.ReadBytes((int)fs.Length);
                        Avatar = UserAuthentication.LoadImage(EditedUser.Avatar);
                        
                    }
                }
            }
        }

        private void ConfirmChange(object? sender) {
            userAuthentication.Avatar = Avatar;
            DbContext.SaveChanges();
        }

        // INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnProperty([CallerMemberName] string? name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
