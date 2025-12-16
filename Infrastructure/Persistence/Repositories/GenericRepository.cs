using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> (StoreDbContext _dbContext)
        : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking = false) 
            => asNoTracking ? await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync()
            : await _dbContext.Set<TEntity>().ToListAsync();
        
        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);
        
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);
        
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);
        
        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        #region Specifications

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> Specifications)
         => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), Specifications).ToListAsync();

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> Specifications)
         => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), Specifications).FirstOrDefaultAsync();

        public async Task<int> CountAsync(ISpecifications<TEntity, TKey> Specifications)
         => await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), Specifications).CountAsync();
        
        #endregion

    }
}
