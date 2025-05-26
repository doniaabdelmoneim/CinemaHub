using CinemaHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CinemaHub.Data
{
    public class AppDbContext :IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base (options)
        {
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovies> ActorMovies { get; set; }

        // orders related  tables

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }






        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ActorMovies> ()
                .HasKey (am => new { am.ActorId, am.MovieId });

            modelBuilder.Entity<ActorMovies> ()
                .HasOne (am => am.Movie)
                .WithMany (m => m.ActorMovies)
                .HasForeignKey (am => am.MovieId);

            modelBuilder.Entity<ActorMovies> ()
                .HasOne (am => am.Actor)
                .WithMany (a => a.ActorMovies)
                .HasForeignKey (am => am.ActorId);

            base.OnModelCreating (modelBuilder);
        }
    }


    }

