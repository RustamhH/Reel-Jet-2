using System;
using System.Linq;
using ReelJet_2.Entities;

namespace ReelJet.Database.Entities.Concretes;

public class UserPersonalMovieHistoryList : BaseEntity {

    public int UserId { get; set; }
    public int PersonalMovieId { get; set; }

    // Navigation Properties

    public virtual User User { get; set; }
    public virtual PersonalMovie PersonalMovie { get; set; }
}