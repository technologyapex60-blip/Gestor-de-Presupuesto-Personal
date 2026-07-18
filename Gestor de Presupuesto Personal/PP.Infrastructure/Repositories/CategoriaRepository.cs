using Microsoft.EntityFrameworkCore;
using PP.Domain.Entities;
using PP.Domain.Repository;
using PP.Infrastructure.Context;
using PP.Infrastructure.Core;

namespace PP.Infrastructure.Repositories
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(PresupuestoPersonalContext context) : base(context) { }

        public async Task<IEnumerable<Categoria>> GetByTipoAsync(string tipo)
            => await _dbSet.Where(c => c.Tipo == tipo && c.Activo).ToListAsync();
    }
}