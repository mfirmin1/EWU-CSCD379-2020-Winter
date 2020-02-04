using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Data;

namespace SecretSanta.Api.Controllers
{
    [Route("api/[controller]")]
    public class GroupController : EntityController<Group>
    {
        public GroupController(IGroupService groupService) : base(groupService) { }
    }
}
