using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    static class SpecificationEvaluator
    {
        // Create Query
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> EntryQuery,
            ISpecifications<TEntity, TKey> Specifications) where TEntity : BaseEntity<TKey>
        {
            var Query = EntryQuery; //_dbContext.Set<TEntity>()

            if (Specifications.Criteria is not null)
                Query = Query.Where(Specifications.Criteria);
            // Where(p => p.id == id)
            // Where(p => p.BrandId == 4 && p.TypeId == 2)

            if (Specifications.OrderBy is not null)
            {
                Query = Query.OrderBy(Specifications.OrderBy);
            }
            if (Specifications.OrderByDescending is not null)
            {
                Query = Query.OrderByDescending(Specifications.OrderByDescending);
            }

            if (Specifications.IncludeExpressions is not null && Specifications.IncludeExpressions.Count > 0)
            {
                Query = Specifications.IncludeExpressions.Aggregate(Query, (currentQuery, includeExp) => currentQuery.Include(includeExp));
            }
            //Include(p => p.ProductBrand).Include(p => p.ProductType)

            if (Specifications.IsPaginated)
            {
                Query = Query.Skip(Specifications.Skip).Take(Specifications.Take);
            }

            return Query;
            // _dbContext.Set<TEntity>().Where(p => p.id == id).Include(p => p.ProductBrand).Include(p => p.ProductType)

        }

    }
}
