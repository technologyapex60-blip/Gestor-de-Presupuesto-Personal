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

        public override async Task<ServiceResult<Ingreso>> AddAsync(Ingreso entity)
        {
            var validacion = Validar(entity);
            if (!validacion.Success)
                return validacion;

            return await base.AddAsync(entity);
        }

        public override async Task<ServiceResult<Ingreso>> UpdateAsync(Ingreso entity)
        {
            var validacion = Validar(entity);
            if (!validacion.Success)
                return validacion;

            return await base.UpdateAsync(entity);
        }

        private ServiceResult<Ingreso> Validar(Ingreso entity)
        {
            if (entity.Monto <= 0)
                return ServiceResult<Ingreso>.Fail("El monto debe ser mayor a cero.");

            if (entity.Fecha == default)
                return ServiceResult<Ingreso>.Fail("La fecha es obligatoria.");

            if (entity.Fecha > DateTime.UtcNow)
                return ServiceResult<Ingreso>.Fail("La fecha no puede ser futura.");

            if (entity.UsuarioId <= 0)
                return ServiceResult<Ingreso>.Fail("Debe especificar un usuario válido.");

            if (entity.CategoriaId <= 0)
                return ServiceResult<Ingreso>.Fail("Debe especificar una categoría válida.");

            return ServiceResult<Ingreso>.Ok(entity);
        }
    }
}