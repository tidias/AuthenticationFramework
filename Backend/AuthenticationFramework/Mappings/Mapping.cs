using System;
using AuthenticationFramework.Entities;
using AuthenticationFramework.Models.UserRegistration;
using AutoMapper;

namespace AuthenticationFramework.Mappings
{
    public static class Mapping
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(options =>
            {
                options.ShouldMapProperty = x => x.GetMethod != null && (x.GetMethod.IsPublic || x.GetMethod.IsAssembly);
                options.AddProfile<MappingProfile>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRegistrationDTO, User>();
        }
    }
}
