﻿using Microsoft.EntityFrameworkCore;
using RCM.Domain.Core.Models;
using RCM.Domain.Repositories;
using RCM.Infra.Data.Context;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RCM.Infra.Data.Repositories
{
    public class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : Entity<TModel>
    {
        protected readonly RCMDbContext _dbContext;
        protected readonly DbSet<TModel> _dbSet;

        public BaseRepository(RCMDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TModel>();
        }

        public virtual IQueryable<TModel> Get()
        {
            return _dbSet.AsNoTracking();
        }

        public virtual IQueryable<TModel> Get(Expression<Func<TModel, bool>> expression)
        {
            return _dbSet.Where(expression).AsNoTracking();
        }

        public virtual TModel GetById(Guid id, bool loadRelatedData = true)
        {
            if (loadRelatedData)
                throw new InvalidOperationException("You need to override this method in order to include entity related data");

            return _dbSet.Find(id);
        }

        public virtual void Add(TModel model)
        {
            _dbSet.Add(model);
        }

        public virtual void Update(TModel model)
        {
            _dbContext.Update(model);
        }

        public virtual void Remove(TModel model)
        {
            _dbSet.Remove(model);
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(_dbContext);
        }
    }
}
