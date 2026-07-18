using PP.Application.Core;
using PP.Domain.Entities;

namespace PP.Application.Contract
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Task<ServiceResult<Usuario>> GetByCorreoAsync(string correo);
    }
}