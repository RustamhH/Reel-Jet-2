using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
 

namespace Reel_Jet.Utilities {
    public partial class CustomPasswordBox : UserControl {
        
        // Private Fields

        private bool _isPasswordChanging;
        
        // Properties

        public string PlaceHolder {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }
        public string Password {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Dependancy Property

        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(string), typeof(CustomPasswordBox));
        

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(CustomPasswordBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        // Constructor
        
        public CustomPasswordBox() {
            InitializeComponent();
        }

        // Functions

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is CustomPasswordBox passwordBox) {
                passwordBox.UpdatePassword();
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e) {
            _isPasswordChanging = true;
            Password = passwordBox.Password;
            _isPasswordChanging = false;
        }

        private void UpdatePassword() {
            if (!_isPasswordChanging) {
                passwordBox.Password = Password;
            }
        }

        private void passwordBox_GotFocus(object sender, RoutedEventArgs e) {
            passwordBox.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}