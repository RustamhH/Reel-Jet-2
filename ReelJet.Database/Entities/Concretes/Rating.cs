using System;
using System.Linq;
using ReelJet_2.Entities;

namespace ReelJet.Database.Entities.Concretes;

public class Rating : BaseEntity {

    public string Source { get; set; }
    public string Value { get; set; }
    public int MovieId { get; set; }

    // Navigation Properties

    public virtual Movie Movie { get; set; }
}