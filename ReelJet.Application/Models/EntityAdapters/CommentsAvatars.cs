using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using ReelJet.Database.Entities.Concretes;

namespace ReelJet.Application.Models.EntityAdapters;

public class CommentsAvatars {

    public int Id { get; set; }
    public User User { get; set; }
    public int LikeCount { get; set; }
    public string? Content { get; set; }
    public BitmapImage Avatar { get; set; }
    public DateTime PostedTime { get; set; }
    public virtual ICollection<CommentsMovies> CommentsMovies { get; set; }
    public virtual ICollection<CommentsPersonalMovies> CommentsPersonalMovies { get; set; }
}