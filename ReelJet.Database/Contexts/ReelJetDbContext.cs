using System;
using ReelJet.Database.Entities;
using Microsoft.EntityFrameworkCore;
using ReelJet.Database.Configurations;
using Microsoft.Extensions.Configuration;
using ReelJet.Database.Entities.Concretes;
using Microsoft.EntityFrameworkCore.Proxies;

namespace ReelJet.Database.Contexts;

public class ReelJetDbContext : DbContext {

    // Functions

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

        var connectionStr = new ConfigurationBuilder().AddJsonFile("databasesettings.json").Build().GetConnectionString("Sql");
        optionsBuilder.UseLazyLoadingProxies(true).UseSqlServer(connectionStr);

        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        modelBuilder.ApplyConfiguration(new RatingConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new PersonalMovieConfiguration());
        modelBuilder.ApplyConfiguration(new UserWatchListConfiguration());
        modelBuilder.ApplyConfiguration(new CommentsMoviesConfiguration());
        modelBuilder.ApplyConfiguration(new UserHistoryListConfiguration());
        modelBuilder.ApplyConfiguration(new CommentsPersonalMoviesConfiguration());
        modelBuilder.ApplyConfiguration(new UserPersonalMovieWatchListConfiguration());
        modelBuilder.ApplyConfiguration(new UserPersonalMovieHistoryListConfiguration());

        base.OnModelCreating(modelBuilder);
    }

    // Tables

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Admin> Admins { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<Rating> Ratings { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<UserWatchList> WatchLists { get; set; }
    public virtual DbSet<UserHistoryList> HistoryLists { get; set; }
    public virtual DbSet<PersonalMovie> PersonalMovies { get; set; }
    public virtual DbSet<CommentsMovies> CommentsMovies { get; set; }
    public virtual DbSet<CommentsPersonalMovies> CommentsPersonalMovies { get; set; }
    public virtual DbSet<UserPersonalMovieWatchList> PersonalWatchLists { get; set; }
    public virtual DbSet<UserPersonalMovieHistoryList> PersonalHistoryLists { get; set; }
}