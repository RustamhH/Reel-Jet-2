using System;
using System.Linq;
using System.Windows.Controls;
using ReelJet.Application.ViewModels.MoviePageModels;

namespace ReelJet.Application.Views.MoviePages {
    public partial class ForYouPage : Page {
        public ForYouPage(Frame frame) {
            InitializeComponent();
            DataContext = new ForYouPageModel(frame);
        }
    }
}
