using CinemaHub.Data.Base;
using CinemaHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Data.Services
{
    public class ActorService :EntityBaseRepository<Actor>, IActorService
    {
        private readonly AppDbContext _context;
        public ActorService (AppDbContext context):base(context){}

  
   
       
    }
}
