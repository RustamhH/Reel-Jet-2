using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User> {

    public void Configure(EntityTypeBuilder<User> builder) {

        builder.Property(p => p.Name);
        builder.Property(p => p.Surname);
        builder.Property(p => p.Age);
        builder.Property(p => p.Username).IsRequired();
        builder.Property(p => p.Avatar).HasColumnType("image");
        builder.Property(p => p.PhoneNumber);
        builder.Property(p => p.Password).IsRequired();
        builder.Property(p => p.Email).IsRequired();

        // Relations

        builder.HasMany(p => p.WatchList).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        builder.HasMany(p => p.HistoryList).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        builder.HasMany(p => p.ForYouMovies).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        builder.HasMany(p => p.PersonalMovieWatchList).WithOne(p => p.User).HasForeignKey(p => p.UserId);
        builder.HasMany(p => p.PersonalMovieHistoryList).WithOne(p => p.User).HasForeignKey(p => p.UserId);
    }
}