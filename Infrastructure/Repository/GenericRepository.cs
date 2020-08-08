using Application.Common.RepositoryInterface;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly CarDbContext _context;
        private DbSet<TEntity> _dbSet;

        public GenericRepository(CarDbContext mainDbContext)
        {
            _context = mainDbContext;
            _dbSet = _context.Set<TEntity>();
        }

        public void Delete(TEntity item)
        {
            if (_context.Entry(item).State == EntityState.Detached)
            {
                _dbSet.Attach(item);
            }

            _dbSet.Remove(item);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<TEntity> GetAll(int page, int countOnPage, out int maxElement)
        {
            maxElement = _dbSet.Count();
            return _dbSet.Skip((page - 1) * countOnPage).Take(countOnPage).ToList();
        }

        public TEntity GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<TEntity> GetFiltered(Expression<Func<TEntity, 
                                                bool>> filter = null, Func<IQueryable<TEntity>, 
                                                IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IEnumerable<TEntity> GetFiltered(int page, int countOnPage, out int maxOfElement, 
                                                Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, 
                                                IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                var entity = orderBy(query);
                maxOfElement = entity.Count();
                return entity.Skip((page - 1) * countOnPage).Take(countOnPage).ToList();
            }
            else
            {
                maxOfElement = query.Count();
                return query.Skip((page - 1) * countOnPage).Take(countOnPage).ToList();
            }
        }

        public void Insert(TEntity item)
        {
            _dbSet.Add(item);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public void SetStateModified(TEntity entity)
        {
            _context.Entry<TEntity>(entity).State = EntityState.Modified;
        }

        public void Update(TEntity item)
        {
            try
            {
                _dbSet.Attach(item);
            }
            catch {
            
            }
            finally
            {
                _dbSet.Update(item);
            }
        }
    }
}
