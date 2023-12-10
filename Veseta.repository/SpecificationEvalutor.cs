﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Veseta.Core.entites;
using Veseta.Core.Specifications;


namespace Veseta.CoreRepository
{
    public static class SpecificationEvalutor<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            if (spec.OrderBy is not null)
                query = query.OrderBy(spec.OrderBy);
            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);

            if (spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);


            query = spec.Includes.Aggregate(query, (currentQuery, expression) => currentQuery.Include(expression));


            return query;
        }
    }
}
