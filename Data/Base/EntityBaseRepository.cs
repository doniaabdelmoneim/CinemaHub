
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace CinemaHub.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly DbContext _Context;

        public EntityBaseRepository (DbContext context)
        {
            _Context = context;
        }
        public async Task AddAsync (T entity)
        {
            await _Context.Set<T>().AddAsync (entity);
            await _Context.SaveChangesAsync ();

        }

        public async Task DeleteAsync (int id)
        {
            var entity = await _Context.Set<T> ().FirstOrDefaultAsync (e => e.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException ($"Entity with id {id} not found.");
            }
            EntityEntry entityEntry = _Context.Entry (entity);
            entityEntry.State = EntityState.Deleted;
            await _Context.SaveChangesAsync ();
        }

        public async Task<IEnumerable<T>> GetAllAsync ()
        {
            return await _Context.Set<T>().ToListAsync ();
        }

        public async Task<IEnumerable<T>> GetAllAsync (params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _Context.Set<T> ();
            query = includeProperties.Aggregate (query, (current, includeProperty) => current.Include (includeProperty));
            return  await query.ToListAsync ();


        }

        public async Task<T> GetByIdAsync (int id)
        {
            var entity = await _Context.Set<T> ().FirstOrDefaultAsync (e => e.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException ($"Entity with id {id} not found.");
            }
            return entity;
        }


        public async Task UpdateAsync (int id, T entity)
        {
            var existingEntity = await _Context.Set<T> ().FindAsync (id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException ($"Entity with id {id} not found.");
            }

            _Context.Entry (existingEntity).CurrentValues.SetValues (entity);
            await _Context.SaveChangesAsync ();
        }

    }
}
