using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class UserPersonalMovieHistoryListConfiguration : IEntityTypeConfiguration<UserPersonalMovieHistoryList> {

    public void Configure(EntityTypeBuilder<UserPersonalMovieHistoryList> builder) {

        builder.Property(p => p.UserId);
        builder.Property(p => p.PersonalMovieId);

        // Navigation Bars

        builder.HasOne(p => p.User).WithMany(p => p.PersonalMovieHistoryList).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(p => p.PersonalMovie).WithMany(p => p.HistoryList).HasForeignKey(p => p.PersonalMovieId);
    }
}
