using CinemaHub.Models;

namespace CinemaHub.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM ()
        {
            producers = new List<Producer> ();
            cinemas = new List<Cinema> ();
            actors = new List<Actor> ();
        }
        public List<Producer> producers{ get; set; }
        public List <Cinema> cinemas{ get; set; }
        public List<Actor> actors { get; set; }

    }
}
