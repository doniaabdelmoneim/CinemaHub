using CinemaHub.Data;
using CinemaHub.Data.Services;
using CinemaHub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CinemaHub.Data.Static;

namespace CinemaHub.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorService _actorService;

        public ActorsController (IActorService actorService)
        {
            _actorService = actorService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index ()
        {
            var actors = await _actorService.GetAllAsync ();
            return View (actors);
        }

        public IActionResult Create ()
        {
            return View ();
        }

        [HttpPost]
        public async Task<IActionResult> Create (Actor actor)
        {       
                await _actorService.AddAsync (actor);
                return RedirectToAction ("Index");
            
            //return View (actor);
        }
        [AllowAnonymous]

        public async Task<IActionResult> Details (int id)
        {
            var actorDetails = await _actorService.GetByIdAsync (id);
            if (actorDetails == null) return View ("NotFound");
            return View (actorDetails);
        }

        public async Task<IActionResult> Edit (int id)
        {
            var actorDetails = await _actorService.GetByIdAsync (id);
            if (actorDetails == null) return View ("NotFound");
            return View (actorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit (int id, Actor actor)
        {

            await _actorService.UpdateAsync (id, actor);
            return RedirectToAction (nameof (Index));
        }

        public async Task<IActionResult> Delete (int id)
        {
            var actorDetails = await _actorService.GetByIdAsync (id);
            if (actorDetails == null) return View ("NotFound");
            return View (actorDetails);
        }

        [HttpPost, ActionName ("Delete")]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var actorDetails = await _actorService.GetByIdAsync (id);
            if (actorDetails == null) return View ("NotFound");
            await _actorService.DeleteAsync (id);
            return RedirectToAction (nameof (Index));
        }
    }
}
