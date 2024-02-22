using System;
using System.Linq;
using ReelJet_2.Entities;
using ReelJet.Database.Entities.Abstracts;

namespace ReelJet.Database.Entities.Concretes;

public class UserHistoryList : BaseEntity {

    public int UserId { get; set; }
    public int MovieId { get; set; }

    // Navigation Properties

    public virtual User User { get; set; }
    public virtual Movie Movie { get; set; }
}