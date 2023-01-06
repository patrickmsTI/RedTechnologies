using RedTechnologies.Domain.Entities;
using RedTechnologies.Domain.Enumerators;

namespace RedTechnologies.Application.Interfaces
{
    public interface IOrderAppService : IAppServiceBase<Order, Guid>
    {
        OrderType CheckEnum(string enumDescription);
    }
   
}
