using CinemaHub.Data.Base;

namespace CinemaHub.Models
{
    public class ActorMovies:IEntityBase
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
