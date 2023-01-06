using RedTechnologies.Application.Interfaces;
using RedTechnologies.Domain.Interfaces.Services;
using System.Linq.Expressions;

namespace RedTechnologies.Application.Services
{
    public class AppServiceBase<TEntity, TKey> : IAppServiceBase<TEntity, TKey> where TEntity : class
    {
        private readonly IServiceBase<TEntity, TKey> _serviceBase;

        public AppServiceBase(IServiceBase<TEntity, TKey> serviceBase)
        {
            _serviceBase = serviceBase;
        }


        public virtual TEntity Add(TEntity obj)
        {
            return _serviceBase.Add(obj);
        }

        public TEntity GetById(TKey id)
        {
            return _serviceBase.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _serviceBase.GetAll();
        }

        public void Update(TEntity obj)
        {
            _serviceBase.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            _serviceBase.Remove(obj);
        }

        public IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return _serviceBase.GetByFilter(filter);
        }

        public Guid GetNextId()
        {
            return this._serviceBase.GetNextId();
        }
    }
}
