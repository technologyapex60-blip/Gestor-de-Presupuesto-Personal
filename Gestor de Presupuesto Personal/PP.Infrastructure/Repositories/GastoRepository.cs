using Microsoft.EntityFrameworkCore;
using PP.Domain.Entities;
using PP.Domain.Repository;
using PP.Infrastructure.Context;
using PP.Infrastructure.Core;

namespace PP.Infrastructure.Repositories
{
    public class GastoRepository : BaseRepository<Gasto>, IGastoRepository
    {
        public GastoRepository(PresupuestoPersonalContext context) : base(context) { }

        public async Task<IEnumerable<Gasto>> GetByUsuarioIdAsync(int usuarioId)
            => await _dbSet.Where(g => g.UsuarioId == usuarioId && g.Activo).ToListAsync();

        public async Task<IEnumerable<Gasto>> GetByCategoriaIdAsync(int categoriaId)
            => await _dbSet.Where(g => g.CategoriaId == categoriaId && g.Activo).ToListAsync();
    }
}