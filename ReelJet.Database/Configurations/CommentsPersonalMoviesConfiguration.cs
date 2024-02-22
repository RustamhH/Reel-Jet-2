using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class CommentsPersonalMoviesConfiguration : IEntityTypeConfiguration<CommentsPersonalMovies> {

    public void Configure(EntityTypeBuilder<CommentsPersonalMovies> builder) {

        builder.Property(p => p.CommentId).IsRequired();
        builder.Property(p => p.PersonalMovieId).IsRequired();

        // Relations

        builder.HasOne(p => p.Comment).WithMany(p => p.CommentsPersonalMovies).HasForeignKey(p => p.CommentId);
        builder.HasOne(p => p.PersonalMovie).WithMany(p => p.CommentsPersonalMovies).HasForeignKey(p => p.PersonalMovieId).OnDelete(DeleteBehavior.NoAction);
    }
}