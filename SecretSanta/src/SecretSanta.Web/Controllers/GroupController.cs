using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using SecretSanta.Web.Api;

namespace SecretSanta.Web.Controllers
{
    public class GroupController : Controller
    {
        IHttpClientFactory ClientFactory { get; }
        public GroupController(IHttpClientFactory clientFactory)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            ClientFactory = clientFactory;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = ClientFactory.CreateClient("SantaApi");

            GroupClient client = new GroupClient(httpClient);
            ICollection<Group> gifts = await client.GetAllAsync();
            return View(client);
        }
    }
}
