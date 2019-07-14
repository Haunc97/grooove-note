using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace PersonalNotesAPI.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        T Add(T entity);

        // Marks an entity as modified
        void Update(T entity);

        // Marks an entity to be removed
        T Delete(T entity);

        void Delete(int id);

        // Get an entity by int id
        T GetSingleById(int id);

        T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null);
    }

}
