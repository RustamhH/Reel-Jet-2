using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Reel_Jet.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReelJet.Database.Entities.Concretes;
using static ReelJet.Application.Models.DatabaseNamespace.Database;

namespace ReelJet.Application.ViewModels.NavigationBarPageModels.SettingsPageModels;

#nullable disable

public class UploadVideoPageModel : INotifyPropertyChanged {

    // Private Fields

    private string posterPath;

    // Binding Properties

    public string Hour { get; set; }
    public string Title { get; set; }
    public string Minute { get; set; }
    public byte[] Poster { get; set; }
    public string MovieLink { get; set; }
    public string Description { get; set; }
    public ICommand? UploadVideoCommand { get; set; }
    public ICommand? UploadPosterCommand { get; set; }
    public string PosterPath { get => posterPath;
        set {
            posterPath = value;
            OnProperty();
        }
    }

    // Constructor

    public UploadVideoPageModel() {
        SetCommands();
    }


    // Functions

    private void SetCommands() {
        UploadVideoCommand = new RelayCommand(UploadVideo);
        UploadPosterCommand = new RelayCommand(UploadPoster);
    }

    private void UploadVideo(object? param) {

        if (Hour == null || Minute == null || Description == null || MovieLink == null || Title == null) {

            MessageBox.Show("You must fill all required fields.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        PersonalMovie personalMovie = new() {

            UserId = CurrentUser.Id,
            MovieLink = MovieLink,
            Description = Description,
            Hour = Hour,
            Poster = Poster,
            Minute = Minute,
            Title = Title,
            UploadDate = DateTime.Now,
            LikeCount = 0,
            ViewCount = 0
        };
        DbContext.PersonalMovies.Add(personalMovie);
        DbContext.SaveChanges();

        MessageBox.Show("Movie uploaded successfully.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void UploadPoster(object? param) {

        OpenFileDialog fileDialog = new OpenFileDialog();
        fileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All files (*.*)|*.*";

        if (fileDialog.ShowDialog() == true) {
            try {
                PosterPath = fileDialog.FileName;
                using (FileStream fs = new FileStream(fileDialog.FileName, FileMode.Open, FileAccess.Read)) {
                    using (BinaryReader br = new BinaryReader(fs)) {
                        Poster = br.ReadBytes((int)fs.Length);
                    }
                }
            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }

    // INotifyPropertChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnProperty([CallerMemberName] string? name = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}