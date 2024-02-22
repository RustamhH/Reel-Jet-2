using System;
using System.Linq;
using System.Text;
using ReelJet.Database.Entities.Concretes;
using ReelJet_2.Entities;

namespace ReelJet.Database.Entities.Abstracts;

public abstract class BaseMovie : BaseEntity {

    public int LikeCount { get; set; } 
    public int ViewCount { get; set; }
}