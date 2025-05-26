using CinemaHub.Data.Base;
using CinemaHub.Models;

namespace CinemaHub.Data.Services
{
    public class CinemaService:EntityBaseRepository<Cinema>,ICinemaService
    {
        private readonly AppDbContext _context;
        public CinemaService (AppDbContext context) : base (context) { }

    }
}
