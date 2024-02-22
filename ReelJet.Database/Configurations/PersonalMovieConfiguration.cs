using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class PersonalMovieConfiguration : IEntityTypeConfiguration<PersonalMovie> {
    public void Configure(EntityTypeBuilder<PersonalMovie> builder) {

        builder.Property(m => m.Hour).IsRequired();
        builder.Property(m => m.Title).IsRequired();
        builder.Property(m => m.Minute).IsRequired();
        builder.Property(m => m.MovieLink).IsRequired();
        builder.Property(m => m.UploadDate).IsRequired();
        builder.Property(m => m.Description).IsRequired();
        builder.Property(m => m.Poster).HasColumnType("image");

        // Relations

        builder.HasOne(p => p.User).WithMany(p => p.ForYouMovies).HasForeignKey(p => p.UserId);
        builder.HasMany(m => m.WatchList).WithOne(p => p.PersonalMovie).HasForeignKey(p => p.PersonalMovieId);
        builder.HasMany(m => m.HistoryList).WithOne(p => p.PersonalMovie).HasForeignKey(p => p.PersonalMovieId);
        builder.HasMany(m => m.CommentsPersonalMovies).WithOne(p => p.PersonalMovie).HasForeignKey(p => p.PersonalMovieId);
    }
}