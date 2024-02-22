using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class CommentsMoviesConfiguration : IEntityTypeConfiguration<CommentsMovies> { 
    public void Configure(EntityTypeBuilder<CommentsMovies> builder) {

        builder.Property(p => p.CommentId).IsRequired();
        builder.Property(p => p.MovieId).IsRequired();

        // Relations

        builder.HasOne(p => p.Movie).WithMany(p => p.CommentsMovies).HasForeignKey(p => p.MovieId);
        builder.HasOne(p => p.Comment).WithMany(p => p.CommentsMovies).HasForeignKey(p => p.CommentId);
    }
}