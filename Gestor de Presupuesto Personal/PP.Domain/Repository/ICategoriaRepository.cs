using PP.Domain.Entities;

namespace PP.Domain.Repository
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetByTipoAsync(string tipo);
    }
}