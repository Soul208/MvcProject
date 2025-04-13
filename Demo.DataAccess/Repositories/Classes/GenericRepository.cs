using Demo.DataAccess.Data.Contexts;
using Demo.DataAccess.Moodels.DepartmentModel;
using Demo.DataAccess.Moodels.Shared;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);
        public IEnumerable<TEntity> GetAll(bool WithTracking = false)
        {
            if (WithTracking)
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).ToList();
            else
                return _dbContext.Set<TEntity>().Where(E => E.IsDeleted != true).AsNoTracking().ToList();
        }

        public void Update(TEntity entity)
        {

            _dbContext.Set<TEntity>().Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);

        }

        public IEnumerable<TEntity> GetIEnumerable()
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> GetIQueryable()
        {
            return _dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)
        {
            return _dbContext.Set<TEntity>()
                .Where(Predicate)
                .ToList();

        }
    }
}
