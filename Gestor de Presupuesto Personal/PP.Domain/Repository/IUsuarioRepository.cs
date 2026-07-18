using PP.Domain.Entities;

namespace PP.Domain.Repository
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario?> GetByCorreoAsync(string correo);
    }
}