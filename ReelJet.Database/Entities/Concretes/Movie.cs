using System;
using System.Linq;
using System.Text;
using ReelJet.Database.Entities.Abstracts;
using ReelJet.Database.Entities.Concretes;

namespace ReelJet.Database.Entities;

public class Movie : BaseMovie {

    public string Title { get; set; }
    public string Year { get; set; }
    public string Rated { get; set; }
    public string Released { get; set; }
    public string Runtime { get; set; }
    public string Genre { get; set; }
    public string Director { get; set; }
    public string Writer { get; set; }
    public string Actors { get; set; }
    public string Plot { get; set; }
    public string Language { get; set; }
    public string Country { get; set; }
    public string Awards { get; set; }
    public string Poster { get; set; }
    public string Metascore { get; set; }
    public string imdbRating { get; set; }
    public string imdbVotes { get; set; }
    public string imdbID { get; set; }
    public string Type { get; set; }
    public string DVD { get; set; }
    public string BoxOffice { get; set; }
    public string Production { get; set; }
    public string Website { get; set; }
    public string Response { get; set; }

    // Navigation Property

    public virtual ICollection<Rating> Ratings { get; set; }
    public virtual ICollection<UserWatchList> WatchList { get; set; }
    public virtual ICollection<UserHistoryList> HistoryList { get; set; }
    public virtual ICollection<CommentsMovies> CommentsMovies { get; set; }

}