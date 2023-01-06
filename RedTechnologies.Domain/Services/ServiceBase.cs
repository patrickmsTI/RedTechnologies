using RedTechnologies.Domain.Interfaces.Repositories;
using RedTechnologies.Domain.Interfaces.Services;
using System.Linq.Expressions;

namespace RedTechnologies.Domain.Services
{
    public class ServiceBase<TEntity, TKey> : IServiceBase<TEntity, TKey> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity, TKey> _repository;

        public ServiceBase(IRepositoryBase<TEntity, TKey> repository)
        {
            _repository = repository;
        }

        public virtual TEntity Add(TEntity obj)
        {
            return _repository.Add(obj);
        }

        public TEntity GetById(TKey id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public void Update(TEntity obj)
        {
            _repository.Update(obj);
        }

        public void Remove(TEntity obj)
        {
            _repository.Remove(obj);
        }

        public IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return _repository.GetByFilter(filter);
        }

        public Guid GetNextId()
        {
            return this._repository.GetNextId();
        }
    }
}
