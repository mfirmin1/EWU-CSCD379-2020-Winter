using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using System.Threading.Tasks;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    public class UserController : EntityController<User>
    {
        public UserController(IUserService userService) : base(userService) { }
    }
}
