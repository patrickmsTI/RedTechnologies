using Microsoft.EntityFrameworkCore.Update.Internal;
using RedTechnologies.Domain.Entities;
using RedTechnologies.Domain.Interfaces.Repositories;
using RedTechnologies.Infra.Data.Context;

namespace RedTechnologies.Infra.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order, Guid>, IOrderRepository
    {

        protected BaseContext db;

        public OrderRepository(BaseContext context) : base(context)
        {
            db = context;
        }

        override
        public void Update(Order obj)
        {
            var dbCopy = db.Order.First(c => c.Id == obj.Id);
            obj.CreatedByUsername = dbCopy.CreatedByUsername;
            obj.CreatedDate = dbCopy.CreatedDate;
            db.Entry(dbCopy).CurrentValues.SetValues(obj);
            db.SaveChanges();
        }


    }
}
