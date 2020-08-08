using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Application.Common.RepositoryInterface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        IEnumerable<TEntity> GetAll(int page, int countOnPage, out int maxElement);

        TEntity GetById(Guid id);

        IEnumerable<TEntity> GetFiltered(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
                IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        IEnumerable<TEntity> GetFiltered(int page, int countOnPage, out int maxOfPage,
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>,
               IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "");

        void Insert(TEntity item);

        void Update(TEntity item);

        void Delete(TEntity item);

        void SetStateModified(TEntity entity);

        int Save();
    }
}
