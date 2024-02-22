using System;
using Reel_Jet.ViewModels.RegistrationPageModels;
using System.Windows.Controls;

namespace Reel_Jet.Views {
    public partial class LoadingPage : Page {
        public LoadingPage(Frame frame) {
            InitializeComponent();
            DataContext = new LoadingPageModel(frame);
        }
    }
}
