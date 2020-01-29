using System;
using System.Collections.Generic;
using System.Text;
using SecretSanta.Data;
using AutoMapper;

namespace SecretSanta.Business
{
    public class UserService : EntityService<User>, IEntityService<User>
    {
        public UserService(ApplicationDbContext applicationDbContext, IMapper mapper) : base(applicationDbContext, mapper)
        {

        }
    }
}
