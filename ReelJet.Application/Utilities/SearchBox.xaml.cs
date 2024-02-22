using System;
using System.Windows;
using System.Windows.Controls;


namespace Reel_Jet.Utilities {
 
    public partial class SearchBox : UserControl {

        // Constructor

        public SearchBox() {
            InitializeComponent();
        }

        // Properties

        public string PlaceHolder {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }
        public string content {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }


        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(string), typeof(SearchBox));

        // Using a DependencyProperty as the backing store for PlaceHolder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("content", typeof(string), typeof(SearchBox));

    }
}
