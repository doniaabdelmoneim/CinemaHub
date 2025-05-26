using CinemaHub.Data.Base;
using CinemaHub.Models;

namespace CinemaHub.Data.Services
{
    public interface IProducerService : IEntityBaseRepository<Producer>
    {
        Task<Producer> GetByIdWithMoviesAsync (int id);

    }
}
