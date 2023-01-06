using System.Linq.Expressions;

namespace RedTechnologies.Domain.Interfaces.Services
{
    public interface IServiceBase<TEntity, TKey> where TEntity : class
    {
        TEntity Add(TEntity obj);
        TEntity GetById(TKey id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter);
        void Update(TEntity obj);
        void Remove(TEntity obj);
        Guid GetNextId();
    }
}
