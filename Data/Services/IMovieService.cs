using CinemaHub.Data.Base;
using CinemaHub.Data.ViewModels;
using CinemaHub.Models;

namespace CinemaHub.Data.Services
{
    public interface IMovieService:IEntityBaseRepository<Movie>
    {

        Task<Movie> GetMovieByIdAsync(int id);
        Task<NewMovieDropdownsVM> GetNewMovieDropdownsVM ();
        Task AddNewMovieAsync (NewMovieVM data);
        Task UpdateMovieAsync (NewMovieVM data);
    }
}
 