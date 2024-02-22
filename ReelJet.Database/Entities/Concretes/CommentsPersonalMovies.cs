using System;
using System.Linq;
using ReelJet_2.Entities;

namespace ReelJet.Database.Entities.Concretes;

public class CommentsPersonalMovies : BaseEntity {

    public int CommentId { get; set; }
    public int PersonalMovieId { get; set; }

    // Navigation Properties

    public virtual Comment Comment { get; set; }
    public virtual PersonalMovie PersonalMovie { get; set; }
}