using AutoMapper;
using RedTechnologies.Application.Model;
using RedTechnologies.Domain.Entities;

namespace RedTechnologies.Application.AutoMapper
{
    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<Order, OrderModel>()
                .ForMember(mdl => mdl.Id, o => o.MapFrom(vwm => vwm.Id.ToString()))
                .ForMember(mdl => mdl.CreatedDate, o => o.MapFrom(vwm => vwm.CreatedDate.ToString()))
                .ForMember(mdl => mdl.Type, o => o.MapFrom(vwm => vwm.Type.ToString()));

        }
    }
}
