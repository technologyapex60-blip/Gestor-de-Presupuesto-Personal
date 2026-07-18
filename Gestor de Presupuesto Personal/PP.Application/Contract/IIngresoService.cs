using PP.Application.Core;
using PP.Domain.Entities;

namespace PP.Application.Contract
{
    public interface IIngresoService : IBaseService<Ingreso>
    {
        Task<ServiceResult<IEnumerable<Ingreso>>> GetByUsuarioIdAsync(int usuarioId);
        Task<ServiceResult<IEnumerable<Ingreso>>> GetByCategoriaIdAsync(int categoriaId);
    }
}