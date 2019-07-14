using Microsoft.EntityFrameworkCore;
using PersonalNotesAPI.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PersonalNotesAPI.Data.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : Auditable
    {
        #region Properties
        private PersonalNotesDbContext dataContext;
        private IUserResolverService userResolverService;
        private readonly DbSet<T> dbSet;

        #endregion

        protected RepositoryBase(PersonalNotesDbContext dataContext, IUserResolverService userResolverService)
        {
            this.dataContext = dataContext;
            this.userResolverService = userResolverService;
            dbSet = dataContext.Set<T>();
        }

        #region Implementation
        public virtual T Add(T entity)
        {
            entity.CreatedBy = userResolverService.CurrentUserName();
            entity.CreatedOn = DateTime.Now;
            entity.UpdatedBy = userResolverService.CurrentUserName();
            entity.Deleted = false;
            return dbSet.Add(entity).Entity;
        }

        public virtual void Update(T entity)
        {
            entity.UpdatedBy = userResolverService.CurrentUserName();
            entity.UpdatedOn = DateTime.Now;
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual T Delete(T entity)
        {
            return dbSet.Remove(entity).Entity;
        }
        public virtual void Delete(int id)
        {
            var entity = dbSet.Find(id);
            entity.Deleted = true;
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        public virtual T GetSingleById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string includes)
        {
            return dbSet.Where(where).ToList();
        }


        public virtual int Count(Expression<Func<T, bool>> where)
        {
            return dbSet.Count(where);
        }

        public IEnumerable<T> GetAll()
        {
            return dataContext.Set<T>().AsNoTracking().Where(x => !x.Deleted &&
                                                    x.CreatedBy == userResolverService.CurrentUserName()).AsQueryable<T>();
        }

        public T GetSingleByCondition(Expression<Func<T, bool>> expression, string[] includes = null)
        {
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.FirstOrDefault(expression);
            }
            return dataContext.Set<T>().FirstOrDefault(expression);
        }

        public virtual IEnumerable<T> GetMulti(Expression<Func<T, bool>> predicate, string[] includes = null)
        {
            //HANDLE INCLUDES FOR ASSOCIATED OBJECTS IF APPLICABLE
            if (includes != null && includes.Count() > 0)
            {
                var query = dataContext.Set<T>().Include(includes.First());
                foreach (var include in includes.Skip(1))
                    query = query.Include(include);
                return query.Where<T>(predicate).AsQueryable<T>();
            }

            return dataContext.Set<T>().Where<T>(predicate).AsQueryable<T>();
        }
        #endregion
    }
}
