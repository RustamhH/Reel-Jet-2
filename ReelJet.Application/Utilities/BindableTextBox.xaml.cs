using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Reel_Jet.Utilities {
    public partial class BindableTextBox : UserControl {

        // Properties

        public string PlaceHolder {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }
        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Dependancy Property

        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(string), typeof(BindableTextBox));


        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(BindableTextBox));

        // Constructor

        public BindableTextBox() {
            InitializeComponent();
        }

        // Functions

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            txtbox.Foreground = new SolidColorBrush(Colors.White);
        }
    }
}
