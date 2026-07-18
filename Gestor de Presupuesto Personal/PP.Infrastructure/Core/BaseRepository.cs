using Microsoft.EntityFrameworkCore;
using PP.Domain.Core;
using PP.Domain.Repository;
using PP.Infrastructure.Context;

namespace PP.Infrastructure.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly PresupuestoPersonalContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(PresupuestoPersonalContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbSet.Where(e => e.Activo).ToListAsync();

        public async Task<T?> GetByIdAsync(int id)
            => await _dbSet.FirstOrDefaultAsync(e => e.Id == id && e.Activo);

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            entity.FechaModificacion = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is null) return;

            entity.Activo = false;
            await UpdateAsync(entity);
        }
    }
}