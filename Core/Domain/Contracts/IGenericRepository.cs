using System.Linq.Expressions;
using Domain.Models;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync (bool asNoTracking = false);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);


        #region Specifications

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> Specifications);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> Specifications);
        Task<int> CountAsync(ISpecifications<TEntity, TKey> Specifications);

        #endregion
    }
}
