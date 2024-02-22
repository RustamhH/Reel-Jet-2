using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations {

    public class CommentConfiguration : IEntityTypeConfiguration<Comment> {
        public void Configure(EntityTypeBuilder<Comment> builder) {

            builder.Property(p => p.PostedTime).IsRequired();
            builder.Property(p => p.Content).IsRequired();
            builder.Property(p => p.LikeCount).IsRequired();
            builder.Property(p => p.UserId).IsRequired();

            // Relations

            builder.HasOne(p => p.User).WithMany(p => p.Comments).HasForeignKey(p => p.UserId);
            builder.HasMany(p => p.CommentsMovies).WithOne(p => p.Comment).HasForeignKey(p => p.CommentId);
            builder.HasMany(p => p.CommentsPersonalMovies).WithOne(p => p.Comment).HasForeignKey(p => p.CommentId);
        }
    }
}
