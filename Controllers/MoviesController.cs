using CinemaHub.Data;
using CinemaHub.Data.Services;
using CinemaHub.Data.Static;
using CinemaHub.Data.ViewModels;
using CinemaHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController (IMovieService movieService)
        {
            _movieService = movieService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index ()
        {
            var allMovies = await _movieService.GetAllAsync(n=>n.Cinema);
            return View(allMovies);
        }
        //search
        public async Task<IActionResult> Filter (string searchString)
        {
            var allMovies = await _movieService.GetAllAsync (n => n.Cinema);
            if (!string.IsNullOrEmpty (searchString))
            {
                var filteredResult = allMovies.Where (n => n.Title.ToLower ().Contains (searchString.ToLower ()) || n.Description.ToLower ().Contains (searchString.ToLower ())).ToList ();
                return View ("Index", filteredResult);
            }
            return View ("Index", allMovies);
        }
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _movieService.GetNewMovieDropdownsVM ();
            ViewBag.Cinemas = new SelectList (movieDropdownsData.cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList (movieDropdownsData.producers, "Id", "Name");
            ViewBag.Actors = new SelectList (movieDropdownsData.actors, "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if(!ModelState.IsValid)
            {
                return View (movie);
            }
            var movieDropdownsData = await _movieService.GetNewMovieDropdownsVM ();
            ViewBag.Cinemas = new SelectList (movieDropdownsData.cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList (movieDropdownsData.producers, "Id", "Name");
            ViewBag.Actors = new SelectList (movieDropdownsData.actors, "Id", "FullName");
            await _movieService.AddNewMovieAsync (movie);
            return RedirectToAction("Index");

        }

        public async Task <IActionResult> Edit (int id)
        {
            var movieDetails = await _movieService.GetMovieByIdAsync (id);
            if (movieDetails == null) return View ("NotFound");
            var response = new NewMovieVM ()
            {
                Id = movieDetails.Id,
                Title = movieDetails.Title,
                Description = movieDetails.Description,
                PosterUrl = movieDetails.PosterUrl,
                startDate = movieDetails.DateAdded,
                EndDate = movieDetails.ReleaseDate,
                price = movieDetails.price,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorsIds = movieDetails.ActorMovies.Select (n => n.ActorId).ToList ()

            };
            var movieDropdownsData = await _movieService.GetNewMovieDropdownsVM ();
            ViewBag.Cinemas = new SelectList (movieDropdownsData.cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList (movieDropdownsData.producers, "Id", "Name");
            ViewBag.Actors = new SelectList (movieDropdownsData.actors, "Id", "FullName");
            return View ();

        }
        [HttpPost]
        public async Task<IActionResult> Edit (int id ,NewMovieVM movie)
        {
            if (id != movie.Id) return View ("NotFound");
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _movieService.GetNewMovieDropdownsVM ();
                ViewBag.Cinemas = new SelectList (movieDropdownsData.cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList (movieDropdownsData.producers, "Id", "Name");
                ViewBag.Actors = new SelectList (movieDropdownsData.actors, "Id", "FullName");
                return View (movie);
            }
            await _movieService.UpdateMovieAsync ( movie);
            return RedirectToAction ("Index");

        }

        [AllowAnonymous]
        public async Task<IActionResult> Details (int id)
        {
            var movieDetails = await _movieService.GetByIdAsync (id);
            if (movieDetails == null) return View ("NotFound");
            return View (movieDetails);
        }
        public async Task<IActionResult> Delete (int id)
        {
            var movieDetails = await _movieService.GetByIdAsync (id);
            if (movieDetails == null) return View ("NotFound");
            return View (movieDetails);
        }
        [HttpPost, ActionName ("Delete")]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var movieDetails = await _movieService.GetByIdAsync (id);
            if (movieDetails == null) return View ("NotFound");
            await _movieService.DeleteAsync (id);
            return RedirectToAction ("Index");
        }




    }
}
