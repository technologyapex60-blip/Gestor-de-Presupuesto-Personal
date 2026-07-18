using PP.Application.Contract;
using PP.Application.Core;
using PP.Domain.Entities;
using PP.Domain.Repository;

namespace PP.Application.Service
{
    public class CategoriaService : BaseService<Categoria>, ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository) : base(categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<ServiceResult<IEnumerable<Categoria>>> GetByTipoAsync(string tipo)
        {
            var categorias = await _categoriaRepository.GetByTipoAsync(tipo);
            return ServiceResult<IEnumerable<Categoria>>.Ok(categorias);
        }

        public override async Task<ServiceResult<Categoria>> AddAsync(Categoria entity)
        {
            var validacion = Validar(entity);
            if (!validacion.Success)
                return validacion;

            return await base.AddAsync(entity);
        }

        public override async Task<ServiceResult<Categoria>> UpdateAsync(Categoria entity)
        {
            var validacion = Validar(entity);
            if (!validacion.Success)
                return validacion;

            return await base.UpdateAsync(entity);
        }

        private ServiceResult<Categoria> Validar(Categoria entity)
        {
            if (string.IsNullOrWhiteSpace(entity.Nombre))
                return ServiceResult<Categoria>.Fail("El nombre es obligatorio.");

            if (entity.Nombre.Length > 100)
                return ServiceResult<Categoria>.Fail("El nombre no puede superar los 100 caracteres.");

            if (string.IsNullOrWhiteSpace(entity.Tipo))
                return ServiceResult<Categoria>.Fail("El tipo es obligatorio.");

            var tiposValidos = new[] { "Ingreso", "Gasto" };
            if (!tiposValidos.Contains(entity.Tipo))
                return ServiceResult<Categoria>.Fail("El tipo debe ser 'Ingreso' o 'Gasto'.");

            return ServiceResult<Categoria>.Ok(entity);
        }
    }
}