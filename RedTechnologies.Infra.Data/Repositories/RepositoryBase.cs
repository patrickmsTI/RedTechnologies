using RedTechnologies.Domain.Interfaces.Repositories;
using RedTechnologies.Infra.Data.Context;
using System.Linq.Expressions;

namespace RedTechnologies.Infra.Data.Repositories
{
    public class RepositoryBase<TEntity, TKey> : IRepositoryBase<TEntity, TKey> where TEntity : class
    {
        protected BaseContext db;
        public RepositoryBase(BaseContext db)
        {
            this.db = db;
        }

        public TEntity Add(TEntity obj)
        {
            db.Set<TEntity>().Add(obj);
            db.SaveChanges();
            return obj;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetByFilter(Expression<Func<TEntity, bool>> filter)
        {
            return db.Set<TEntity>().Where(filter).ToList();
        }

        public TEntity GetById(TKey id)
        {
            var entidade = db.Set<TEntity>().Find(id);
            return entidade;
        }

        public void Remove(TEntity obj)
        {
            db.Set<TEntity>().Remove(obj);
            db.SaveChanges();
        }

        public virtual void Update(TEntity obj)
        {
            db.Set<TEntity>().Update(obj);
            db.SaveChanges();
        }

        public Guid GetNextId()
        {
            return Guid.NewGuid();
        }
    }
}
