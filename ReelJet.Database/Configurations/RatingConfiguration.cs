using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class RatingConfiguration : IEntityTypeConfiguration<Rating> {  

    public void Configure(EntityTypeBuilder<Rating> builder) {

        builder.Property(p => p.Source);
        builder.Property(p => p.Value);
        builder.Property(p => p.MovieId);

        // Relations

        builder.HasOne(p => p.Movie).WithMany(p => p.Ratings).HasForeignKey(p => p.MovieId);
    }
}