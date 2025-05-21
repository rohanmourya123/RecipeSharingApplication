using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity FindById(object id);  // object is use to pass any data type 
        bool Add(TEntity entity);
        bool Update(TEntity entity);
        bool Delete(object id);

        int SaveChanges();

        IQueryable<TEntity> Query();
    }
}
