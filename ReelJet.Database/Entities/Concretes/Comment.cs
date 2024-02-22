using System;
using System.Linq;
using System.Text;
using ReelJet_2.Entities;

namespace ReelJet.Database.Entities.Concretes;

public class Comment : BaseEntity {

    public DateTime PostedTime { get; set; }
    public string? Content { get; set; }
    public int LikeCount { get; set; }
    public int UserId { get; set; }
 
    // Navigation Properties

    public virtual User User { get; set; }
    public virtual ICollection<CommentsMovies> CommentsMovies { get; set; }
    public virtual ICollection<CommentsPersonalMovies> CommentsPersonalMovies { get; set; }
}