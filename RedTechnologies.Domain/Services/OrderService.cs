using RedTechnologies.Domain.Entities;
using RedTechnologies.Domain.Interfaces.Repositories;
using RedTechnologies.Domain.Interfaces.Services;

namespace RedTechnologies.Domain.Services
{
    public class OrderService : ServiceBase<Order, Guid>, IOrderService
    {
        private IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
        }
    }
}
