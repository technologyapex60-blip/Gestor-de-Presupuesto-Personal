using PP.Application.Contract;
using PP.Application.Core;
using PP.Domain.Entities;
using PP.Domain.Repository;

namespace PP.Application.Service
{
    public class IngresoService : BaseService<Ingreso>, IIngresoService
    {
        private readonly IIngresoRepository _ingresoRepository;

        public IngresoService(IIngresoRepository ingresoRepository) : base(ingresoRepository)
        {
            _ingresoRepository = ingresoRepository;
        }

        public async Task<ServiceResult<IEnumerable<Ingreso>>> GetByUsuarioIdAsync(int usuarioId)
        {
            var ingresos = await _ingresoRepository.GetByUsuarioIdAsync(usuarioId);
            return ServiceResult<IEnumerable<Ingreso>>.Ok(ingresos);
        }

        public async Task<ServiceResult<IEnumerable<Ingreso>>> GetByCategoriaIdAsync(int categoriaId)
        {
            var ingresos = await _ingresoRepository.GetByCategoriaIdAsync(categoriaId);
            return ServiceResult<IEnumerable<Ingreso>>.Ok(ingresos);
        }
    }
}