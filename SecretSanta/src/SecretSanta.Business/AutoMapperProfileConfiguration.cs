using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SecretSanta.Data;
using System.Reflection;

namespace SecretSanta.Business
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Gift, Gift>().ForMember(property => property.Id, option => option.Ignore());
            CreateMap<User, User>().ForMember(property => property.Id, option => option.Ignore());
        }

        public static IMapper CreateMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetExecutingAssembly());
            });
            return mapperConfiguration.CreateMapper();
        }
    }
}
