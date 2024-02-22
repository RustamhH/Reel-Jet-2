using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class UserPersonalMovieWatchListConfiguration : IEntityTypeConfiguration<UserPersonalMovieWatchList> {

    public void Configure(EntityTypeBuilder<UserPersonalMovieWatchList> builder) {

        builder.Property(l => l.UserId);
        builder.Property(l => l.PersonalMovieId);

        // Relations

        builder.HasOne(l => l.User).WithMany(l => l.PersonalMovieWatchList).HasForeignKey(l => l.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(l => l.PersonalMovie).WithMany(l => l.WatchList).HasForeignKey(l => l.PersonalMovieId);
    }
}
