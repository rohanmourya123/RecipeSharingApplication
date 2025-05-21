using System;
using System.Collections.Generic;
using System.Linq;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Implementations
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public bool Add(TEntity entity)
        {
            try
            {
                _dbSet.Add(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(object id)
        {
            try
            {
                var entity = _dbSet.Find(id);
                if (entity == null)
                    return false;

                _dbSet.Remove(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public TEntity FindById(object id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public bool Update(TEntity entity)
        {
            try
            {
                _dbSet.Attach(entity);
                _db.Entry(entity).State = EntityState.Modified;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
