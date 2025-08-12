using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    static class SpescificationEvaluator
    {
        public static IQueryable<TEntity>GetQuery<TEntity,TKey>
            (IQueryable <TEntity>
            inputQuery,ISpecifications<TEntity , TKey> spec
            )
            where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
           if (spec.Criateria is not null)
           {
                query = query.Where(spec.Criateria);
           }

             query = spec.IncludeExpressions.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));




            return  query;
        }   
    }
}
