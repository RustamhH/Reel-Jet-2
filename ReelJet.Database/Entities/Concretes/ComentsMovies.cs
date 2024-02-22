using System;
using ReelJet_2.Entities;
using ReelJet.Database.Entities.Abstracts;

namespace ReelJet.Database.Entities.Concretes;

public class CommentsMovies : BaseEntity {

    public int CommentId { get; set; }
    public int MovieId { get; set; }

    // Navigation Properties

    public virtual Movie Movie { get; set; }
    public virtual Comment Comment { get; set; }
}