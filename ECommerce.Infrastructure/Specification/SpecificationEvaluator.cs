using ECommerce.Domin.Contracts;
using ECommerce.Domin.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Specification
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> entryPoint , 
                                                                     ISpecification<TEntity, TKey> spec) where TEntity : BaseEntity<TKey>
        {
            //1- entry point  
            var query = entryPoint;
            //2- where()
            //3- include()
            ////if (spec.IncludeExpressions.Any())
            ////{
            ////    foreach (var expression in spec.IncludeExpressions)
            ////    {
            ////        query = query.Include(expression);
            ////    }
            ////}

            // same but less code and more readable ↓↓↓↓
            // current >>>>>> dbContext.Set<>()
            query = spec.IncludeExpressions.Aggregate(query, (current, nextExp) => current.Include(nextExp));
            return query;
        }
    }
}
