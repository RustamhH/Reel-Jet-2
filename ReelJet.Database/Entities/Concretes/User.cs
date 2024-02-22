using System;
using System.Linq;
using ReelJet_2.Entities;

namespace ReelJet.Database.Entities.Concretes;

public class User : Person {

    public int? Age { get; set; }
    public string? PhoneNumber { get; set; }

    // Navigation Properties

    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<UserWatchList> WatchList { get; set; }
    public virtual ICollection<PersonalMovie> ForYouMovies { get; set; }
    public virtual ICollection<UserHistoryList> HistoryList { get; set; }
    public virtual ICollection<UserPersonalMovieWatchList> PersonalMovieWatchList { get; set; }
    public virtual ICollection<UserPersonalMovieHistoryList> PersonalMovieHistoryList { get; set; }
}