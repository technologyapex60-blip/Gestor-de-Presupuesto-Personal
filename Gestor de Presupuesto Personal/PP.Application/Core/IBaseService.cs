using PP.Domain.Core;

namespace PP.Application.Core
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<ServiceResult<IEnumerable<T>>> GetAllAsync();
        Task<ServiceResult<T>> GetByIdAsync(int id);
        Task<ServiceResult<T>> AddAsync(T entity);
        Task<ServiceResult<T>> UpdateAsync(T entity);
        Task<ServiceResult<bool>> DeleteAsync(int id);
    }
}