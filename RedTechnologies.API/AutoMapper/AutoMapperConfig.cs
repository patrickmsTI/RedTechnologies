using AutoMapper;
using RedTechnologies.Application.AutoMapper;

namespace RedTechnologies.API.AutoMapper
{
    /// <summary>
    /// Class that implements the AutoMapperConfig
    /// </summary>
    public class AutoMapperConfig
    {
        /// <summary>
        /// Registers the mappings.
        /// </summary>
        /// <returns></returns>
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(x =>
            {
                x.AddProfile<DomainToModelMappingProfile>();
                x.AddProfile<ModelToDomainMappingProfile>();
            });
        }
    }
}
