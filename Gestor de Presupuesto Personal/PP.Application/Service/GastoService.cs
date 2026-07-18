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

        public override async Task<ServiceResult<Gasto>> AddAsync(Gasto entity)
        {
            var validacion = Validar(entity);
            if (!validacion.Success)
                return validacion;

            return await base.AddAsync(entity);
        }

        public override async Task<ServiceResult<Gasto>> UpdateAsync(Gasto entity)
        {
            var validacion = Validar(entity);
            if (!validacion.Success)
                return validacion;

            return await base.UpdateAsync(entity);
        }

        private ServiceResult<Gasto> Validar(Gasto entity)
        {
            if (entity.Monto <= 0)
                return ServiceResult<Gasto>.Fail("El monto debe ser mayor a cero.");

            if (entity.Fecha == default)
                return ServiceResult<Gasto>.Fail("La fecha es obligatoria.");

            if (entity.Fecha > DateTime.UtcNow)
                return ServiceResult<Gasto>.Fail("La fecha no puede ser futura.");

            if (entity.UsuarioId <= 0)
                return ServiceResult<Gasto>.Fail("Debe especificar un usuario válido.");

            if (entity.CategoriaId <= 0)
                return ServiceResult<Gasto>.Fail("Debe especificar una categoría válida.");

            return ServiceResult<Gasto>.Ok(entity);
        }
    }
}