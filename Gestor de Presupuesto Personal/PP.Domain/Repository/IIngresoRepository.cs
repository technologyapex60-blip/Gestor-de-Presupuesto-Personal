using PP.Domain.Entities;

namespace PP.Domain.Repository
{
    public interface IIngresoRepository : IBaseRepository<Ingreso>
    {
        Task<IEnumerable<Ingreso>> GetByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Ingreso>> GetByCategoriaIdAsync(int categoriaId);
    }
}