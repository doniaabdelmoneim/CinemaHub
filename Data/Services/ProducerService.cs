using CinemaHub.Data.Base;
using CinemaHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Data.Services
{
    public class ProducerService: EntityBaseRepository<Producer>, IProducerService
    {
        private readonly AppDbContext _context;
        public ProducerService(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Producer> GetByIdWithMoviesAsync (int id)
        {
            return await _context.Producers
                .Include (p => p.Movies)
                .FirstOrDefaultAsync (p => p.Id == id);
        }
    }
}
