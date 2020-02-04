using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SecretSanta.Data;
using SecretSanta.Business;

namespace SecretSanta.Api.Controllers
{

    [Route("api/[controller]")]
    public class GiftController : EntityController<Gift>
    {
        public GiftController(IGiftService giftService) : base(giftService) { }
    }
}
