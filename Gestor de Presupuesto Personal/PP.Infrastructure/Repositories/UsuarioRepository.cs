using Microsoft.EntityFrameworkCore;
using PP.Domain.Entities;
using PP.Domain.Repository;
using PP.Infrastructure.Context;
using PP.Infrastructure.Core;

namespace PP.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(PresupuestoPersonalContext context) : base(context) { }

        public async Task<Usuario?> GetByCorreoAsync(string correo)
            => await _dbSet.FirstOrDefaultAsync(u => u.Correo == correo && u.Activo);
    }
}