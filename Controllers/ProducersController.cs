using CinemaHub.Data;
using CinemaHub.Data.Services;
using CinemaHub.Data.Static;
using CinemaHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Controllers
{
    [Authorize(Roles =UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducerService _producerService;
        public ProducersController (IProducerService producerService)
        {
            _producerService = producerService;
        }
         [AllowAnonymous]
        public async Task<IActionResult> Index ()
        {
            var producers = await _producerService.GetAllAsync ();
            return View (producers);
        }
        public IActionResult Create ()
        {
            return View ();
        }
        [HttpPost]
        public async Task<IActionResult> Create (Producer producer)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View (producer);
            //}
            await _producerService.AddAsync (producer);
            TempData["Success"] = "Producer created successfully!";
            return RedirectToAction (nameof (Index));
        }
          [AllowAnonymous]
        public async Task<IActionResult> Details (int id)
        {
            var producerDetails = await _producerService.GetByIdAsync (id);
            if (producerDetails == null) return View ("NotFound");
            return View (producerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit (int id)
        {
            var producerDetails = await _producerService.GetByIdAsync (id);
            if (producerDetails == null) return View ("NotFound");
            return View (producerDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit (int id, Producer producer)
        {
            if (id != producer.Id)
            {
                return View ("NotFound");
            }
            //if (!ModelState.IsValid)
            //{
            //    return View (producer);
            //}
                await _producerService.UpdateAsync (id, producer);
                TempData["Success"] = "Producer updated successfully!";
            return RedirectToAction (nameof (Index));
        }
        public async Task<IActionResult> Delete (int id)
        {
            var producerDetails = await _producerService.GetByIdAsync (id);
            if (producerDetails == null) return View ("NotFound");
            return View (producerDetails);
        }
        [HttpPost, ActionName ("Delete")]
        public async Task<IActionResult> DeleteConfirmed (int id)
        {
            var producerDetails = await _producerService.GetByIdAsync (id);
            if (producerDetails == null) return View ("NotFound");
            await _producerService.DeleteAsync (id);
            TempData["Success"] = "Producer deleted successfully!";
            return RedirectToAction (nameof (Index));
        }

    }
}
