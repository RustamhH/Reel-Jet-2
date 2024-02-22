using System;
using ReelJet.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ReelJet.Database.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie> {
    public void Configure(EntityTypeBuilder<Movie> builder) {

        builder.Property(m => m.Title).IsRequired();
        builder.Property(m => m.Year);
        builder.Property(m => m.Rated);
        builder.Property(m => m.Released);
        builder.Property(m => m.Runtime);
        builder.Property(m => m.Genre);
        builder.Property(m => m.Director);
        builder.Property(m => m.Writer);
        builder.Property(m => m.Actors);
        builder.Property(m => m.Plot);
        builder.Property(m => m.Language);
        builder.Property(m => m.Country);
        builder.Property(m => m.Awards);
        builder.Property(m => m.Poster);
        builder.Property(m => m.Metascore);
        builder.Property(m => m.imdbRating);
        builder.Property(m => m.imdbVotes);
        builder.Property(m => m.imdbID);
        builder.Property(m => m.Type);
        builder.Property(m => m.DVD);
        builder.Property(m => m.BoxOffice);
        builder.Property(m => m.Production);
        builder.Property(m => m.Website);
        builder.Property(m => m.Response);
        builder.Property(m => m.LikeCount);
        builder.Property(m => m.ViewCount);

        // Relations

        builder.HasMany(m => m.Ratings).WithOne(m => m.Movie).HasForeignKey(m => m.MovieId);
        builder.HasMany(m => m.WatchList).WithOne(m => m.Movie).HasForeignKey(m => m.MovieId);
        builder.HasMany(m => m.HistoryList).WithOne(m => m.Movie).HasForeignKey(m => m.MovieId);
        builder.HasMany(m => m.CommentsMovies).WithOne(m => m.Movie).HasForeignKey(m => m.MovieId);
    }
}