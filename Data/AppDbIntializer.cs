using CinemaHub.Data.Enums;
using CinemaHub.Data.Static;
using CinemaHub.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CinemaHub.Data
{
    public class AppDbInitializer
    {
        public static void Seed (IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope ())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext> ();
                if (context == null)
                {
                    throw new InvalidOperationException ("AppDbContext is not registered in the service provider.");
                }
                context.Database.EnsureCreated ();

                // Seed Cinemas
                if (!context.Cinemas.Any ())
                {
                    context.Cinemas.AddRange (new List<Cinema> ()
                    {
                        new Cinema()
                        {
                            Name = "Cineplex Downtown",
                            Description = "The largest cinema in downtown with 10 halls and premium seating.",
                            Location = "123 Main St, Downtown",
                            ContactNumber = "555-123-4567",
                            ContactEmail = "info@cineplexdowntown.com",
                            OpeningHours = "10:00 AM - 11:00 PM Daily",
                            TotalHalls = 10,
                            CinemaImageUrl = "/images/cinemas/cineplex-downtown.jpeg"
                        },
                        new Cinema()
                        {
                            Name = "Starlight Cinemas",
                            Description = "Modern cinema with IMAX and 3D capabilities.",
                            Location = "456 Oak Ave, Westside",
                            ContactNumber = "555-987-6543",
                            ContactEmail = "contact@starlightcinemas.com",
                            OpeningHours = "9:00 AM - 12:00 AM Daily",
                            TotalHalls = 8,
                            CinemaImageUrl = "/images/cinemas/starlight.jpg"
                        },
                        new Cinema()
                        {
                            Name = "Royal Theater",
                            Description = "Historic theater with classic movie screenings.",
                            Location = "789 Elm St, Old Town",
                            ContactNumber = "555-456-7890",
                            ContactEmail = "royal@royaltheater.com",
                            OpeningHours = "11:00 AM - 10:00 PM Daily",
                            TotalHalls = 5,
                            CinemaImageUrl = "/images/cinemas/royal-theater.jpeg"
                        }
                    });
                    context.SaveChanges ();
                }

                // Seed Producers
                if (!context.Producers.Any ())
                {
                    context.Producers.AddRange (new List<Producer> ()
                    {
                        new Producer()
                        {
                            CompanyLogoUrl = "/images/producers/warner-bros.jpeg",
                            Name = "Warner Bros",
                            Bio = "American entertainment company known for its film studio.",
                            ContactEmail = "info@warnerbros.com"
                        },
                        new Producer()
                        {
                            CompanyLogoUrl = "/images/producers/disney.jpg",
                            Name = "Disney",
                            Bio = "American diversified multinational mass media and entertainment conglomerate.",
                            ContactEmail = "contact@disney.com"
                        },
                        new Producer()
                        {
                            CompanyLogoUrl = "/images/producers/universal.png",
                            Name = "Universal Pictures",
                            Bio = "American film production and distribution company.",
                            ContactEmail = "support@universal.com"
                        }
                    });
                    context.SaveChanges ();
                }

                // Seed Actors
                if (!context.Actors.Any ())
                {
                    context.Actors.AddRange (new List<Actor> ()
                    {
                        new Actor()
                        {
                            PhotoUrl = "/images/actors/robert-downey.jpeg",
                            FirstName = "Robert",
                            LastName = "Downey Jr.",
                            BirthDate = new DateTime(1965, 4, 4),
                            Bio = "American actor known for his roles in the Marvel Cinematic Universe."
                        },
                        new Actor()
                        {
                            PhotoUrl = "/images/actors/scarlett-johansson.jpg",
                            FirstName = "Scarlett",
                            LastName = "Johansson",
                            BirthDate = new DateTime(1984, 11, 22),
                            Bio = "American actress and singer known for playing Black Widow in the MCU."
                        },
                        new Actor()
                        {
                            PhotoUrl = "/images/actors/tom-hanks.jpg",
                            FirstName = "Tom",
                            LastName = "Hanks",
                            BirthDate = new DateTime(1956, 7, 9),
                            Bio = "American actor and filmmaker known for his versatile roles."
                        },
                        new Actor()
                        {
                            PhotoUrl = "/images/actors/meryl-streep.jpg",
                            FirstName = "Meryl",
                            LastName = "Streep",
                            BirthDate = new DateTime(1949, 6, 22),
                            Bio = "American actress often described as the best actress of her generation."
                        }
                    });
                    context.SaveChanges ();
                }

                // Seed Movies
                if (!context.Movies.Any ())
                {
                    context.Movies.AddRange (new List<Movie> ()
                    {
                        new Movie()
                        {
                            Title = "Avengers: Endgame",
                            Description = "After the devastating events of Avengers: Infinity War, the universe is in ruins. With the help of remaining allies, the Avengers assemble once more in order to reverse Thanos' actions and restore balance to the universe.",
                            DurationMinutes = 181,
                            ReleaseDate = new DateTime(2019, 4, 26),
                            PosterUrl = "/images/movies/avengers-endgame.jpg",
                            Director = "Anthony Russo, Joe Russo",
                            Language = "English",
                            AgeRating = "PG-13",
                            ImdbRating = 8.4,
                            price = 12.99,
                            Category = MovieCategory.Action,
                            CinemaId = 1,
                            ProducerId = 1
                        },
                        new Movie()
                        {
                            Title = "The Lion King",
                            Description = "After the murder of his father, a young lion prince flees his kingdom only to learn the true meaning of responsibility and bravery.",
                            DurationMinutes = 118,
                            ReleaseDate = new DateTime(2019, 7, 19),
                            PosterUrl = "/images/movies/lion-king.jpg",
                            Director = "Jon Favreau",
                            Language = "English",
                            AgeRating = "PG",
                            ImdbRating = 6.9,
                            price = 10.99,
                            Category = MovieCategory.Animation,
                            CinemaId = 2,
                            ProducerId = 2
                        },
                        new Movie()
                        {
                            Title = "Forrest Gump",
                            Description = "The presidencies of Kennedy and Johnson, the events of Vietnam, Watergate, and other historical events unfold through the perspective of an Alabama man with an IQ of 75.",
                            DurationMinutes = 142,
                            ReleaseDate = new DateTime(1994, 7, 6),
                            PosterUrl = "/images/movies/forrest-gump.jpg",
                            Director = "Robert Zemeckis",
                            Language = "English",
                            AgeRating = "PG-13",
                            ImdbRating = 8.8,
                            price = 8.99,
                            Category = MovieCategory.Drama,
                            CinemaId = 3,
                            ProducerId = 3
                        },
                        new Movie()
                        {
                            Title = "Jurassic Park",
                            Description = "A pragmatic paleontologist touring an almost complete theme park on an island in Central America is tasked with protecting a couple of kids after a power failure causes the park's cloned dinosaurs to run loose.",
                            DurationMinutes = 127,
                            ReleaseDate = new DateTime(1993, 6, 11),
                            PosterUrl = "/images/movies/jurassic-park.jpg",
                            Director = "Steven Spielberg",
                            Language = "English",
                            AgeRating = "PG-13",
                            ImdbRating = 8.1,
                            price = 9.99,
                            Category = MovieCategory.Adventure,
                            CinemaId = 1,
                            ProducerId = 3
                        }
                    });
                    context.SaveChanges ();
                }

                // Seed ActorMovies (relationships)
                if (!context.ActorMovies.Any ())
                {
                    context.ActorMovies.AddRange (new List<ActorMovies> ()
                    {
                        new ActorMovies()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new ActorMovies()
                        {
                            ActorId = 2,
                            MovieId = 1
                        },
                        new ActorMovies()
                        {
                            ActorId = 3,
                            MovieId = 3
                        },
                        new ActorMovies()
                        {
                            ActorId = 4,
                            MovieId = 3
                        }
                    });
                    context.SaveChanges ();
                }

            } 
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope=applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>> ();
                if (! await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync (new IdentityRole (UserRoles.Admin));
                }
                if (!await roleManager.RoleExistsAsync (UserRoles.User))
                {
                    await roleManager.CreateAsync (new IdentityRole (UserRoles.User));
                }

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>> ();
                var adminUser = await userManager.FindByEmailAsync ("admin.cinemaHub.com");
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser ()
                    {
                        FullName = "Admin User",
                        UserName = "admin",
                        Email = "admin.cinemaHub.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync (newAdminUser, "Admin@123");
                    await userManager.AddToRoleAsync (newAdminUser, UserRoles.Admin);

                }
                var appUser = await userManager.FindByEmailAsync ("user.cinemaHub.com");
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser ()
                    {
                        FullName = "Application User",
                        UserName = "user",
                        Email = "user.cinemaHub.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync (newAppUser, "User@123");
                    await userManager.AddToRoleAsync (newAppUser, UserRoles.User);
                }

            }

        }
    }
}