using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class UserHistoryListConfiguration : IEntityTypeConfiguration<UserHistoryList> {

    public void Configure(EntityTypeBuilder<UserHistoryList> builder) {

        builder.Property(l => l.MovieId);
        builder.Property(l => l.UserId);

        // Relations

        builder.HasOne(l => l.User).WithMany(l => l.HistoryList).HasForeignKey(l => l.UserId);
        builder.HasOne(l => l.Movie).WithMany(l => l.HistoryList).HasForeignKey(l => l.MovieId);

    }
}