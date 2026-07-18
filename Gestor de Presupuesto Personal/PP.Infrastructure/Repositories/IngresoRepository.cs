using Microsoft.EntityFrameworkCore;
using PP.Domain.Entities;
using PP.Domain.Repository;
using PP.Infrastructure.Context;
using PP.Infrastructure.Core;

namespace PP.Infrastructure.Repositories
{
    public class IngresoRepository : BaseRepository<Ingreso>, IIngresoRepository
    {
        public IngresoRepository(PresupuestoPersonalContext context) : base(context) { }

        public async Task<IEnumerable<Ingreso>> GetByUsuarioIdAsync(int usuarioId)
            => await _dbSet.Where(i => i.UsuarioId == usuarioId && i.Activo).ToListAsync();

        public async Task<IEnumerable<Ingreso>> GetByCategoriaIdAsync(int categoriaId)
            => await _dbSet.Where(i => i.CategoriaId == categoriaId && i.Activo).ToListAsync();
    }
}