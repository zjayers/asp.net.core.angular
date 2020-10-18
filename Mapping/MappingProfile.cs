using asp.net.core.angular.Models;
using asp.net.core.angular.wwwroot;
using AutoMapper;

namespace asp.net.core.angular.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
        }
    }
}
