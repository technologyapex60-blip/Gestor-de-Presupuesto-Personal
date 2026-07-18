using PP.Domain.Core;
using PP.Domain.Repository;

namespace PP.Application.Core
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<IEnumerable<T>>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return ServiceResult<IEnumerable<T>>.Ok(entities);
        }

        public async Task<ServiceResult<T>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult<T>.Fail("Registro con Id " + id + " no encontrado.");

            return ServiceResult<T>.Ok(entity);
        }

        public async Task<ServiceResult<T>> AddAsync(T entity)
        {
            var created = await _repository.AddAsync(entity);
            return ServiceResult<T>.Ok(created, "Registro creado correctamente.");
        }

        public async Task<ServiceResult<T>> UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
            return ServiceResult<T>.Ok(entity, "Registro actualizado correctamente.");
        }

        public async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return ServiceResult<bool>.Fail("Registro con Id " + id + " no encontrado.");

            await _repository.DeleteAsync(id);
            return ServiceResult<bool>.Ok(true, "Registro eliminado correctamente.");
        }
    }
}