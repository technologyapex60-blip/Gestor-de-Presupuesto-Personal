using PP.Application.Core;
using PP.Domain.Entities;

namespace PP.Application.Contract
{
    public interface IGastoService : IBaseService<Gasto>
    {
        Task<ServiceResult<IEnumerable<Gasto>>> GetByUsuarioIdAsync(int usuarioId);
        Task<ServiceResult<IEnumerable<Gasto>>> GetByCategoriaIdAsync(int categoriaId);
    }
}