using PP.Application.Contract;
using PP.Application.Core;
using PP.Domain.Entities;
using PP.Domain.Repository;

namespace PP.Application.Service
{
    public class GastoService : BaseService<Gasto>, IGastoService
    {
        private readonly IGastoRepository _gastoRepository;

        public GastoService(IGastoRepository gastoRepository) : base(gastoRepository)
        {
            _gastoRepository = gastoRepository;
        }

        public async Task<ServiceResult<IEnumerable<Gasto>>> GetByUsuarioIdAsync(int usuarioId)
        {
            var gastos = await _gastoRepository.GetByUsuarioIdAsync(usuarioId);
            return ServiceResult<IEnumerable<Gasto>>.Ok(gastos);
        }

        public async Task<ServiceResult<IEnumerable<Gasto>>> GetByCategoriaIdAsync(int categoriaId)
        {
            var gastos = await _gastoRepository.GetByCategoriaIdAsync(categoriaId);
            return ServiceResult<IEnumerable<Gasto>>.Ok(gastos);
        }
    }
}