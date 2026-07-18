using PP.Application.Core;
using PP.Domain.Entities;

namespace PP.Application.Contract
{
    public interface ICategoriaService : IBaseService<Categoria>
    {
        Task<ServiceResult<IEnumerable<Categoria>>> GetByTipoAsync(string tipo);
    }
}