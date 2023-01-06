using AutoMapper;
using RedTechnologies.Application.Model;
using RedTechnologies.Domain.Entities;
using RedTechnologies.Domain.Enumerators;

namespace RedTechnologies.Application.AutoMapper
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<OrderModel, Order>()
                .ForMember(c => c.Type, o => o.MapFrom(d => (OrderType)Enum.Parse(typeof(OrderType), d.Type)));
        }
    }
}
