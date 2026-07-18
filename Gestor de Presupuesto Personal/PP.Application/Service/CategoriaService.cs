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
    }
}