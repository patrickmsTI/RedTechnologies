using RedTechnologies.Application.Interfaces;
using RedTechnologies.Domain.Entities;
using RedTechnologies.Domain.Enumerators;
using RedTechnologies.Domain.Interfaces.Services;

namespace RedTechnologies.Application.Services
{
    public class OrderAppService : AppServiceBase<Order, Guid>, IOrderAppService
    {
        private readonly IOrderService _service;

        public OrderAppService(IOrderService orderService) : base(orderService)
        {
            _service = orderService;
        }

        override
        public Order Add(Order obj)
        {
            obj.Id = _service.GetNextId();
            obj.CreatedDate = DateTime.Now;
            return _service.Add(obj);
        }     

        public OrderType CheckEnum(string descriptionEnum)
        {
            var orderTypes = Enum.GetNames(typeof(OrderType)).ToList();
            if (orderTypes.Any(c => c.ToUpper().Equals(descriptionEnum.ToUpper())))
            {
                OrderType enumOrder = (OrderType) Enum.Parse(typeof(OrderType), char.ToUpper(descriptionEnum[0]) + descriptionEnum.Substring(1));
                return enumOrder;
            }
            else
                throw new Exception($"Type \"{descriptionEnum}\" not found.");
        }

    }
}
