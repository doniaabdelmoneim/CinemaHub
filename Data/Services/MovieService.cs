using CinemaHub.Data.Base;
using CinemaHub.Data.ViewModels;
using CinemaHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Data.Services
{
    public class MovieService:EntityBaseRepository<Movie>,IMovieService
    {
        private readonly AppDbContext _context;
        public MovieService (AppDbContext context) : base (context) {
            _context = context;
        }

        public async Task AddNewMovieAsync (NewMovieVM data)
        {
            var newMovie = new Movie ()
            {
                Title = data.Title,
                Description = data.Description,
                PosterUrl = data.PosterUrl,
                DateAdded = data.startDate,
                ReleaseDate = data.EndDate,
                price = data.price,
                CinemaId = data.CinemaId,
                ProducerId = data.ProducerId
            };
            await _context.Movies.AddAsync (newMovie);
            await _context.SaveChangesAsync ();
            //add actor movie
            foreach (var actorId in data.ActorsIds)
            {
                var newActorMovie = new ActorMovies ()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.ActorMovies.AddAsync (newActorMovie);
            }
            await _context.SaveChangesAsync ();


        }

        public async Task<Movie> GetMovieByIdAsync (int id)
        {
            var movieDetails =await  _context.Movies
                .Include (n => n.Cinema)
                .Include (n => n.Producer)
                .Include (n => n.ActorMovies).ThenInclude (n => n.Actor)
                .FirstOrDefaultAsync (n => n.Id == id);
            return  movieDetails;
        }

        public async  Task<NewMovieDropdownsVM> GetNewMovieDropdownsVM ()
        {
            var response = new NewMovieDropdownsVM ()
            {
                actors = await _context.Actors.OrderBy (n => n.FirstName ).ToListAsync (),
               producers = await _context.Producers.OrderBy (n => n.Name).ToListAsync (),
                cinemas = await _context.Cinemas.OrderBy (n => n.Name).ToListAsync (),
            };
            return response;
        }

        public async Task UpdateMovieAsync (NewMovieVM data)
        {
            var dbMovie =await  _context.Movies.FirstOrDefaultAsync (n => n.Id == data.Id);
            if (dbMovie != null)
            {
                dbMovie.Title = data.Title;
                dbMovie.Description = data.Description;
                dbMovie.PosterUrl = data.PosterUrl;
                dbMovie.DateAdded = data.startDate;
                dbMovie.ReleaseDate = data.EndDate;
                dbMovie.price = data.price;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.ProducerId = data.ProducerId;
                await _context.SaveChangesAsync ();
            }
            //remove old actors
            var oldActorDb =  _context.ActorMovies
                .Where (n => n.MovieId == data.Id).ToList ();
                 _context.ActorMovies.RemoveRange (oldActorDb);
            await _context.SaveChangesAsync ();
            //add new actors
            foreach (var actorId in data.ActorsIds)
            {
                var newActorMovie = new ActorMovies ()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.ActorMovies.AddAsync (newActorMovie);
            }
            await _context.SaveChangesAsync ();


        }
    }
}
