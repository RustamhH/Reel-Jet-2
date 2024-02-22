using System;
using System.Windows;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using ReelJet.Database.Entities.Concretes;

#nullable disable

namespace ReelJet.Application.Models.EntityAdapters {

    public class UserAdapter : INotifyPropertyChanged {

        // Private Fields

        private string _name;
        private string _surname;
        private string _username;
        private string _phone;
        private string _email;
        private string _password;
        private int? _age;

        // Binding Properties

        public byte[] Avatar { get; set; }
        public string Name {
            get => _name;
            set {
                _name = value; OnProperty();
            }
        }

        public string Surname {
            get => _surname;
            set {
                _surname = value; OnProperty();
            }
        }
        public int? Age {
            get => _age;
            set {
                if (value < 6)
                    MessageBox.Show("Invalid Age", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _age = value; OnProperty();
            }
        }

        public string Username {
            get => _username;
            set {
                if (!Regex.IsMatch(value, "^[a-zA-Z0-9]+([._]?[a-zA-Z0-9]+)*$"))
                    MessageBox.Show("Invalid Username", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _username = value; OnProperty();
            }
        }

        public string PhoneNumber {
            get => _phone;
            set {
                if (!Regex.IsMatch(value!, "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{2}[-\\s\\.]?[0-9]{2}$"))
                    MessageBox.Show("Invalid Phone Number", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _phone = value; OnProperty();
            }
        }


        public string Email {
            get { return _email; }
            set {
                if (!Regex.IsMatch(value, "[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+"))
                    MessageBox.Show("Invalid Email", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _email = value; OnProperty();
            }
        }

        public string Password {
            get { return _password; }
            set {
                if (!Regex.IsMatch(value, "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$"))
                    MessageBox.Show("Invalid Password", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    _password = value; OnProperty();
            }
        }

        // Functions

        public User ConvertToUser() {

            return new User() {
                Name = this.Name,
                Surname = this.Surname,
                Age = this.Age,
                Username = this.Username,
                Email = this.Email,
                Password = this.Password,
                PhoneNumber = this.PhoneNumber,
                Avatar = this.Avatar
            };
        }

        // Property Changed

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnProperty([CallerMemberName] string? name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}