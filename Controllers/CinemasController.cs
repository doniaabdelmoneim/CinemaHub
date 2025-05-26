using CinemaHub.Data;
using CinemaHub.Data.Services;
using CinemaHub.Data.Static;
using CinemaHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CinemaHub.Controllers
{
    [Authorize (Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly ICinemaService _cinemaService;

        public CinemasController (ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var cinemas=await _cinemaService.GetAllAsync();
            return View(cinemas);
        }

        public IActionResult Create () {
            return View();

        }
        [HttpPost]
        public async Task <IActionResult> Create(Cinema cinema)
        {
            await _cinemaService.AddAsync(cinema);
            return RedirectToAction("Index");

        }
        [AllowAnonymous]
        public async Task <IActionResult> Details(int id)
        {
            var cinemaDetails=await _cinemaService.GetByIdAsync(id);
            if (cinemaDetails == null) return View ("NotFound");
            return View(cinemaDetails);
        }

        public async Task<ActionResult> Edit (int id) 
        {
            var cinemaDetails = await _cinemaService.GetByIdAsync(id);
            if (cinemaDetails == null) return View ("NotFound");
             return View(cinemaDetails);
        }
        [HttpPost]
        public async Task<ActionResult> Edit (int id,Cinema cinema)
        {
            await _cinemaService.UpdateAsync (id, cinema);
            return RedirectToAction("Index");
        }

        public async Task <IActionResult> Delete (int id )
        {
            var cinemaDetails = await _cinemaService.GetByIdAsync (id);
            if (cinemaDetails == null) return View ("NotFound");
            return View(cinemaDetails);
        }
       [HttpPost,ActionName("Delete")]
       public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var cinemaDetails = await _cinemaService.GetByIdAsync (id);
            if (cinemaDetails == null) return View ("NotFound");
            await _cinemaService.DeleteAsync (id);
            return RedirectToAction("Index");
        }
    }
}
