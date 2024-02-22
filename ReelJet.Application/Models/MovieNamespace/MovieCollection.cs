using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reel_Jet.Models.MovieNamespace;

public class MovieCollection {
    public ObservableCollection<ShortMovieInfo> Search { get; set; }
    public string totalResults { get; set; }
    public string Response { get; set; }
}