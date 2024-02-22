using System;
using System.Linq;
using ReelJet.Database.Entities.Abstracts;

namespace ReelJet.Database.Entities.Concretes;

public class PersonalMovie : BaseMovie {

    public int UserId { get; set; }
    public string Hour { get; set; }
    public string Title { get; set; }
    public string Minute { get; set; }
    public byte[] Poster { get; set; }
    public string MovieLink { get; set; }
    public string Description { get; set; }
    public DateTime UploadDate { get; set; }

    // Navigation Properties

    public virtual User User { get; set; }
    public virtual ICollection<UserPersonalMovieWatchList> WatchList { get; set; }
    public virtual ICollection<UserPersonalMovieHistoryList> HistoryList { get; set; }
    public virtual ICollection<CommentsPersonalMovies> CommentsPersonalMovies { get; set; }
}