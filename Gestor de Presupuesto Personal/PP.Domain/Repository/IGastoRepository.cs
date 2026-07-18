using PP.Domain.Entities;

namespace PP.Domain.Repository
{
    public interface IGastoRepository : IBaseRepository<Gasto>
    {
        Task<IEnumerable<Gasto>> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Gasto>> GetByCategoriaIdAsync(int categoriaId);
    }
}