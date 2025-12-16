using System.Linq.Expressions;
using Domain.Contracts;
using Domain.Models;

namespace Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity 
        : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        
        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
        protected void AddIncludes(Expression<Func<TEntity, object>> Expressions)
        => IncludeExpressions.Add(Expressions);
        
        #endregion

        #region Sorting

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

  
        protected void AddOrderBy(Expression<Func<TEntity, object>> order) => OrderBy = order;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderDesc) => OrderByDescending = orderDesc;

        #endregion

        #region Pagination

        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaginated { get ; private set; }

        // Total Count = 40
        // PageSize = 10
        // PageIndex = 3
        protected void ApplyPagination(int pageSize , int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }

        #endregion


    }
}
